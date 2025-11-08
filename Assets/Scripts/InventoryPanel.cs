using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public Inventory inventory;
    public GameObject inventoryPanel;
    public Button toggleButton;
    public List<Button> itemButtons;

    void Start()
    {
        inventory.onInventoryChangedCallback += UpdateUI;
        toggleButton.onClick.AddListener(ToggleInventory);
        UpdateUI();
    }

    void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    void UpdateUI()
    {
        List<Item> items = inventory.GetItems();

        for (int i = 0; i < itemButtons.Count; i++)
        {
            if (i < items.Count)
            {
                Item item = items[i];
                itemButtons[i].image.sprite = item.icon;
                itemButtons[i].onClick.RemoveAllListeners();
                itemButtons[i].onClick.AddListener(() => UseItem(item));
            }
            else
            {
                itemButtons[i].image.sprite = null;
                itemButtons[i].onClick.RemoveAllListeners();
            }
        }
    }

    void UseItem(Item item)
    {
        if (item.itemType == ItemType.HealthPack)
        {
            inventory.GetComponent<Health>()?.Heal(1);
            inventory.RemoveItem(item);
            Debug.Log("You have used your health pack!");


        }
        else if (item.itemType == ItemType.Book)
        {
            Debug.Log("You cannot remove the 'Book of Robots' from your inventory!");
        }
        else
        {
            Debug.Log($"Dropped {item.itemName} from your inventory.");
            inventory.RemoveItem(item);
        }
    }
}
