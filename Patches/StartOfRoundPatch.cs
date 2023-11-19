using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine.UI;
using UnityEngine;

namespace no00ob.Mod.LethalCompany.ItemAdditions.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch(nameof(StartOfRound.openingDoorsSequence))]
        [HarmonyPostfix]
        static void openingDoorsSequencePatch()
        {
            HUDManager.Instance.AddTextToChatOnServer("Minimap Test");

            GameObject rawImageObject = GameObject.Find("minimap_display");
            RawImage rawImage = null;

            if (rawImageObject == null)
            {
                rawImageObject = new GameObject("minimap_display");
            }

            if (rawImageObject.TryGetComponent(out CanvasGroup cg))
            {
                cg.ignoreParentGroups = true;
                cg.blocksRaycasts = false;
                cg.alpha = 1f;
            }
            else
            {
                cg = rawImageObject.AddComponent<CanvasGroup>();
                cg.ignoreParentGroups = true;
                cg.blocksRaycasts = false;
                cg.alpha = 1f;
            }

            if (rawImageObject.TryGetComponent(out RawImage img))
            {
                rawImage = img;
            }
            else
            {
                rawImage = rawImageObject.AddComponent<RawImage>();
            }

            GameObject parentObject = GameObject.Find("IngamePlayerHUD");

            rawImageObject.transform.SetParent(parentObject.transform.parent, false);

            RenderTexture rTex = StartOfRound.Instance.mapScreen.cam.targetTexture;

            ItemAdditionsMod.Instance.logger.LogDebug($"RenderTexture {rTex} {rTex.name} assigned to {GameNetworkManager.Instance.localPlayerController.playerUsername}!");

            if (rTex != null)
            {
                rawImage.texture = rTex;
            }

            rawImage.rectTransform.anchorMax = new Vector2(1f, 0.5f);
            rawImage.rectTransform.anchorMin = new Vector2(1f, 0.5f);
            rawImage.rectTransform.pivot = new Vector2(1f, 0.5f);
            rawImage.rectTransform.anchoredPosition3D = Vector3.zero;

            rawImage.rectTransform.sizeDelta = new Vector2(Mathf.RoundToInt(Screen.width * 0.05f), Mathf.RoundToInt(Screen.height * 0.05f));
        }

        [HarmonyPatch(nameof(StartOfRound.ShipHasLeft))]
        [HarmonyPrefix]
        static void ShipHasLeftPatch()
        {
            GameObject rawImageObject = GameObject.Find("minimap_display");

            if (rawImageObject != null)
            {
                if (rawImageObject.TryGetComponent(out CanvasGroup group))
                {
                    group.ignoreParentGroups = true;
                    group.blocksRaycasts = false;
                    group.alpha = 0f;
                }
            }
        }
    }
}
