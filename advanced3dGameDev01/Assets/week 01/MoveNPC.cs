using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNPC : MonoBehaviour
{
    public GameObject target;
    public GameObject[] wayPoints;

    public enum Type
    {
        Follow,
        Path,
        RandomPath,
        Wandering
    };

    public Type npcType;
    int WPCount = 0;
    float timer;
    public GameObject wanderingTarget;
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
                target = GameObject.Find("Target");
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);

                if (Vector3.Distance(transform.position, target.transform.position) < 1)
                    anim.SetTrigger("stopWalking");
                else
                    anim.SetTrigger("startWalking");

                break;
            }
            case Type.Path:
            {
                target = wayPoints[WPCount];
                if (Vector3.Distance(transform.position, target.transform.position) < 1)
                {
                    WPCount++;
                    if (WPCount > wayPoints.Length - 1)
                        WPCount = 0;
                }
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                break;
            }
            case Type.RandomPath:
            {
                int previous = WPCount;
                int random = 0;
                if (Vector3.Distance(transform.position, target.transform.position) < 1)
                {
                    do
                    {
                        random = Random.Range(0, wayPoints.Length - 1);
                    } while (random == previous);
                    WPCount = random;
                    target = wayPoints[WPCount];
                }
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                break;
            }
            case Type.Wandering:
            {
                timer += Time.deltaTime;
                if (timer > 4)
                {
                    timer = 0;
                    Ray ray = new Ray();
                    RaycastHit hit;
                    ray.origin = transform.position + Vector3.up * 0.7f;
                    float distanceToObstacle = 0;
                    float castingDistance = 20;
                    float random = 0.0f;
                    do
                    {
                        float randomDirectionX = Random.Range(-0.5f, 0.5f);
                        float randomDirectionZ = Random.Range(-0.5f, 1.0f);

                        ray.direction =
                            transform.forward * randomDirectionZ
                            + transform.right * randomDirectionX;

                        Debug.DrawRay(ray.origin, ray.direction, Color.red);

                        if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
                        {
                            distanceToObstacle = hit.distance;
                        }
                        else
                        {
                            // distanceToObstacle = castingDistance;
                            // wanderingTarget.transform.position = ray.origin + ray.direction * (distanceToObstacle)
                            target = wanderingTarget;
                        }
                    } while (distanceToObstacle < 1.0f);
                }

                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                break;
            }
            default:
                break;
        }
    }
}
