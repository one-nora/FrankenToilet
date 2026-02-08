using FrankenToilet.Core;
using HarmonyLib;
using UnityEngine;

namespace FrankenToilet.duviz;

[PatchOnEntry]
[HarmonyPatch(typeof(DeathSequence))]
public static class DeathSequencePatch
{
    [HarmonyPatch("OnEnable")]
    [HarmonyPrefix]
    public static bool OnEnable_Prefix(DeathSequence __instance)
    {
        TimeController.Instance.SlowDown(0.15f);

        return true;
    }

    [HarmonyPatch("EndSequence")]
    [HarmonyPrefix]
    public static bool EndSequence_Prefix(DeathSequence __instance)
    {
        TimeController.Instance.RestoreTime();

        return true;
    }
}

[PatchOnEntry]
[HarmonyPatch(typeof(NewMovement))]
public class NewMovementPatch
{
    [HarmonyPatch("Respawn")]
    [HarmonyPrefix]
    public static bool Respawn_Prefix(DeathSequence __instance)
    {
        TimeController.Instance.SlowDown(0.3f);

        return true;
    }
}

[PatchOnEntry]
[HarmonyPatch(typeof(FinalRank))]
public class FinalRankPatch
{
    static Vector3 originalPosition;
    static float screenshakeTime = 0;
    [HarmonyPatch("Appear")]
    [HarmonyPostfix]
    public static void Appear_Postfix(FinalRank __instance)
    {
        if (__instance.toAppear == null) return;
        if (__instance.toAppear.Length == 0) return;

        var i = Traverse.Create(__instance).Field("i").GetValue<int>();

        if (i >= __instance.toAppear.Length) return;

        if (__instance.totalRank == null) { screenshakeTime += 0.2f; return; }

        if (__instance.toAppear[i] == __instance.totalRank.gameObject)
        {
            if (originalPosition == Vector3.zero) originalPosition = __instance.transform.localPosition;
            screenshakeTime += 0.2f;
            if (__instance.totalRank.text == "<color=#FFFFFF>P</color>")
                screenshakeTime += 0.6f;
        }
    }

    [HarmonyPatch("FlashPanel")]
    [HarmonyPrefix]
    public static bool FlashPanel_Prefix(FinalRank __instance, GameObject panel)
    {
        if (originalPosition == Vector3.zero) originalPosition = __instance.transform.localPosition;
        screenshakeTime += 0.4f;

        return true;
    }

    [HarmonyPatch("Update")]
    [HarmonyPrefix]
    public static bool Update_Prefix(FinalRank __instance)
    {
        if (screenshakeTime > 0)
        {
            screenshakeTime -= Time.deltaTime;
            __instance.transform.localPosition = originalPosition + Random.insideUnitSphere * Time.deltaTime * 300 * screenshakeTime;
            return true;
        }
        else
        {
            screenshakeTime = 0;
        }
        __instance.transform.localPosition = Vector3.Lerp(__instance.transform.localPosition, originalPosition, Time.deltaTime * 5f);
        return true;
    }

    [HarmonyPatch("Start")]
    [HarmonyPrefix]
    public static bool Start_Prefix(FinalRank __instance)
    {
        screenshakeTime = 0;
        return true;
    }
}

[PatchOnEntry]
[HarmonyPatch(typeof(LeaderboardController))]
[HarmonyPatch("SubmitLevelScore")]
internal class LeaderboardControllerPatch
{
    public static bool Prefix(string levelName, int difficulty, float seconds, int kills, int style, int restartCount, bool pRank = false)
    {
        return false;
    }
}