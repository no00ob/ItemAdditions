using BepInEx;
using BepInEx.Harmony;
using BepInEx.Logging;
using HarmonyLib;
using no00ob.Mod.LethalCompany.ItemAdditions.Patches;
using UnityEngine;
using UnityEngine.UI;

namespace no00ob.Mod.LethalCompany.ItemAdditions
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class ItemAdditionsMod : BaseUnityPlugin
    {
        private const string PLUGIN_GUID = "no00ob.Mod.LethalCompany.ItemAdditions";
        private const string PLUGIN_NAME = "Item Additions";
        private const string PLUGIN_VERSION = "1.0.0";

        private readonly Harmony harmony = new Harmony(PLUGIN_GUID);

        public static ItemAdditionsMod Instance;

        internal ManualLogSource logger;

        public bool minimapOn;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);

            logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");

            harmony.PatchAll(typeof(ItemAdditionsMod));
            harmony.PatchAll(typeof(StartOfRoundPatch));
            harmony.PatchAll(typeof(ManualCameraRendererPatch));
        }
    }
}