using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "player")
            GameObject.Find("NPC").GetComponent<ManageNPCWeek2>().playerEnteredAmbushArea();
    }

    // Update is called once per frame
    void Update() { }
}
