using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;

namespace KickWithoutBan
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInDependency(GUID_LOBBY_COMPATIBILITY, BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        internal const string PLUGIN_GUID = "butterystancakes.lethalcompany.kickwithoutban", PLUGIN_NAME = "Kick Without Ban", PLUGIN_VERSION = "1.0.1";

        const string GUID_LOBBY_COMPATIBILITY = "BMX.LobbyCompatibility";

        void Awake()
        {
            if (Chainloader.PluginInfos.ContainsKey(GUID_LOBBY_COMPATIBILITY))
            {
                Logger.LogInfo("CROSS-COMPATIBILITY - Lobby Compatibility detected");
                LobbyCompatibility.Init();
            }

            new Harmony(PLUGIN_GUID).PatchAll();

            Logger.LogInfo($"{PLUGIN_NAME} v{PLUGIN_VERSION} loaded");
        }
    }

    [HarmonyPatch]
    class KickWithoutBanPatches
    {
        [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.KickPlayer))]
        [HarmonyPostfix]
        static void PostKickPlayer(StartOfRound __instance)
        {
            __instance.KickedClientIds.Clear();
        }
    }
}