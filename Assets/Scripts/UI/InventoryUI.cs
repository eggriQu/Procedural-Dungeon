using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject uiItemPrefab;

    [Header("References")]
    [SerializeField] Inventory inventory;
    [SerializeField] Transform uiInventoryParent;

    [Header("State")]
    [SerializeField] SerializedDictionary<Item, int> inventoryUI = new();
    [SerializeField] SerializedDictionary<Item, TextMeshProUGUI> itemQuantites = new();

    public void AddUIItem(string inventoryId, Item item)
    {
        if (inventoryUI.ContainsKey(item))
        {
            inventoryUI[item] += 1;
            itemQuantites[item].SetText("x" + inventoryUI[item]);
        }
        else
        {
            var itemUI = Instantiate(uiItemPrefab).GetComponent<ItemUI>();
            itemQuantites.Add(item, itemUI.quantityText);

            itemUI.transform.SetParent(uiInventoryParent);
            inventoryUI.Add(item, 1);
            itemQuantites[item].SetText("x" + inventoryUI[item]);
            itemUI.Initialize(inventoryId, item, inventory.DropItem);
        }
    }

    public void DropUIItem(Item item)
    {
        inventoryUI[item] -= 1;
        itemQuantites[item].SetText("x" + inventoryUI[item]);
    }

    public void RemoveUIItem(Item item)
    {
        inventoryUI.Remove(item);
        itemQuantites.Remove(item);
    }

    public bool CheckForKey(Item item)
    {
        return inventoryUI.ContainsKey(item);
    }
}
