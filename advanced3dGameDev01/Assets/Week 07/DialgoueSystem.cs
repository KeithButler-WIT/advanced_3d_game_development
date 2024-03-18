using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialgoueSystem : MonoBehaviour
{
    string nameOfCharacter;
    Dialogue[] dialogues;
    int nbDialogues;
    int currentDialogueIndex = 0;
    bool waitingForUserInput = false;
    bool diagloueIsActive = false;

    GameObject dialogueBox,
        dialoguePanel;

    public void StartDialogue()
    {
        waitingForUserInput = false;
        diagloueIsActive = true;
    }

    public void DisplayDialogue1()
    {
        print(dialogues[currentDialogueIndex].message);
        print("[A]" + dialogues[currentDialogueIndex].response[0]);
        print("[B]" + dialogues[currentDialogueIndex].response[1]);
    }

    public void DisplayDialogue2(){
        string textToDisplay = "[" + gameObject.name + "]" + dialogues[currentDialogueIndex].message + "\n[A] >" + dialogues[currentDialogueIndex].response[0] + "\n[B] >" + dialogues[currentDialogueIndex].response[1];
        GameObject.Find("dialogueBox").GetComponent<TextMeshProUGUI>().text = textToDisplay;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("dialogueBox");
        dialoguePanel = GameObject.Find("dialoguePanel");
        GameObject.Find("dialogueImage").GetComponent<RawImage>().texture =
            Resources.Load<Texture2D>(gameObject.name) as Texture2D;

        nameOfCharacter = gameObject.name;
        nbDialogues = CalculateNbDiaglogues();
        dialogues = new Dialogue[nbDialogues];

        LoadDialogues();
        StartDialogue();
    }

    public void LoadDialogues()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("dialogues");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        int dialogueIndex = 0;
        foreach (XmlNode character in doc.SelectNodes("dialogues/character"))
        {
            if (character.Attributes.GetNamedItem("name").Value == nameOfCharacter)
            {
                dialogueIndex = 0;
                foreach (XmlNode dialogueFromXml in doc.SelectNodes("dialogues/character/dialogue"))
                {
                    dialogues[dialogueIndex] = new Dialogue();
                    dialogues[dialogueIndex].message = dialogueFromXml
                        .Attributes.GetNamedItem("content")
                        .Value;
                    int choiceIndex = 0;
                    dialogues[dialogueIndex].response = new string[2];
                    dialogues[dialogueIndex].targetForResponse = new int[2];

                    foreach (XmlNode choice in dialogueFromXml)
                    {
                        dialogues[dialogueIndex].response[choiceIndex] = choice
                            .Attributes.GetNamedItem("content")
                            .Value;
                        dialogues[dialogueIndex].targetForResponse[choiceIndex] = int.Parse(
                            choice.Attributes.GetNamedItem("target").Value
                        );
                        choiceIndex++;
                    }
                    dialogueIndex++;

                    // DisplayDialogue1();
                }
            }
        }
    }

    public int CalculateNbDiaglogues()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("dialogues");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        int dialogueIndex = 0;
        foreach (XmlNode character in doc.SelectNodes("dialogues/character"))
        {
            if (character.Attributes.GetNamedItem("name").Value == nameOfCharacter)
            {
                foreach (XmlNode dialogueFromXml in doc.SelectNodes("dialogues/character/dialogue"))
                {
                    dialogueIndex++;
                }
            }
        }
        return dialogueIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (diagloueIsActive)
        {
            if (!waitingForUserInput)
            {
                if (currentDialogueIndex != -1)
                    DisplayDialogue2();
                else
                {
                    diagloueIsActive = false;
                    waitingForUserInput = false;
                    currentDialogueIndex = 0;
                }
                waitingForUserInput = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currentDialogueIndex = dialogues[currentDialogueIndex].targetForResponse[0];
                    waitingForUserInput = false;
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    currentDialogueIndex = dialogues[currentDialogueIndex].targetForResponse[1];
                    waitingForUserInput = false;
                }
            }
        }
    }
}
