namespace FrankenToilet.Bryan.Patches;

using FrankenToilet.Core;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[PatchOnEntry]
[HarmonyPatch(typeof(Projectile))]
public static class ProjectilePatch
{
    /// <summary> Add a projectile fucker to the projectile and that will handle all the fancy shit </summary>
    [HarmonyPrefix]
    [HarmonyPatch("Awake")]
    public static void meow(Projectile __instance) => 
        __instance.gameObject.AddComponent<ProjectileFucker>();
}