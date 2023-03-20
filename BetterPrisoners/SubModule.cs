using HarmonyLib;
using BetterPrisoners.Utils;
using BetterPrisoners.Settings;
using TaleWorlds.MountAndBlade;

namespace BetterPrisoners {
	public class SubModule : MBSubModuleBase {

		protected override void OnBeforeInitialModuleScreenSetAsRoot() {
			base.OnBeforeInitialModuleScreenSetAsRoot();
			string modName = base.GetType().Assembly.GetName().Name;

			new Harmony("Bannerlord.Shadow." + modName).PatchAll();
			Helper.SetModName(modName);
			Helper.settings = SettingsManager.Instance;
		}
	}
}
