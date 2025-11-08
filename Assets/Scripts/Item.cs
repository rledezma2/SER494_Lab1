using UnityEngine;

public enum ItemType { HealthPack, Book, Treasure, Pickaxe }

[System.Serializable]
public class Item
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;

    public Item(string name, ItemType type, Sprite sprite)
    {
        itemName = name;
        itemType = type;
        icon = sprite;
    }
}
