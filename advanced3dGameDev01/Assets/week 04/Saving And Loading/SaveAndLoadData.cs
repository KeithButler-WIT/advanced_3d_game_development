using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoadData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveData4();
        if (Input.GetKeyDown(KeyCode.L))
            LoadData4();
    }

    void SaveData4()
    {
        string multipleData = "John|10,10,2|100*Mary|20,20,1|300";
        File.WriteAllText(Application.dataPath + "/gameData.txt", "" + multipleData);
    }

    void LoadData4() { }

    void SaveData3()
    {
        print("Saving Data");
        string multipleData = "John|2*Mary|3";
        File.WriteAllText(Application.dataPath + "/gameData.txt", "" + multipleData);
        print("Saving Complete");
    }

    void LoadData3()
    {
        print("Reading Data");
        string dataToRead = File.ReadAllText(Application.dataPath + "/gameData.txt");
        string newData;
        for (int i = 0; i < 2; i++)
        {
            newData = dataToRead.Split("*")[i];
            print("Data" + i + "name=" + newData.Split("|")[0] + "score=" + newData.Split("|")[1]);
        }
    }

    void SaveData2()
    {
        print("Saving Data");
        int newScore = 30;
        string playersName = "John";
        string[] multipleData = new string[] { "" + newScore, playersName };
        string dataToSave = string.Join("|", multipleData);
        File.WriteAllText(Application.dataPath + "/gameData.txt", "" + dataToSave);
        print("Saving Complete");
    }

    void LoadData2()
    {
        print("Reading Data");
        string dataToRead = File.ReadAllText(Application.dataPath + "/gameData.txt");
        string[] multipleData = dataToRead.Split("|");
        int newScore = int.Parse(multipleData[0]);
        string newName = multipleData[1];
        print("Loaded Data:\tscore = " + newScore + ", name = " + newName);
    }

    void SaveData1()
    {
        print("Saving Data");
        int nbLives = 2;
        File.WriteAllText(Application.dataPath + "/gameData.txt", "" + nbLives);
        print("Saving Complete");
    }

    void LoadData1()
    {
        print("Reading Data");
        string savedData = File.ReadAllText(Application.dataPath + "/gameData.txt");
        print("Loaded Data" + savedData);
    }
}
