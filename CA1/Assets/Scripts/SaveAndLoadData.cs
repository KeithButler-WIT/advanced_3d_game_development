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

    private class DataToSave
    {
        public string name;
        public int score;
        public Vector3 position;
        public string lastLevel;
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

}
