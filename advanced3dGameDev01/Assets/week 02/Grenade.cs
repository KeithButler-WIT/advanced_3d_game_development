using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject, 0);
        }
    }
}
