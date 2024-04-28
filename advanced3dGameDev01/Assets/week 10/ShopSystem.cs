using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public List<Item> shopItems;
    public GameObject shopItemComponent;
    GameObject[] shopItemComponents;
    int totalPurchase = 0;
    int initialMoney;
    public int moneyLeft;
    float topLeftX,
        topLeftY;

    public bool CanAddItemsToCart(int index)
    {
        if (moneyLeft >= shopItems[index].price && shopItems[index].nb < shopItems[index].maxNB)
            return true;
        else
            return false;
    }

    public void UpdateTotal(int index, int itemAmount)
    {
        shopItems[index].nb = itemAmount;
        int tempTotal = CalculateTotal();
        GameObject.Find("shopTotalValue").GetComponent<TextMeshProUGUI>().text = "" + tempTotal;
        totalPurchase = tempTotal;
        moneyLeft = initialMoney - tempTotal;
        GameObject.Find("shopMoneyLeftValue").GetComponent<TextMeshProUGUI>().text = "" + moneyLeft;
    }

    public int CalculateTotal()
    {
        int total = 0;
        for (int i = 0; i < shopItems.Count; i++)
        {
            total += shopItems[i].nb * shopItems[i].price;
        }
        return total;
    }

    void setupShopItemComponent(int index)
    {
        shopItems[index].nb = 0;
        shopItemComponents[index] = Instantiate(
            shopItemComponent,
            transform.position,
            Quaternion.identity
        );
        shopItemComponents[index].GetComponent<ShopItem>().index = index;

        float width = shopItemComponents[index]
            .transform.Find("itemBg")
            .GetComponent<RectTransform>()
            .sizeDelta.x;
        float height = shopItemComponents[index]
            .transform.Find("itemBg")
            .GetComponent<RectTransform>()
            .sizeDelta.y;
        float borderAroundEachItem = 1.05f;

        shopItemComponents[index].name = "shopItem_" + index + shopItems[index].name;
        shopItemComponents[index].transform.Find("itemLabel").GetComponent<TextMeshProUGUI>().text =
            shopItems[index].name + "($" + shopItems[index].price + ")";
        shopItemComponents[index].transform.Find("itemQty").GetComponent<TextMeshProUGUI>().text =
            "" + shopItems[index].nb;
        shopItemComponents[index].transform.parent = GameObject.Find("shopUI").transform;
        shopItemComponents[index].transform.localPosition = new Vector3(
            -width + topLeftX + (index % 3) * (width * borderAroundEachItem),
            -30 + height + topLeftY - (index / 3) + (width * borderAroundEachItem),
            0.0f
        );

        shopItemComponents[index].transform.Find("itemImage").GetComponent<RawImage>().texture =
            shopItems[index].GetTexture();
    }

    public void Init()
    {
        initialMoney = 1000;
        moneyLeft = initialMoney;
        topLeftX = 50;
        topLeftY = 300;

        shopItems = new List<Item>();
        // shopItems.Add(new Item(Item.ItemType.YELLOW_DIAMOND));
        // shopItems.Add(new Item(Item.ItemType.BLUE_DIAMOND));
        // shopItems.Add(new Item(Item.ItemType.RED_DIAMOND));
        // shopItems.Add(new Item(Item.ItemType.MEAT));
        shopItems.Add(new Item(Item.ItemType.APPLE));

        shopItemComponents = new GameObject[shopItems.Count];
        GameObject.Find("shopMoneyLeftValue").GetComponent<TextMeshProUGUI>().text =
            "" + initialMoney;
        for (int i = 0; i < shopItems.Count; i++)
        {
            setupShopItemComponent(i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update() { }
}
