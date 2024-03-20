using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Type03 : NPC
{
    GameObject target;
    GameObject[] wayPoints;

    int WPCount = 0;
    float timer = 0;
    public GameObject player;
    float distance;
    GameObject[] allBCs;

    Ray ray;
    RaycastHit hit;
    float castingDistance;

    Vector3 direction;

    float attackTimer;
    public GameObject bullet;
    GameObject clone;
    Rigidbody r;

    float healthTimer = 0;
    GameObject[] healthPacks;
    float distanceToClosestPack;
    int rankOfClosestPack;
    float distanceToCurrentPack;

    int ammo;
    GameObject[] ammoPacks;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() != null)
            anim = GetComponent<Animator>();
        wayPoints = GameObject.FindGameObjectsWithTag("wp");
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i].GetComponent<Renderer>().enabled = false;
        }
        SetHealth(100);
        SetAmmo(10);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>() != null)
            info = anim.GetCurrentAnimatorStateInfo(0);

        healthTimer += Time.deltaTime;
        if (healthTimer > 2)
        {
            healthTimer = 0;
            SetHealth(health - 5);
            //health -=2;
            //anim.SetInteger("health", health);
        }

        if (health <= 0) Destroy(gameObject, 0);

        if (info.IsName("FollowPlayer"))
        {
                    target = GameObject.Find("player");
                    GetComponent<NavMeshAgent>().isStopped = false;
                    GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                    // gun.Shoot() shoot every 3 seconds
                    Attack(target);
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
                SelectPackToCollect(healthPacks);
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
        if (info.IsName("LookForAmmoPack"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            //GetComponent<NavMeshAgent>().SetDestination (GameObject.Find("healthPack").transform.position);
            ammoPacks = (GameObject[])GameObject.FindGameObjectsWithTag("ammoPack");

            print("Nb Ammo Packs: " + ammoPacks.Length);
            if (ammoPacks.Length == 0)
            {
                anim.SetBool("ammoPackAvailable", false);
            }
            else
            {
                anim.SetBool("ammoPackAvailable", true);
                SelectPackToCollect(ammoPacks);
            }
            if (target != null)
            {
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                if (Vector3.Distance(transform.position, target.transform.position) < 2)
                {
                    SetAmmo(10);
                    Destroy(target);
                }
            }
        }

   }

   public void Attack(GameObject t)
    {
        if (ammo>0) {
            timer += Time.deltaTime;
            if (timer > 3) {
                anim.gameObject.transform.LookAt(GameObject.Find("player").transform.position);
                clone = Instantiate(bullet, anim.rootPosition, Quaternion.identity) as GameObject;
                r = clone.GetComponent<Rigidbody>();
                r.AddForce(anim.gameObject.transform.forward * 1000);
                // anim.SetTrigger("attackOneToOne");
                // anim.SetTrigger("respondToAttack");
                timer = 0;
                SetAmmo(--ammo);
                print(ammo);
            }
        }
        
    }

    void SelectPackToCollect(GameObject[] packs)
    {
        print("Select Pack");
        distanceToClosestPack = 1000;
        for (int i = 0; i < packs.Length; i++)
        {
            GetComponent<NavMeshAgent>().SetDestination(packs[i].transform.position);
            distanceToCurrentPack = GetComponent<NavMeshAgent>().remainingDistance;
            //distanceToCurrentPack = Vector3.Distance(transform.position, healthPacks[i].transform.position);
            if (distanceToCurrentPack < distanceToClosestPack)
            {
                distanceToClosestPack = distanceToCurrentPack;
                rankOfClosestPack = i;
            }
        }

        target = packs[rankOfClosestPack];
    }

    void SetHealth(int newValue)
    {
        health = newValue;
        anim.SetInteger("health", health);
    }

    void SetAmmo(int newValue)
    {
        ammo = newValue;
        anim.SetInteger("ammo", ammo);
    }


}