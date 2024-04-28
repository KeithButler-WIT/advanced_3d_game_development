using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveShop(){  
        GameObject.Find("Player").GetComponent<InventorySystem>().CheckInventory2();
        List<Item> purchasedItems = new List<Item>();
        purchasedItems = GameObject.Find("shopSystem").GetComponent<ShopSystem>().shopItems;
        int moneyLeft = GameObject.Find("shopSystem").GetComponent<ShopSystem>().moneyLeft;
        
        GameObject.Find("player").GetComponent<InventorySystem>().SetMoney(moneyLeft);
        GameObject.Find("player").GetComponent<InventorySystem>().AddPurchasedItems(purchasedItems);
        GameObject.Find("player").GetComponent<ControlPlayerShopSystem>().shopIsDisplayed = false;

        GameObject.Find("player").transform.position -= GameObject.Find("player").transform.forward * 4;
        GameObject.Find("player").transform.Rotate(0,180,0);

        GameObject.Find("Player").GetComponent<InventorySystem>().CheckInventory2();
    }
}
