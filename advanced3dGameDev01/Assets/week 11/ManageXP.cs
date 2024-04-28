using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ManageXP : MonoBehaviour
{
    float initialXP,initialPower,initialAccuracy,initialCommunication;
    float currentXP,currentPower,currentAccuracy,currentCommunication;
    float previousXP,previousPower,previousAccuracy,previousCommunication;

    GameObject powerSlider,accuracySlider,communicationSlider,initalXPTextUI;
    float deltaPower,deltaAccuracy,deltaCommunication,deltaXP;

    // Start is called before the first frame update
    void Start()
    {
        initialPower = 40;
        initialAccuracy = 40;
        initialCommunication = 40;
        initialXP = 50;
        currentPower = initialPower;
        currentAccuracy = currentAccuracy;
        currentCommunication = currentCommunication;
        // currentXP = initialXP;

        powerSlider = GameObject.Find("powerSlider");
        accuracySlider = GameObject.Find("accuracySlider");
        communicationSlider = GameObject.Find("communicationSlider");

        initalXPTextUI = GameObject.Find("xpGained");
        initalXPTextUI.GetComponent<TextMeshProUGUI>().text = "" + initialXP;

        powerSlider.GetComponent<Slider>().minValue = initialPower;
        powerSlider.GetComponent<Slider>().maxValue = initialPower + initialXP;

        accuracySlider.GetComponent<Slider>().minValue = initialAccuracy;
        accuracySlider.GetComponent<Slider>().maxValue = initialAccuracy + initialXP;

        communicationSlider.GetComponent<Slider>().minValue = initialCommunication;
        communicationSlider.GetComponent<Slider>().maxValue = initialCommunication + initialXP;
        
    }

    void SaveCurrentValues() {
        previousPower = currentPower;
        previousAccuracy = currentAccuracy;
        previousCommunication = currentCommunication;
    }

    void GetNewValues() {
        currentPower = powerSlider.GetComponent<Slider>().value;
        currentAccuracy = accuracySlider.GetComponent<Slider>().value;
        currentCommunication = communicationSlider.GetComponent<Slider>().value;
    }

    void CalculateDeltas() {
        deltaPower = currentPower - initialPower;
        deltaAccuracy = currentAccuracy - initialAccuracy;
        deltaCommunication = currentCommunication - initialCommunication;
        deltaXP = deltaAccuracy + deltaCommunication + deltaPower;
        GameObject.Find("xpGained").GetComponent<TextMeshProUGUI>().text = "" + (int) (initialXP - deltaXP);
    }

    public void SetXP() {
        string currentXPAsText = initalXPTextUI.GetComponent<TextMeshProUGUI>().text;
        currentXP = Int32.Parse(currentXPAsText);
        SaveCurrentValues();
        GetNewValues();
        if (currentXP == 0) {
            if (currentPower > previousPower) currentPower = currentPower;
            if (currentAccuracy > currentAccuracy) currentAccuracy = currentAccuracy;
            if (currentCommunication > previousCommunication) currentCommunication = currentCommunication;
        }
        CalculateDeltas();
        GameObject.Find("powerLabel").GetComponent<TextMeshProUGUI>().text = ""+(int)currentPower;
        GameObject.Find("accuracyLabel").GetComponent<TextMeshProUGUI>().text = ""+(int)currentAccuracy;
        GameObject.Find("communicationLabel").GetComponent<TextMeshProUGUI>().text = ""+(int)currentCommunication;
    }
}
