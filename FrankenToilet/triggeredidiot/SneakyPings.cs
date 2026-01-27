using System.Collections;
using FrankenToilet.Core;
using HarmonyLib;
using UnityEngine;

namespace FrankenToilet.triggeredidiot;

// id use [EntryPoint] but then it'd print FrankenToilet.triggeredidiot.SneakyPings or whatever and that kinda ruins it

[PatchOnEntry]
[HarmonyPatch(typeof(NewMovement), "Start")]
public static class SneakyPingsInjector_Start
{
    private static MonoBehaviour? _current = null;
    private static IEnumerator Runner()
    {
        float minTime = 20000.0f;
        float maxTime = 30000.0f;

        if (SteamHelper.IsSlopTuber)
        {
            minTime = 60;
            maxTime = 240;

            // allow them to start getting their recording ready and testing done before actually trying to fake ping
            float waitTime = Random.Range(300.0f, 400.0f);
            yield return new WaitForSecondsRealtime(waitTime);
        }

        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSecondsRealtime(waitTime);

            AssetsController.LoadAsset("Ping");
            yield return null;
        }
    }

    public static void Prefix(NewMovement __instance)
    {
        if(_current != null) return;
        _current = new GameObject("PingRunner").AddComponent<DontDestroy>();
        _current.StartCoroutine(Runner());
    }
}