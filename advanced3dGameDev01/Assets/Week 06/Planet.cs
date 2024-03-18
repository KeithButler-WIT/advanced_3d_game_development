using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    GameObject sun;
    float distanceToSun = 150f;
    float rotationalSpeed = 10f;
    float orbitalSpeed = 0.2f;
    float angle = 0f;
    float orbitalAngle = 0f;
    float orbitalRotationalSpeed = 20f;
    Color c1 = Color.blue;
    int lengthOfLineRenderer = 100;

    void drawOrbit()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(1.0f, 1.0f);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);

        int i = 0;
        while (i < lengthOfLineRenderer)
        {
            float unitAngle = (float)(2 * 3.14) / lengthOfLineRenderer;
            float currentAngle = (float)unitAngle * i;
            Vector3 pos = new Vector3(
                distanceToSun * Mathf.Cos(currentAngle),
                0,
                distanceToSun * Mathf.Sin(currentAngle)
            );
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sun = GameObject.Find("Sun");
        transform.position = new Vector3(distanceToSun, 0, distanceToSun);
        drawOrbit();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationalSpeed * Time.deltaTime, Space.World);
        float tempx,
            tempy,
            tempz;
        orbitalAngle += Time.deltaTime * orbitalSpeed;

        tempx = sun.transform.position.x + distanceToSun * Mathf.Cos(orbitalAngle);
        tempy = sun.transform.position.y;
        tempz = sun.transform.position.z + distanceToSun * Mathf.Sin(orbitalAngle);

        transform.position = new Vector3(tempx, tempy, tempz);
    }

    public void SetRotationalSpeed(float s)
    {
        rotationalSpeed = s * rotationalSpeed;
    }

    public void SetOrbitSpeed(float os)
    {
        orbitalSpeed = os * orbitalSpeed;
    }

    public void SetDistanceToSun(float d)
    {
        distanceToSun = d * distanceToSun;
    }

    public void SetName(string n)
    {
        name = n;
        transform.Find("label").GetComponent<TextMeshPro>().text = name;
    }

    public void SetRadius(float radius)
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }
}
