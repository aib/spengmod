using HarmonyLib;
using System;
using System.Collections.Generic;
using VRage.Plugins;
using VRage.Scripting;

public class SandboxEscapePlugin: IPlugin
{
	private Harmony harmony = new Harmony("SandboxEscapePlugin");

	public void Init(object gameInstance)
	{
		harmony.PatchAll();
		// No idea why, but this seems to be necessary
		harmony.GetPatchedMethods().Do(_ => {});
	}

	public void Update()
	{
	}

	public void Dispose()
	{
	}

	[HarmonyPatch(
		typeof(MyScriptCompiler),
		"Compile",
		new Type[] {
			typeof(MyApiTarget), typeof(string), typeof(IEnumerable<Script>), typeof(List<MyScriptCompiler.Message>), typeof(string), typeof(bool)
		}
	)]
	public static class Patch_Compile
	{
		public static void Prefix(ref MyApiTarget target)
		{
			target = MyApiTarget.None;
		}
	}
}
