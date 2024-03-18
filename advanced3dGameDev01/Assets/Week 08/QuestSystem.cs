using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    int currentStage;
    List<string> actions,
        targets,
        xps;
    List<bool> objectivesAchieved;
    string stage,
        stageTitle,
        stageDescription,
        stageObjectives,
        startingPointForPlayer;

    public enum possibleActions
    {
        do_nothing = 0,
        talk_to = 1,
        acquire_a = 2,
        destroy_one = 3,
        enter_a_place_called = 4
    };

    List<possibleActions> actionsForQuest;

    GameObject stagePanel,
        stageTitleText,
        stageDescriptionText,
        stageObjectivesText;

    bool panelDisplayed = true;

    public GameObject player;

    int nbOjectivesAchieved = 0;
    int nbOjectivesToAchieve = 0;
    int XPAchieved = 0;
    int totalXP = 0;

    public void Notify(possibleActions action, string target)
    {
        print("Notified Action: " + action + " Target: " + target);
        nbOjectivesToAchieve = actionsForQuest.Count;
        for (int i = 0; i < actionsForQuest.Count; i++)
        {
            if (action == actionsForQuest[i] && target == targets[i] && !objectivesAchieved[i])
            {
                nbOjectivesAchieved++;
                XPAchieved += Int32.Parse(xps[i]);
                objectivesAchieved[i] = true;
            }
            if (nbOjectivesAchieved == nbOjectivesToAchieve)
            {
                DisplayMessage("Stage Complete");
                // totalXP = CalculateTotalXPForLevel();
            }
        }
    }

    public void DisplayMessage(string message)
    {
        GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = message;
    }

    public void MovePlayerToStartingPoint()
    {
        GameObject p = Instantiate(player);
        p.name = "Player";
        p.transform.position = GameObject.Find("startingPoint").transform.position;
    }

    public void Init()
    {
        stagePanel = GameObject.Find("stagePanel");
        stageTitleText = GameObject.Find("stageTitle");
        stageDescriptionText = GameObject.Find("stageDescription");
        stageObjectivesText = GameObject.Find("stageObjectives");

        actions = new List<string>();
        targets = new List<string>();
        xps = new List<string>();
        objectivesAchieved = new List<bool>();
        actionsForQuest = new List<possibleActions>();

        LoadQuest2();
    }

    public void DisplayQuestInfo()
    {
        stageTitleText.GetComponent<TextMeshProUGUI>().text = stageTitle;
        stageDescriptionText.GetComponent<TextMeshProUGUI>().text = stageDescription;
        stageObjectivesText.GetComponent<TextMeshProUGUI>().text =
            stageObjectives + "\n Press H to Hide/Displayer this text";
    }

    public void LoadQuest2()
    {
        print("Loading Quest");
        TextAsset textAsset = (TextAsset)Resources.Load("quest");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        stageObjectives = "For this stage, you need to: \n";
        foreach (XmlNode stage in doc.SelectNodes("quest/stage"))
        {
            if (stage.Attributes.GetNamedItem("id").Value == "" + currentStage)
            {
                stageTitle = stage.Attributes.GetNamedItem("name").Value;
                stageDescription = stage.Attributes.GetNamedItem("description").Value;

                foreach (XmlNode results in stage)
                {
                    print("For this stage, you need to: \n");
                    foreach (XmlNode result in results)
                    {
                        string action = result.Attributes.GetNamedItem("action").Value;
                        string target = result.Attributes.GetNamedItem("target").Value;
                        string xp = result.Attributes.GetNamedItem("xp").Value;

                        possibleActions actionForQuest = possibleActions.do_nothing;

                        if (action.IndexOf("Aquire") >= 0)
                            actionForQuest = possibleActions.acquire_a;
                        else if (action.IndexOf("Talk") >= 0)
                            actionForQuest = possibleActions.talk_to;
                        else if (action.IndexOf("Destroy") >= 0)
                            actionForQuest = possibleActions.destroy_one;
                        else if (action.IndexOf("Enter") >= 0)
                            actionForQuest = possibleActions.enter_a_place_called;
                        else
                            actionForQuest = possibleActions.do_nothing;

                        actionsForQuest.Add(actionForQuest);
                        actions.Add(action);
                        targets.Add(target);
                        xps.Add(xp);
                        objectivesAchieved.Add(false);

                        print(action + " " + target + " [" + xp + "XP]");
                        stageObjectives += "\n ->" + action + " " + target + "[" + xp + "XP]";
                    }
                }
            }
        }
    }

    public void LoadQuest()
    {
        print("Loading Quest");
        TextAsset textAsset = (TextAsset)Resources.Load("quest");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        stageObjectives = "For this stage, you need to: \n";
        foreach (XmlNode stage in doc.SelectNodes("quest/stage"))
        {
            if (stage.Attributes.GetNamedItem("id").Value == "" + currentStage)
            {
                stageTitle = stage.Attributes.GetNamedItem("name").Value;
                stageDescription = stage.Attributes.GetNamedItem("description").Value;

                foreach (XmlNode results in stage)
                {
                    print("For this stage, you need to: \n");
                    foreach (XmlNode result in results)
                    {
                        string action = result.Attributes.GetNamedItem("action").Value;
                        string target = result.Attributes.GetNamedItem("target").Value;
                        string xp = result.Attributes.GetNamedItem("xp").Value;

                        actions.Add(action);
                        targets.Add(target);
                        xps.Add(xp);
                        objectivesAchieved.Add(false);

                        print(action + " " + target + " [" + xp + "XP]");
                        stageObjectives += "\n ->" + action + " " + target + "[" + xp + "XP]";
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        MovePlayerToStartingPoint();
        DisplayQuestInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            panelDisplayed = !panelDisplayed;
            stagePanel.SetActive(true);
        }
    }
}
