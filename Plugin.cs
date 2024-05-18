using BepInEx;
using HarmonyLib;

namespace KickWithoutBan
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        const string PLUGIN_GUID = "butterystancakes.lethalcompany.kickwithoutban", PLUGIN_NAME = "Kick Without Ban", PLUGIN_VERSION = "1.0.0";

        void Awake()
        {
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