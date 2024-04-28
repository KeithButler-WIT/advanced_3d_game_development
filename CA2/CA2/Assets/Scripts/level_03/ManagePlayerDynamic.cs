using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePlayerDynamic : MonoBehaviour
{
    Vector3 playerInitialPosition;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "checkPoint")
        {
            GameObject
                .Find("dynamicGamePlay")
                .GetComponent<ControlDynamicGameplay>()
                .ChangePlayerSkillLevel(+1, "CheckPoint");
            Destroy(coll.collider.gameObject);
        }
        if (coll.collider.tag == "deathZone")
        {
            GameObject
                .Find("dynamicGamePlay")
                .GetComponent<ControlDynamicGameplay>()
                .ChangePlayerSkillLevel(-1, "Death Zone");
            GetComponent<Collider>().enabled = false;
            transform.position = playerInitialPosition;
            GetComponent<Collider>().enabled = true;
            Destroy(coll.collider.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update() { }
}
