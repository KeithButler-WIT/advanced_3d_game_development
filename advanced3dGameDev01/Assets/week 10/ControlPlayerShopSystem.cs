using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerShopSystem : MonoBehaviour
{
    public bool shopIsDisplayed = false;
    GameObject shopUI;

    // Start is called before the first frame update
    void Start()
    {
        shopUI = GameObject.Find("shopUI");
        shopUI.SetActive(false);
    }

    public void DisplayShopUI()
    {
        shopUI.SetActive(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "shopSystem")
        {
            shopIsDisplayed = true;
            DisplayShopUI();
            GameObject.Find("shopSystem").GetComponent<ShopSystem>().Init();
        }
    }

    // Update is called once per frame
    void Update() { }
}
