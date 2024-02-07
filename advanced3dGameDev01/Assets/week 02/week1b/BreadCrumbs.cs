using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadCrumbs : MonoBehaviour
{
    Vector3 previousPosition,
        currentPosition;
    float counter = 0;
    float distance;
    public GameObject BC,
        g;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DropBreadCrumb();
    }

    void DropBreadCrumb()
    {
        currentPosition = transform.position;
        distance = Vector3.Distance(previousPosition, currentPosition);
        if (distance > 1)
        {
            previousPosition = currentPosition;
            g = Instantiate(BC, currentPosition, Quaternion.identity);
            g.name = "BC" + counter;
            counter++;
        }
    }
}
