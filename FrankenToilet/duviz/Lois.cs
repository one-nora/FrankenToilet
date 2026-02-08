#pragma warning disable CS8618
using UnityEngine;

namespace FrankenToilet.duviz;

public class Lois : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 visibleOffset;

    float randomTime = 0;
    bool isPlayerActive = false;

    AudioSource source;
    Animator animator;

    public void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = Bundle.bundle.LoadAsset<AudioClip>("goofy-ahh-car-horn-sound-effect");

        GameObject canvas = Instantiate(Bundle.bundle.LoadAsset<GameObject>("BoatCanvas"));
        GameObject wheel = canvas.transform.Find("Wheel").gameObject;
        animator = wheel.GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void LateUpdate()
    {
        randomTime += Time.deltaTime;

        if (!isPlayerActive)
            if (NewMovement.instance.activated)
            {
                HudMessageReceiver.instance.SendHudMessage("<color=blue>Ferryman (the 2nd): </color>Sorry, we're having problems with the boat");
                isPlayerActive = true;
            }

        if (randomTime > 10)
        {
            randomTime = 0;
            source.Play();
            animator.SetTrigger("oop");
            offset = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
        }

        visibleOffset = Vector3.Lerp(visibleOffset, offset, Mathf.Clamp01(Time.deltaTime * 5));
        offset = Vector3.Lerp(offset, Vector3.zero, Mathf.Clamp01(Time.deltaTime));

        CameraController.instance.transform.localEulerAngles += visibleOffset;
    }
}