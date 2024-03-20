using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeamMember : MonoBehaviour
{
    public GameObject leader;
    Animator anim;
    AnimatorStateInfo info;
    float distanceToLeader,
        distanceToTarget;
    GameObject target;
    int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // leader = GameObject.Find("player");
        if (gameObject.tag == "teamMember")
            leader = GameObject.Find("player");
        else
            leader = GameObject.Find("teamLeader");
    }

    // Update is called once per frame
    void Update()
    {
        if (!leader) GameObject.Find("teamLeader");

        info = anim.GetCurrentAnimatorStateInfo(0);
        distanceToLeader = Vector3.Distance(transform.position, leader.transform.position);
        if (distanceToLeader < 5)
            anim.SetBool("closeToLeader", true);
        else
            anim.SetBool("closeToLeader", false);

        if (info.IsName("Idle"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (info.IsName("MoveTowardsLeader"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().SetDestination(leader.transform.position);
        }
        if (info.IsName("GoToTarget"))
        {
            if (target != null)
            {
                GetComponent<NavMeshAgent>().isStopped = false;
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
                if (distanceToTarget < 1.5)
                {
                    anim.SetBool("closeToTarget", true);
                    GetComponent<NavMeshAgent>().isStopped = true;
                }
                else
                    anim.SetBool("closeToTarget", false);
            }
            else
            {
                anim.SetBool("targetDestroyed", true);
            }
        }
        if (info.IsName("AttackTarget"))
        {
            if (target != null)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                gameObject.transform.LookAt(target.transform);
                if (info.normalizedTime % 1.0 >= .98)
                {
                    if (gameObject.tag == "team2")
                        damage = 10;
                    else
                        damage = 20;
                    target.GetComponent<NPC>().HitByOpponent(gameObject, damage);
                }
            }
            else
            {
                anim.SetBool("targetDestroyed", true);
            }
        }
    }

    public void Attack(GameObject t)
    {
        target = t;
        // anim.SetTrigger("attackOneToOne");
        anim.SetTrigger("respondToAttack");
    }

    public void Retreat()
    {
        anim.SetTrigger("retreat");
    }
}
