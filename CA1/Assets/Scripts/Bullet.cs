using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject, 0);
        }
    }
}
