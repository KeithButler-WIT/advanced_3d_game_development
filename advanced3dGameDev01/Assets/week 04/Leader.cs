using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Leader : MonoBehaviour
{
    Animator anim;
    AnimatorStateInfo info;

    GameObject[] teamMembers;
    int nbTeamMembers,
        nbTargets;
    int WPIndex;
    GameObject[] WPs;
    float patrolTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (gameObject.name == "player")
            teamMembers = GameObject.FindGameObjectsWithTag("teamMember");
        else
            teamMembers = GameObject.FindGameObjectsWithTag("team2");
        nbTeamMembers = teamMembers.Length;
        WPIndex = 0;
        WPs = new GameObject[]
        {
            GameObject.Find("WP0"),
            GameObject.Find("WP1"),
            GameObject.Find("WP2"),
            GameObject.Find("WP3"),
            GameObject.Find("WP4")
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "player")
        {
            if (Input.GetKeyDown(KeyCode.P))
                Attack();
            if (Input.GetKeyDown(KeyCode.O))
                Retreat();
        }
        else
        {
            patrolTimer += Time.deltaTime;
            info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("Idle"))
            {
                if (patrolTimer >= 5)
                {
                    patrolTimer = 0;
                    anim.SetTrigger("startPatrol");
                }
            }
            if (info.IsName("Patrol"))
            {
                if (patrolTimer >= 4)
                {
                    DetectEnemies();
                    patrolTimer = 0;
                }
                if (
                    Vector3.Distance(gameObject.transform.position, WPs[WPIndex].transform.position)
                    < 1.0f
                )
                    WPIndex++;
                if (WPIndex > 4)
                    WPIndex = 0;
                GetComponent<NavMeshAgent>().SetDestination(WPs[WPIndex].transform.position);
                GetComponent<NavMeshAgent>().isStopped = false;
            }
            if (info.IsName("Attack")) { }
        }
    }

    void DetectEnemies()
    {
        if (
            Vector3.Distance(
                gameObject.transform.position,
                GameObject.Find("player").transform.position
            ) < 10.0f
        )
            anim.SetTrigger("closeToEnemy");
    }

    public void Attack()
    {
        GameObject[] allTargets;

        if (gameObject.name == "teamLeader")
            allTargets = GameObject.FindGameObjectsWithTag("teamMember");
        else
            allTargets = GameObject.FindGameObjectsWithTag("team2");

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
