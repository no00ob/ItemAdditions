using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
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

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);

            logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");
        }

        [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.openingDoorsSequence))]
        [HarmonyPostfix]
        static void openingDoorsSequencePatch()
        {
            HUDManager.Instance.AddTextToChatOnServer("Minimap Test");

            GameObject rawImageObject = new GameObject("minimap_display");
            RawImage rawImage = rawImageObject.AddComponent<RawImage>();

            GameObject parentObject = GameObject.Find("IngamePlayerHUD");

            rawImageObject.transform.SetParent(parentObject.transform.parent, false);

            RenderTexture rTex = GameNetworkManager.Instance.localPlayerController.gameplayCamera.targetTexture;

            Instance.logger.LogDebug($"RenderTexture {rTex} {rTex.name} assigned to {GameNetworkManager.Instance.localPlayerController.playerUsername}!");

            if (rTex != null )
            {
                rawImage.texture = rTex;
            }       

            rawImage.rectTransform.anchorMax = new Vector2(1f, 0.5f);
            rawImage.rectTransform.anchorMin = new Vector2(1f, 0.5f);
            rawImage.rectTransform.pivot = new Vector2(1f, 0.5f);
            rawImage.rectTransform.anchoredPosition3D = Vector3.zero;

            rawImage.rectTransform.sizeDelta = new Vector2(Mathf.RoundToInt(Screen.width * 0.25f), Mathf.RoundToInt(Screen.height * 0.25f));
        }
    }
}