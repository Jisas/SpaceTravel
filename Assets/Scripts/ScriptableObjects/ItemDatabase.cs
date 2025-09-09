using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items = new List<Item>();

    public Item FindItemInDatabase(int id)
    {
        foreach (Item item in items)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Item
{
    public int id;
    public string spanishName;
    public string englishName;
    [TextArea(5, 5)] public string spanishDescrption;
    [TextArea(5, 5)] public string englishDescrption;
    public bool isStackable;
    public ItemType itemType;    
    public float spawnRate;
    public int sellCost;
    public Vector2 scrollPos;
    public Sprite itemImage;

    public enum ItemType
    {
        CONSUMABLE, ABILITY, MISC, RARE
    }
}