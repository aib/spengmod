using System;
using System.Collections.Generic;
using System.Linq;
using VRage.Game.ModAPI.Ingame;
using VRageMath;

public
class DamageDisplay
{
	const float DENT_LIMIT = .75f;

	const char ARMOR_COLOR      = '\xe110';
	const char PRISTINE_COLOR   = '\xe120';
	const char DENTED_COLOR     = '\xe1e0';
	const char DAMAGED_COLOR    = '\xe1c8';
	const char BROKEN_COLOR     = '\xe1c0';

	private readonly IMyCubeGrid grid;
	private readonly Action<string> log;

	private List<Vector3I> gridBlocks;
	private Vector3I gridMin;
	private Vector3I gridMax;

	public DamageDisplay(IMyCubeGrid grid, Action<string> logger = null)
	{
		this.grid = grid;
		this.log = logger ?? (_ => {});
	}

	public void initialize(Vector3I rootPos)
	{
		log($"Initializing...");
		var cw = new CubeWalker(grid);
		cw.walk(rootPos);

		gridBlocks = cw.getFull().ToList();
		gridMin = new Vector3I(gridBlocks.Min(v => v.X), gridBlocks.Min(v => v.Y), gridBlocks.Min(v => v.Z));
		gridMax = new Vector3I(gridBlocks.Max(v => v.X), gridBlocks.Max(v => v.Y), gridBlocks.Max(v => v.Z));

		log($"{gridBlocks.Count} blocks loaded");
	}

	public void draw(IScaledPixelDrawer drawer)
	{
		Func<Vector3I, Vector2I> project = v => new Vector2I(v.X, v.Z);

		var armor    = new List<Vector2I>();
		var pristine = new List<Vector2I>();
		var dented   = new List<Vector2I>();
		var damaged  = new List<Vector2I>();
		var broken   = new List<Vector2I>();

		var lengths = gridMax - gridMin + new Vector3I(1, 1, 1);
		drawer.setScale(project(lengths).X, project(lengths).Y);

		foreach (var pos in gridBlocks) {
			var block = grid.GetCubeBlock(pos);
			Vector2I ppos = project(pos - gridMin);

			if (grid.CubeExists(pos)) {
				if (block == null) { // Armor block
					armor.Add(ppos);
				} else if (block.IsFullIntegrity) {
					pristine.Add(ppos);
				} else {
					var health = (block.BuildIntegrity - block.CurrentDamage) / block.MaxIntegrity;
					if (health < DENT_LIMIT) {
						damaged.Add(ppos);
					} else {
						dented.Add(ppos);
					}
				}
			} else {
				broken.Add(ppos);
			}
		}

		armor   .ForEach(p => drawer.drawPixel(p.X, p.Y, ARMOR_COLOR));
		pristine.ForEach(p => drawer.drawPixel(p.X, p.Y, PRISTINE_COLOR));
		dented  .ForEach(p => drawer.drawPixel(p.X, p.Y, DENTED_COLOR));
		damaged .ForEach(p => drawer.drawPixel(p.X, p.Y, DAMAGED_COLOR));
		broken  .ForEach(p => drawer.drawPixel(p.X, p.Y, BROKEN_COLOR));
	}
}
