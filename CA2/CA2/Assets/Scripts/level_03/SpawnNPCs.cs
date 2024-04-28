using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCs : MonoBehaviour
{
    public float spawnPeriod;
    float timer;
    public GameObject npc;
    GameObject t;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        spawnPeriod = 10;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnPeriod)
        {
            timer = 0;
            t = Instantiate(npc, transform.position, Quaternion.identity);
            t.tag = "npc";
            float newHearingDistance = GameObject
                .Find("dynamicGamePlay")
                .GetComponent<ControlDynamicGameplay>()
                .npcHearingRadius = 3.5f;
            t.GetComponent<ControlNPCDynamic>().ChangeHearingDistance(newHearingDistance);
        }
    }
}
