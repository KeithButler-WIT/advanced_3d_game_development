using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageShopItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void IncreaseQuantity()
    {
        transform.parent.GetComponent<ShopItem>().IncreaseQuantity();
    }

    public void DecreaseQuantity()
    {
        transform.parent.GetComponent<ShopItem>().DecreaseQuantity();
    }
}
