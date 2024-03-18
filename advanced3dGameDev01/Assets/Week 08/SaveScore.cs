using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveScore : MonoBehaviour
{
    string playerName;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_InputField>().onEndEdit.AddListener(SaveTheScore);
        score = 10000;
    }

    // Update is called once per frame
    void Update() { }

    void SaveTheScore(string textInField)
    {
        print("Starting to save score for user " + textInField);
        playerName = textInField;
        StartCoroutine(ConnectToPHP());
    }

    IEnumerator ConnectToPHP()
    {
        string url = "https://advanced3dgamedev-keithbutler.000webhostapp.com/test.php";
        url += "?name=" + playerName + "&score=" + score;
        WWW www = new WWW(url);
        yield return www;
        print("DB updated with " + url);
    }
}
