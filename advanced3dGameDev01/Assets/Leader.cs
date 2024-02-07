using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    GameObject[] teamMembers;
    int nbTeamMembers,
        nbTargets;

    int WPIndex;
    GameObject[] WPs;

    // Start is called before the first frame update
    void Start()
    {
        teamMembers = GameObject.FindGameObjectsWithTag("teamMember");
        WPIndex = 0;
        WPs = new GameObject[] { GameObject.Find("WP0"),GameObject.Find("WP1"),GameObject.Find("WP2"),GameObject.Find("WP3"),GameObject.Find("WP4")};
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Retreat();
        }
    }

    void Attack()
    {
        GameObject[] allTargets;
        allTargets = GameObject.FindGameObjectsWithTag("target");
        nbTargets = allTargets.Length;
        for (int i = 0; i < nbTargets; i++)
        {
            teamMembers[i].GetComponent<TeamMember>().Attack(allTargets[i]);
        }
    }

    void Retreat()
    {
        for (int i = 0; i < nbTargets; i++)
        {
            teamMembers[i].GetComponent<TeamMember>().Retreat();
        }
    }
}
