using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    List<Item> playerInventory;
    int currentInventoryIndex = 0;
    bool isVisible = false;

    GameObject inventoryPanel,
        inventoryText,
        inventoryDescription,
        inventoryImage;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = new List<Item>();
        // playerInventory.Add(new Item(Item.ItemType.MEAT));
        playerInventory.Add(new Item(Item.ItemType.GOLD));

        inventoryPanel = GameObject.Find("inventoryPanel");
        inventoryText = GameObject.Find("inventoryText");
        inventoryDescription = GameObject.Find("inventoryDescription");
        inventoryImage = GameObject.Find("inventoryImage");

        for (int i = 0; i < playerInventory.Count; i++)
        {
            print(playerInventory[i].Info());
        }
        isVisible = false;
        DisplayUI(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible)
        {
            DisplayUI(true);
            Item currentItem = playerInventory[currentInventoryIndex];
            GameObject.Find("inventoryText").GetComponent<TextMeshProUGUI>().text =
                currentItem.name + "[" + currentItem.nb + "]";
            GameObject.Find("inventoryDescription").GetComponent<TextMeshProUGUI>().text =
                currentItem.description;
            GameObject.Find("inventoryImage").GetComponent<RawImage>().texture =
                currentItem.GetTexture();

            if (Input.GetKeyDown(KeyCode.I))
                currentInventoryIndex++;
            if (currentInventoryIndex >= playerInventory.Count)
            {
                currentInventoryIndex = 0;
                isVisible = false;
                DisplayUI(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
                isVisible = true;
        }
    }

    void DisplayUI(bool toggle)
    {
        inventoryPanel.SetActive(toggle);
        inventoryText.SetActive(toggle);
        inventoryDescription.SetActive(toggle);
        inventoryImage.SetActive(toggle);
    }

    public bool UpdateItem(Item.ItemType type, int nbItemsToAdd)
    {
        bool foundSimilarItem = false;
        for (int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory[i].type == type)
            {
                if (playerInventory[i].nb + nbItemsToAdd <= playerInventory[i].maxNB)
                {
                    playerInventory[i].nb += nbItemsToAdd;
                    foundSimilarItem = true;
                    break;
                }
                else
                {
                    return false;
                }
            }
        }
        if (!foundSimilarItem)
        {
            playerInventory.Add(new Item(type));
            playerInventory[playerInventory.Count - 1].nb = nbItemsToAdd;
        }
        return true;
    }

    public void AddPurchasedItems(List<Item> purchasedItems)
    {
        bool t;
        for (int i = 0; i < purchasedItems.Count; i++)
        {
            if (purchasedItems[i].nb > 0)
                UpdateItem(purchasedItems[i].type, purchasedItems[i].nb);
        }
    }

    public int GetMoney()
    {
        for (int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory[i].type == Item.ItemType.GOLD)
                return playerInventory[i].nb;
        }
        return 0;
    }

    public void SetMoney(int newAmount)
    {
        for (int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory[i].type == Item.ItemType.GOLD)
                playerInventory[i].nb = newAmount;
        }
    }

    public void CheckInventory2() {
        string fullMessage = "";
        for (int i = 0; i < playerInventory.Count; i++)
        {
            fullMessage += "" + playerInventory[i].nb + " " + playerInventory[i].name;
        }
        print(fullMessage);
    }
}
