using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Type02 : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anim;
    AnimatorStateInfo info;

    [Range(0, 100)]
    public int health;

    float healthTimer = 0;
    GameObject[] healthPacks;
    GameObject target;

    float distanceToClosestPack;
    int rankOfClosestPack;
    float distanceToCurrentPack;

    Vector3 initialPosition;

    public enum Type
    {
        Flee,
        Ambush
    };

    public Type npcType;
    public GameObject player;
    float minimumDistanceBetweenPlayerAndNpc = 20;
    Vector3 towardsPlayer;

    public GameObject ambushStart;

    void Start()
    {
        anim = GetComponent<Animator>();
        SetHealth(30);
        initialPosition = transform.position;
        //health = 30;
        //anim.SetInteger("health", health);
    }

    void SetHealth(int newHealth)
    {
        health = newHealth;
        anim.SetInteger("health", health);
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);

        healthTimer += Time.deltaTime;
        if (healthTimer > 2)
        {
            healthTimer = 0;
            SetHealth(health - 2);
            //health -=2;
            //anim.SetInteger("health", health);
        }
        if (info.IsName("GoToAmbushSpot"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            target = ambushStart;
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            GetComponent<NavMeshAgent>().speed = 5.5f;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < 2.5f)
            {
                anim.SetTrigger("reachedAmbush");
            }
        }

        if (info.IsName("BackToStartingPoint"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().SetDestination(initialPosition);
            if (Vector3.Distance(transform.position, initialPosition) < 1)
                anim.SetTrigger("reachedStartingPoint");
        }

        if (info.IsName("LookForHealthPack"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            //GetComponent<NavMeshAgent>().SetDestination (GameObject.Find("healthPack").transform.position);
            healthPacks = (GameObject[])GameObject.FindGameObjectsWithTag("healthPack");

            print("Nb Health Packs: " + healthPacks.Length);
            if (healthPacks.Length == 0)
            {
                anim.SetBool("healthPackAvailable", false);
            }
            else
            {
                anim.SetBool("healthPackAvailable", true);
                SelectHealthPackToCollect2();
            }
            if (target != null)
            {
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                if (Vector3.Distance(transform.position, target.transform.position) < 2)
                {
                    SetHealth(100);
                    Destroy(target);
                }
            }
        }
        switch (npcType)
        {
            case Type.Flee:
            {
                if (
                    Vector3.Distance(transform.position, player.transform.position)
                    < minimumDistanceBetweenPlayerAndNpc
                )
                {
                    anim.SetTrigger("startToFlee");
                    towardsPlayer = (player.transform.position - transform.position).normalized;
                    if (target == null)
                        target = new GameObject();
                    target.transform.position =
                        transform.position - towardsPlayer * minimumDistanceBetweenPlayerAndNpc;
                    GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                    GetComponent<NavMeshAgent>().isStopped = false;
                }
                break;
            }
            case Type.Ambush:
            {
                break;
            }
            default:
            {
                break;
            }
        }
    }

    void SelectHealthPackToCollect()
    {
        print("Select Pack");
        distanceToClosestPack = 1000;
        for (int i = 0; i < healthPacks.Length; i++)
        {
            distanceToCurrentPack = Vector3.Distance(
                transform.position,
                healthPacks[i].transform.position
            );
            if (distanceToCurrentPack < distanceToClosestPack)
            {
                distanceToClosestPack = distanceToCurrentPack;
                rankOfClosestPack = i;
            }
        }

        target = healthPacks[rankOfClosestPack];
    }

    void SelectHealthPackToCollect2()
    {
        print("Select Pack");
        distanceToClosestPack = 1000;
        for (int i = 0; i < healthPacks.Length; i++)
        {
            GetComponent<NavMeshAgent>().SetDestination(healthPacks[i].transform.position);
            distanceToCurrentPack = GetComponent<NavMeshAgent>().remainingDistance;
            //distanceToCurrentPack = Vector3.Distance(transform.position, healthPacks[i].transform.position);
            if (distanceToCurrentPack < distanceToClosestPack)
            {
                distanceToClosestPack = distanceToCurrentPack;
                rankOfClosestPack = i;
            }
        }

        target = healthPacks[rankOfClosestPack];
    }

    public void playerEnteredAmbushArea()
    {
        anim.SetTrigger("goToAmbush");
    }

}
