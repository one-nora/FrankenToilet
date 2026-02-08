using UnityEngine;

namespace FrankenToilet.duviz;

public class DestroyInTime : MonoBehaviour
{
    float timer = 1.5f;

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }
}