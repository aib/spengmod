using System;
using System.Collections.Generic;
using VRage.Game.ModAPI.Ingame;
using VRageMath;

public
class CubeWalker
{
	private readonly IMyCubeGrid cubeGrid;
	private readonly ISet<Vector3I> visited;
	private readonly ISet<Vector3I> full;
	private readonly Queue<Vector3I> visitQueue;

	public CubeWalker(IMyCubeGrid cubeGrid)
	{
		this.cubeGrid = cubeGrid;
		visited = new HashSet<Vector3I>();
		full = new HashSet<Vector3I>();
		visitQueue = new Queue<Vector3I>();
	}

	public void walk(Vector3I startPos)
	{
		visitQueue.Enqueue(startPos);
		finishWalk();
	}

	private void finishWalk()
	{
		try {
			while (true) {
				var pos = visitQueue.Dequeue();

				if (visited.Add(pos)) {
					if (cubeGrid.CubeExists(pos)) {
						full.Add(pos);
						visitQueue.Enqueue(new Vector3I(pos.X - 1, pos.Y, pos.Z));
						visitQueue.Enqueue(new Vector3I(pos.X + 1, pos.Y, pos.Z));
						visitQueue.Enqueue(new Vector3I(pos.X, pos.Y - 1, pos.Z));
						visitQueue.Enqueue(new Vector3I(pos.X, pos.Y + 1, pos.Z));
						visitQueue.Enqueue(new Vector3I(pos.X, pos.Y, pos.Z - 1));
						visitQueue.Enqueue(new Vector3I(pos.X, pos.Y, pos.Z + 1));
					}
				}
			}
		} catch (InvalidOperationException) {}
	}

	public IEnumerable<Vector3I> getFull() => full;
}
