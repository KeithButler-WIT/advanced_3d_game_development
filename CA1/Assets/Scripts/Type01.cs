using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Type01 : NPC
{
    GameObject target;
    GameObject[] wayPoints;

    public enum Type
    {
        Follow,
        Path
    };

    public Type npcType = Type.Path;
    int WPCount = 0;
    float timer = 0;
    public GameObject player;
    float distance;
    GameObject[] allBCs;

    Ray ray;
    RaycastHit hit;
    float castingDistance;

    Vector3 direction;

    float healthTimer = 0;
    float attackTimer;
    public GameObject bullet;
    GameObject clone;
    Rigidbody r;

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

        switch (npcType)
        {
            case Type.Path:
            {
                anim.SetBool("isPatrolling", true);

                Listen();
                Smell();
                Look();

                if (info.IsName("Walking"))
                {
                    target = wayPoints[WPCount];
                    if (Vector3.Distance(transform.position, target.transform.position) < 1)
                    {
                        WPCount++;
                        if (WPCount > wayPoints.Length - 1)
                            WPCount = 0;
                    }
                    GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                }
                if (info.IsName("FollowPlayer"))
                {
                    target = GameObject.Find("player");
                    GetComponent<NavMeshAgent>().isStopped = false;
                    GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                    // gun.Shoot() shoot every 3 seconds
                    Attack(target);
                }
                break;
            }
            default:
                break;
        }
   }

   public void Attack(GameObject t)
    {
        timer += Time.deltaTime;
        if (timer > 3) {
            anim.gameObject.transform.LookAt(GameObject.Find("player").transform.position);
            clone = Instantiate(bullet, anim.rootPosition, Quaternion.identity) as GameObject;
            r = clone.GetComponent<Rigidbody>();
            r.AddForce(anim.gameObject.transform.forward * 1000);
            // anim.SetTrigger("attackOneToOne");
            // anim.SetTrigger("respondToAttack");
            timer = 0;
        }
    }

    void Listen()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 3)
            anim.SetBool("playerDetected", true);
        else
            anim.SetBool("playerDetected", false);
    }

    void Smell()
    {
        allBCs = GameObject.FindGameObjectsWithTag("BC");
        float minDistance = 2;
        bool detectedBC = false;

        for (int i = 0; i < allBCs.Length; i++)
        {
            if (Vector3.Distance(transform.position, allBCs[i].transform.position) < minDistance)
            {
                detectedBC = true;
                break;
            }
        }
        if (detectedBC)
            anim.SetBool("playerDetected", true);
        else
            anim.SetBool("playerDetected", false);
    }

    void Look()
    {
        direction = (GameObject.Find("player").transform.position - transform.position).normalized;
        bool isInFieldOfView = (Vector3.Dot(transform.forward.normalized, direction) > 0.7f);
        Debug.DrawRay(transform.position, direction * 100, Color.green);
        Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
        Debug.DrawRay(transform.position, (transform.forward - transform.right) * 100, Color.red);
        Debug.DrawRay(transform.position, (transform.forward + transform.right) * 100, Color.red);

        Ray ray = new Ray();
        RaycastHit hit;

        ray.origin = transform.position + Vector3.up * 0.7f;
        string objectInSight = "";
        float castingDistance = 20;
        ray.direction = transform.forward * castingDistance;
        Debug.DrawRay(ray.origin, ray.direction * castingDistance, Color.red);

        if (Physics.Raycast(ray.origin, direction, out hit, castingDistance))
        {
            if (objectInSight == "player" || isInFieldOfView)
                anim.SetBool("playerDetected", true);
            else
                anim.SetBool("playerDetected", false);
        }
    }

    void SetHealth(int newValue)
    {
        health = newValue;
        // anim.SetInteger("health", health);
    }

}