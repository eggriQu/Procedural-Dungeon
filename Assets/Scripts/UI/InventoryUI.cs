using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Unity.VisualScripting;

public class InventoryUI : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject uiItemPrefab;

    [Header("References")]
    [SerializeField] Inventory inventory;
    [SerializeField] Transform uiInventoryParent;

    [Header("State")]
    [SerializeField] SerializedDictionary<Item, int> inventoryUI = new();

    public void AddUIItem(string inventoryId, Item item)
    {
        if (inventoryUI.ContainsKey(item))
        {
            inventoryUI[item] += 1;
        }
        else
        {
            var itemUI = Instantiate(uiItemPrefab).GetComponent<ItemUI>();
            itemUI.transform.SetParent(uiInventoryParent);
            inventoryUI.Add(item, 1);
            itemUI.Initialize(inventoryId, item, inventory.DropItem);
        }
    }

    public void DropUIItem(Item item)
    {
        inventoryUI[item] -= 1;
    }

    public void RemoveUIItem(Item item)
    {
        inventoryUI.Remove(item);
    }

    public bool CheckForKey(Item item)
    {
        return inventoryUI.ContainsKey(item);
    }
}
