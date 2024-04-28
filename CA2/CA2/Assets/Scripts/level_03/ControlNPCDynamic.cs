using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlNPCDynamic : MonoBehaviour
{
    public float speed = 3.5f;
    public float hearingDistance = 3.0f;

    Animator anim;
    AnimatorStateInfo info;

    GameObject[] WPs;
    GameObject player;
    int WPIndex = 0;

    public void ChangeHearingDistance(float newHearingDistance)
    {
        hearingDistance = newHearingDistance;
        transform.GetChild(0).gameObject.transform.localScale = new Vector3(
            hearingDistance,
            1,
            hearingDistance
        );
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
        // transform.GetChild(0).gameObject.transform.localScale = new Vector3(hearingDistance, 1 hearingDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().speed = speed;
        anim = GetComponent<Animator>();
        WPs = GameObject.FindGameObjectsWithTag("WP");
        player = GameObject.Find("Player");
        hearingDistance = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        Listen();
        if (info.IsName("Chase"))
        {
            GetComponent<NavMeshAgent>().speed = speed;
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            GetComponent<NavMeshAgent>().isStopped = false;
        }
        if (info.IsName("Idle"))
        {
            // GetComponent<NavMeshAgent>().speed = speed;
            // GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            GetComponent<NavMeshAgent>().isStopped = false;
        }
        if (info.IsName("Patrol"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            if (Vector3.Distance(transform.position, WPs[WPIndex].transform.position) < 2)
                WPIndex++;
            if (WPIndex > WPs.Length - 1)
                WPIndex = 0;
            // GetComponent<NavMeshAgent>().speed = speed;
            GetComponent<NavMeshAgent>()
                .SetDestination(WPs[WPIndex].transform.position);
            // GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    void Listen()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) < hearingDistance)
            anim.SetBool("canHearPlayer", true);
        else
            anim.SetBool("canHearPlayer", false);
    }

}
