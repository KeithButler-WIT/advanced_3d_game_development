using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDynamicGameplay : MonoBehaviour
{
    GameObject[] allNpcs,
        allXBridges,
        allZBridges;
    public float npcSpeed,
        npcHearingRadius;

    [SerializeField]
    [Range(1, 3)]
    int currentDifficultyLevel;

    float HPCheckFrequency,
        HPCheckTimer,
        checkUserProgressTimer = 0,
        checkUserProgressTimerPeriod;
    bool userIsHighlySkilled;
    public int userSkillLevel = 5;
    float bridgeSize = 5f;

    public int npcSpawnPeriod;
    public int healthPackSpawnPeriod;

    public void ChangeXBridgeSize()
    {
        allXBridges = GameObject.FindGameObjectsWithTag("xBridge");
        for (int i = 0; i < allXBridges.Length; i++)
        {
            allXBridges[i].transform.localScale = new Vector3(
                allXBridges[i].transform.localScale.x,
                allXBridges[i].transform.localScale.y,
                bridgeSize
            );
        }
    }

    public void ChangeZBridgeSize()
    {
        allZBridges = GameObject.FindGameObjectsWithTag("zBridge");
        for (int i = 0; i < allZBridges.Length; i++)
        {
            allZBridges[i].transform.localScale = new Vector3(
                bridgeSize,
                allZBridges[i].transform.localScale.y,
                allZBridges[i].transform.localScale.z
            );
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        checkUserProgressTimerPeriod = 10;
        ChangeDifficulty();
    }

    void ChangeDifficulty()
    {
        print("Current Level " + currentDifficultyLevel);
        switch (currentDifficultyLevel)
        {
            case 1:
                npcSpeed = 2.5f;
                npcHearingRadius = 3f;
                bridgeSize = 10f;
                npcSpawnPeriod = 60;
                healthPackSpawnPeriod = 5;
                break;
            case 2:
                npcSpeed = 4.5f;
                npcHearingRadius = 7f;
                bridgeSize = 5f;
                npcSpawnPeriod = 30;
                healthPackSpawnPeriod = 20;
                break;
            case 3:
                npcSpeed = 6.5f;
                npcHearingRadius = 10f;
                bridgeSize = 1.5f;
                npcSpawnPeriod = 10;
                healthPackSpawnPeriod = 30;
                break;
            default:
                break;
        }

        changeNPCSpeed();
        changeNPCHearingRadius();
        ChangeZBridgeSize();
        ChangeXBridgeSize();
        ChangeNPCSpawnPeriod();
        ChangeHealthPacksSpawnPeriod();
    }

    void ChangeHealthPacksSpawnPeriod()
    {
        GameObject.Find("spawnHealthPacks").GetComponent<SpawnHealthPacks>().HPCheckPeriod =
            healthPackSpawnPeriod;
    }

    void ChangeNPCSpawnPeriod()
    {
        GameObject.Find("spawnNPCs").GetComponent<SpawnNPCs>().spawnPeriod = npcSpawnPeriod;
    }

    void CheckUserProgress()
    {
        if (userSkillLevel < 3)
            currentDifficultyLevel = 1;
        else if (userSkillLevel < 7)
            currentDifficultyLevel = 2;
        else
            currentDifficultyLevel = 3;
        ChangeDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        checkUserProgressTimer += Time.deltaTime;
        if (checkUserProgressTimer >= checkUserProgressTimerPeriod)
        {
            checkUserProgressTimer = 0;
            CheckUserProgress();
        }

        if (Input.GetKeyDown(KeyCode.P)) // increase
        {
            npcSpeed = 5.5f;
            changeNPCSpeed();
            npcHearingRadius = 10f;
            changeNPCHearingRadius();
        }
        if (Input.GetKeyDown(KeyCode.O)) // decrease
        {
            npcSpeed = 2.0f;
            changeNPCSpeed();
            npcHearingRadius = 2f;
            changeNPCHearingRadius();
        }
    }

    public void ChangePlayerSkillLevel(int delta, string cause)
    {
        userSkillLevel += delta;
        if (userSkillLevel < 1)
            userSkillLevel = 1;
        else if (userSkillLevel > 10)
            userSkillLevel = 10;

        print("Player level adjusted level adjusted to " + userSkillLevel + " due to " + cause);
    }

    void changeNPCSpeed()
    {
        allNpcs = GameObject.FindGameObjectsWithTag("npc");
        for (int i = 0; i < allNpcs.Length; i++)
        {
            allNpcs[i].GetComponent<ControlNPCDynamic>().ChangeSpeed(npcSpeed);
        }
    }

    void changeNPCHearingRadius()
    {
        allNpcs = GameObject.FindGameObjectsWithTag("npc");
        for (int i = 0; i < allNpcs.Length; i++)
        {
            allNpcs[i].GetComponent<ControlNPCDynamic>().ChangeHearingDistance(npcHearingRadius);
        }
    }
}
