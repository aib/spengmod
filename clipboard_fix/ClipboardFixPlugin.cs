using HarmonyLib;
using VRage.Plugins;

public class ClipboardFixPlugin: IPlugin
{
	public const string CLIPBOARD_FILENAME = "/tmp/speng_clipboard";

	public void Init(object gameInstance)
	{
		var harmony = new Harmony("ClipboardFixPlugin");
		harmony.PatchAll();
		harmony.GetPatchedMethods().Do(_ => {});
	}

	public void Update()
	{
	}

	public void Dispose()
	{
	}

	[HarmonyPatch(typeof(VRage.Platform.Windows.Forms.MyClipboardHelper), "SetClipboard")]
	public static class Patch_MyClipboardHelper_SetClipboard
	{
		public static bool Prefix(ref string text)
		{
			System.IO.File.WriteAllText(CLIPBOARD_FILENAME, text);
			return false;
		}
	}
}
