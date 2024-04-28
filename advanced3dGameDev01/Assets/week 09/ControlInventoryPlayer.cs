using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlInventoryPlayer : MonoBehaviour
{
    GameObject objectToPickUp;
    bool itemToPickUpNearBy = false;
    GameObject userMessage;

    // Start is called before the first frame update
    void Start()
    {
        userMessage = GameObject.Find("userMessage");
        userMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (itemToPickUpNearBy)
        {
            if (Input.GetKeyDown(KeyCode.Y))
                pickUpObject1();
            if (Input.GetKeyDown(KeyCode.N))
            {
                GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = "";
                userMessage.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("Trigger hit");
        if (other.tag == "itemToBeCollected")
        {
            print("Object to be collected is present");
            objectToPickUp = other.gameObject;
            itemToPickUpNearBy = true;
            // pickUpObject1();
            pickUpObject2();
        }
    }

    void OnTriggerExit(Collider other)
    {
        itemToPickUpNearBy = false;
        if (userMessage.active)
        {
            GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = "";
            userMessage.SetActive(false);
        }
    }

    void pickUpObject1()
    {
        if (
            GetComponent<InventorySystem>()
                .UpdateItem(objectToPickUp.GetComponent<ObjectToBeCollected>().type, 1)
        )
        {
            Destroy(objectToPickUp);
            itemToPickUpNearBy = false;
            GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = "";
            userMessage.SetActive(true);
        }
        else
        {
            string message = "You can can't collect this item as you have reached the maximum";
            GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = message;
        }
    }

    void pickUpObject2()
    {
        string article = objectToPickUp.GetComponent<ObjectToBeCollected>().item.article;
        string message =
            "You just found "
            + article
            + " "
            + objectToPickUp.GetComponent<ObjectToBeCollected>().item.name;
        message += " \n Collect (Y/N)";
        userMessage.SetActive(true);
        GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = message;
    }
}
