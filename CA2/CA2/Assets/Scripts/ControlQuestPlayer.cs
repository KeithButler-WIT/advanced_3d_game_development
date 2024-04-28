using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlQuestPlayer : MonoBehaviour
{

    void OnCollisionEnter(Collision coll)
    {
        print("Collided with " + coll.gameObject.name);
        if (coll.collider.gameObject.tag == "collect")
        {
            GameObject
                .Find("Manager")
                .GetComponent<QuestSystem>()
                .Notify(QuestSystem.possibleActions.acquire_a, coll.gameObject.name);
            Destroy(coll.collider.gameObject);
        }
        if (coll.collider.gameObject.tag == "dialogue")
        {
            GameObject
                .Find("Manager")
                .GetComponent<QuestSystem>()
                .Notify(QuestSystem.possibleActions.talk_to, coll.gameObject.name);
        }
        if (coll.collider.gameObject.tag == "guard")
        {
            GameObject
                .Find("Manager")
                .GetComponent<QuestSystem>()
                .Notify(QuestSystem.possibleActions.destroy_one, coll.gameObject.name);
            Destroy(coll.collider.gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    	{
    		if (coll.GetComponent<Collider>().gameObject.tag == "location") 
	    	{
	    		//Destroy (coll.collider.gameObject);
	    		GameObject.Find("Manager").GetComponent<QuestSystem>().Notify(QuestSystem.possibleActions.enter_place_called, coll.gameObject.name);
	    	}

    	}
}
