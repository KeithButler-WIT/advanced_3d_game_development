using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoadData : MonoBehaviour
{
    Vector3 playerPosition;
    string playerName;
    int playerHealth;

    public GameObject playerCharacter;

    DataToSave data;
    string jsonText;
    string saveFile = "/Saves/gameData.json";

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
            // t.name = playerName;
            t.GetComponent<Player>().health = playerHealth;
            // t.GetComponent<Player>().score = playerScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveDataJson();
        if (Input.GetKeyDown(KeyCode.L))
            LoadDataJson();
    }

    private class DataToSave
    {
        public string name;
        public int score;
        public Vector3 position;
        public string lastLevel;
        // public int health;
    }

    void SaveDataJson()
    {
        playerCharacter = GameObject.Find("Player");
        data = new DataToSave
        {
            name = playerCharacter.name,
            score = 100,
            position = playerCharacter.transform.position,
            lastLevel = "Level01"
            // health = 
        };
        jsonText = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + saveFile, jsonText);
        Debug.Log(jsonText);
    }

    void LoadDataJson()
    {
        string dataToRead = File.ReadAllText(Application.dataPath + saveFile);
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
                // + " health = "
                // + savedData.health
        );
        // playerName = savedData.name;
        // playerPosition = savedData.position;
        // playerCharacter.name = savedData.name;
        // playerName = savedData.name;
        playerPosition = savedData.position;
        SceneManager.LoadScene("Scenes/Level 01");
    }

}
