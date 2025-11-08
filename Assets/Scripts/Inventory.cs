using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxItems = 5;
    private List<Item> items = new List<Item>();

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedCallback;

    public bool AddItem(Item newItem)
    {
        if (newItem.itemType == ItemType.Book && items.Exists(i => i.itemType == ItemType.Book))
        {
            Debug.Log("You already have a Book item!");
            return false;
        }

        if (items.Count >= maxItems)
        {
            Debug.Log("Inventory full!");
            return false;
        }

        items.Add(newItem);
        onInventoryChangedCallback?.Invoke();
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        onInventoryChangedCallback?.Invoke();
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public bool HasItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.itemName.Equals(itemName, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItemByName(string itemName)
    {
        Item itemToRemove = items.Find(i => i.itemName.Equals(itemName, System.StringComparison.OrdinalIgnoreCase));
        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
            onInventoryChangedCallback?.Invoke();
        }
    }
}
