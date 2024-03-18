using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationToReach : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Player")
        {
            GameObject
                .Find("Manager")
                .GetComponent<QuestSystem>()
                .Notify(QuestSystem.possibleActions.enter_a_place_called, gameObject.name);
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
