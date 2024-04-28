using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    string name;
    int price,
        quantity;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        quantity = 10;
        UpdateQuantitytLabel();
    }

    // Update is called once per frame
    void Update() { }

    void UpdateQuantitytLabel()
    {
        transform.Find("itemQty").GetComponent<TextMeshProUGUI>().text = "" + quantity;
        GameObject.Find("shopSystem").GetComponent<ShopSystem>().UpdateTotal(index, quantity);
    }

    public void IncreaseQuantity()
    {
        if (!CanClick())
            return;
        quantity++;
        UpdateQuantitytLabel();
    }

    public void DecreaseQuantity()
    {
        if (!CanClick())
            return;
        quantity--;
        if (quantity < 0)
            quantity = 0;
        UpdateQuantitytLabel();
    }

    bool CanClick()
    {
        // return true;
        GameObject shopSystem = GameObject.Find("shopSystem");
        return shopSystem.GetComponent<ShopSystem>().CanAddItemsToCart(this.index);
    }
}
