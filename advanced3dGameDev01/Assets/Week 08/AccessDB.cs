using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccessDB : MonoBehaviour
{
    string url = "https://advanced3dgamedev-keithbutler.000webhostapp.com/high_scores.php";

    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        string result = www.text;
        // print("data recieved " + result);
        GameObject.Find("high_scores").GetComponent<TextMeshProUGUI>().text = result;
    }

    // Update is called once per frame
    void Update() { }
}
