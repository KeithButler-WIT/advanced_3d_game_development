using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Type01 : MonoBehaviour
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
    public Animator anim;
    AnimatorStateInfo info;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() != null)
            anim = GetComponent<Animator>();
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i].GetComponent<Renderer>().enabled = false;
        }
        wayPoints = GameObject.FindGameObjectsWithTag("wp");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>() != null)
            info = anim.GetCurrentAnimatorStateInfo(0);

        switch (npcType)
        {
            case Type.Follow:
            {
                target = GameObject.Find("player");
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);

                if (Vector3.Distance(transform.position, target.transform.position) < 1)
                    anim.SetBool("isPatrolling", false);
                else
                    anim.SetBool("isPatrolling", true);

                break;
            }
            case Type.Path:
            {
                Listen();
                Smell();
                Look2();

                if (info.IsName("Walking"))
                {
                    print(wayPoints.Length);
                    print(WPCount);
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
                    GetComponent<NavMeshAgent>().isStopped = false;
                    GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                }
                break;
            }
            default:
                break;
        }
   }

}


//     Animator anim;
//     AnimatorStateInfo info;
//     public GameObject player;
//     float distance;
//     GameObject[] allBCs;

//     Ray ray;
//     RaycastHit hit;
//     float castingDistance;

//     Vector3 direction;

//     // Start is called before the first frame update
//     void Start()
//     {
//         anim = GetComponent<Animator>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         info = anim.GetCurrentAnimatorStateInfo(0);
//         Listen();
//         Smell();
//         Look2();
//         if (info.IsName("followPlayer"))
//         {
//             GetComponent<NavMeshAgent>().isStopped = false;
//             GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
//         }
//     }

//     void Listen()
//     {
//         distance = Vector3.Distance(transform.position, player.transform.position);
//         if (distance < 3)
//             anim.SetBool("playerDetected", true);
//         else
//             anim.SetBool("playerDetected", false);
//     }

//     void Smell()
//     {
//         allBCs = GameObject.FindGameObjectsWithTag("BC");
//         float minDistance = 2;
//         bool detectedBC = false;

//         for (int i = 0; i < allBCs.Length; i++)
//         {
//             if (Vector3.Distance(transform.position, allBCs[i].transform.position) < minDistance)
//             {
//                 detectedBC = true;
//                 break;
//             }
//         }
//         if (detectedBC)
//             anim.SetBool("playerDetected", true);
//         else
//             anim.SetBool("playerDetected", false);
//     }

//     void Look()
//     {
//         ray = new Ray();
//         ray.origin = transform.position + Vector3.up * 0.7f;
//         string objctInSight = "";
//         castingDistance = 20;
//         ray.direction = transform.forward * castingDistance;
//         Debug.DrawRay(ray.origin, ray.direction * castingDistance, Color.red);

//         if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
//         {
//             objctInSight = hit.collider.gameObject.name;
//             if (objctInSight == "Player")
//                 anim.SetBool("playerDetected", true);
//             else
//                 anim.SetBool("playerDetected", false);
//         }
//     }

//     void Look2()
//     {
//         direction = (GameObject.Find("Player").transform.position - transform.position).normalized;
//         bool isInFieldOfView = (Vector3.Dot(transform.forward.normalized, direction) > 0.7f);
//         Debug.DrawRay(transform.position, direction * 100, Color.green);
//         Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
//         Debug.DrawRay(transform.position, (transform.forward - transform.right) * 100, Color.red);
//         Debug.DrawRay(transform.position, (transform.forward + transform.right) * 100, Color.red);

//         Ray ray = new Ray();
//         RaycastHit hit;

//         ray.origin = transform.position + Vector3.up * 0.7f;
//         string objectInSight = "";
//         float castingDistance = 20;
//         ray.direction = transform.forward * castingDistance;
//         Debug.DrawRay(ray.origin, ray.direction * castingDistance, Color.red);

//         if (Physics.Raycast(ray.origin, direction, out hit, castingDistance))
//         {
//             if (objectInSight == "Player" || isInFieldOfView)
//                 anim.SetBool("playerDetected", true);
//             else
//                 anim.SetBool("playerDetected", false);
//         }
//     }