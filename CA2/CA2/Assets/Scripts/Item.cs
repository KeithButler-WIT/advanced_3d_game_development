using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        APPLE,
        MEAT,
        GOLD,
        RED_DIAMOND,
        BLUE_DIAMOND,
        YELLOW_DIAMOND,
        SWORD,
        BATON
    }

    public enum ItemFamilyType
    {
        FOOD,
        LOOT,
        WEAPON
    }

    public string name,
        description;
    public int price,
        healthBenefits,
        damage,
        nb,
        maxNB;
    public ItemType type;
    public ItemFamilyType familyType;
    public string article;

    public Texture GetTexture()
    {
        Texture2D tx;
        if (this.familyType == Item.ItemFamilyType.WEAPON)
            return (Resources.Load<Texture2D>("weapons/" + this.name.Replace(" ", "_")));
        else if (this.familyType == Item.ItemFamilyType.FOOD)
            return (Resources.Load<Texture2D>("food/" + this.name.Replace(" ", "_")));
        else if (this.familyType == Item.ItemFamilyType.LOOT)
            return (Resources.Load<Texture2D>("loot/" + this.name.Replace(" ", "_")));
        else
            return null;
    }

    public string Info()
    {
        string info =
            "Name = "
            + this.name
            + ", Health Benefits = "
            + this.healthBenefits
            + ", Damage = "
            + this.damage
            + ", nb: "
            + this.nb;
        return info;
    }

    public Item(ItemType type)
    {
        switch (type)
        {
            case ItemType.APPLE:
                name = "Apple";
                price = 50;
                healthBenefits = 10;
                damage = 0;
                nb = 1;
                maxNB = 5;
                description = "An apple";
                familyType = ItemFamilyType.FOOD;
                article = "an";
                break;
            case ItemType.MEAT:
                name = "Meat";
                price = 50;
                healthBenefits = 30;
                damage = 0;
                nb = 1;
                maxNB = 2;
                description = "Some meat";
                familyType = ItemFamilyType.FOOD;
                article = "some";
                break;
            case ItemType.GOLD:
                name = "Gold";
                price = 100;
                healthBenefits = 0;
                damage = 0;
                nb = 1;
                maxNB = 20;
                description = "A gold ingot";
                familyType = ItemFamilyType.LOOT;
                article = "g";
                break;
            case ItemType.RED_DIAMOND:
                name = "Red diamond";
                price = 250;
                healthBenefits = 0;
                damage = 0;
                nb = 1;
                maxNB = 10;
                description = "A valuable diamond";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;
            case ItemType.BLUE_DIAMOND:
                name = "Blue diamond";
                price = 500;
                healthBenefits = 0;
                damage = 0;
                nb = 1;
                maxNB = 10;
                description = "A valuable diamond";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;
            case ItemType.YELLOW_DIAMOND:
                name = "Yellow diamond";
                price = 1000;
                healthBenefits = 0;
                damage = 0;
                nb = 1;
                maxNB = 10;
                description = "A valuable diamond";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;
            case ItemType.SWORD:
                name = "Sword";
                price = 100;
                healthBenefits = 0;
                damage = 10;
                nb = 1;
                maxNB = 1;
                description = "A powerful sword";
                familyType = ItemFamilyType.WEAPON;
                article = "a";
                break;
            case ItemType.BATON:
                name = "Baton";
                price = 80;
                healthBenefits = 0;
                damage = 5;
                nb = 1;
                maxNB = 1;
                description = "A powerful baton";
                familyType = ItemFamilyType.WEAPON;
                article = "a";
                break;
        }
    }
}
