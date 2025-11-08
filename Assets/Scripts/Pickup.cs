using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                Item newItem = new Item(itemName, itemType, icon);
                if (inventory.AddItem(newItem))
                {
                    GetComponent<Collider>().enabled = false; 
                    Destroy(gameObject);
                }
            }
        }
    }
}
