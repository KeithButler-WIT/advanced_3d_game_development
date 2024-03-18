using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoadData : MonoBehaviour
{
    const int PLAYER_NAME = 0;
    const int PLAYER_POSITION = 1;
    const int PLAYER_SCORE = 2;
    const int PLAYER_LAST_LEVEL = 3;
    const int PLAYER_DIFFICULITY_LEVEL = 4;

    Vector3 playerPosition;
    string playerName;

    public GameObject playerCharacter;

    DataToSave data;
    string jsonText;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("saveData");
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        if (scene.name != "SaveData")
        {
            GameObject t = Instantiate(playerCharacter, playerPosition, Quaternion.identity);
            t.name = playerName;
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveDataJson();
        if (Input.GetKeyDown(KeyCode.L))
            LoadDataJson();
    }

    void SaveData4()
    {
        string multipleData = "John|10,10,2|100*Mary|20,20,1|300";
        File.WriteAllText(Application.dataPath + "/gameData.txt", "" + multipleData);
    }

    void LoadData4()
    {
        print("Reading Data");
        string dataToRead = File.ReadAllText(Application.dataPath + "/gameData.txt");
        string newData,
            playerName,
            coordinates;
        float x,
            y,
            z;
        int score;
        for (int i = 0; i < 2; i++)
        {
            newData = dataToRead.Split("*")[i];

            playerName = newData.Split("|")[PLAYER_NAME];
            coordinates = newData.Split("|")[PLAYER_POSITION];
            score = int.Parse(newData.Split("|")[PLAYER_SCORE]);

            x = float.Parse(coordinates.Split(",")[0]);
            y = float.Parse(coordinates.Split(",")[1]);
            z = float.Parse(coordinates.Split(",")[2]);
            Vector3 newPosition = new Vector3(x, y, z);

            print(
                "Data" + i + "name=" + playerName + "\nPosition=" + newPosition + "score=" + score
            );

            GameObject t = Instantiate(playerCharacter, newPosition, Quaternion.identity);
            t.name = playerName;
            print("Current Score = " + score);
        }
    }

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

    void SaveDataJson()
    {
        data = new DataToSave
        {
            name = "John",
            score = 100,
            position = new Vector3(10, 0, 10),
            lastLevel = "Week6Level2"
        };
        jsonText = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/gameData.json", jsonText);
        Debug.Log(jsonText);
    }

    void LoadDataJson()
    {
        string dataToRead = File.ReadAllText(Application.dataPath + "/gameData.json");
        DataToSave savedData = JsonUtility.FromJson<DataToSave>(dataToRead);
        Debug.Log(
            "Loaded Data: Name = "
                + savedData.name
                + " score = "
                + savedData.score
                + " position = "
                + savedData.position
                + " lastlevel = "
                + savedData.lastLevel
        );
        // playerName = saveData.name;
        // playerPosition = saveData.position;
        SceneManager.LoadScene("Week6Level2");
    }

    private class DataToSave
    {
        public string name;
        public int score;
        public Vector3 position;
        public string lastLevel;
    }
}
