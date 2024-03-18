using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadPlanets : MonoBehaviour
{
    public GameObject planetTemplate;

    void LoadAllPlanets()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("planets");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        foreach (XmlNode planet in doc.SelectNodes("planets/planet"))
        {
            string name,
                diameter,
                distancetoSun,
                rotationPeriod,
                orbitalVelocity;
            name = planet.Attributes.GetNamedItem("name").Value;
            diameter = planet.Attributes.GetNamedItem("diameter").Value;
            distancetoSun = planet.Attributes.GetNamedItem("distancetoSun").Value;
            rotationPeriod = planet.Attributes.GetNamedItem("rotationPeriod").Value;
            orbitalVelocity = planet.Attributes.GetNamedItem("orbitalVelocity").Value;
            Debug.Log("Name of the planet: " + name);

            float diameter2,
                distanceToSun2,
                rotationPeriod2,
                orbitalVelocity2;
            diameter2 = float.Parse(diameter);
            distanceToSun2 = float.Parse(distancetoSun);
            rotationPeriod2 = float.Parse(rotationPeriod);
            orbitalVelocity2 = float.Parse(orbitalVelocity);

            GameObject g = Instantiate(planetTemplate);
            g.GetComponent<Planet>().SetName(name);
            g.GetComponent<Planet>().SetRadius(diameter2);
            g.GetComponent<Planet>().SetDistanceToSun(distanceToSun2);
            g.GetComponent<Planet>().SetRotationalSpeed(1 / rotationPeriod2);
            g.GetComponent<Planet>().SetOrbitSpeed(orbitalVelocity2);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadAllPlanets();
    }

    // Update is called once per frame
    void Update() { }
}
