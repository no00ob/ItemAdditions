using System;
using System.Collections.Generic;
using System.Text;
using GameNetcodeStuff;
using HarmonyLib;

namespace no00ob.Mod.LethalCompany.ItemAdditions.Patches
{
    [HarmonyPatch(typeof(ManualCameraRenderer))]
    internal class ManualCameraRendererPatch
    {
        [HarmonyPatch("MeetsCameraEnabledConditions")]
        [HarmonyPrefix]
        static bool MeetsCameraEnabledConditions(PlayerControllerB player)
        {
            return true;
        }
    }
}
