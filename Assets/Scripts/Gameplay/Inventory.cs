using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InventoryUI ui;
    [SerializeField] AudioSource audioSource;

    [Header("Prefabs")]
    [SerializeField] GameObject droppedItemPrefab;

    [Header("Audio Clips")]
    [SerializeField] AudioClip pickUpItemAudio;
    [SerializeField] AudioClip dropItemAudio;

    [Header("State")]
    public Item currentlyEquipped;
    public int maxInventorySlots;
    public int inventoryCount;
    [SerializeField] SerializedDictionary<string, int> inventory = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupItem(BaseItem droppedItem)
    {
        if (inventoryCount < maxInventorySlots || inventory.ContainsKey(droppedItem.item.id))
        {
            var item = droppedItem;
            AddItem(droppedItem.item);
            //audioSource.PlayOneShot(pickUpItemAudio);
        }
        else
        {
            Debug.Log("You're carrying too many items!");
        }
    }

    void AddItem(Item item)
    {
        var inventoryId = item.id;
        if (inventory.ContainsKey(inventoryId))
        {
            inventory[inventoryId] += 1;
            ui.AddUIItem(inventoryId, item);
        }
        else
        {
            inventory.Add(inventoryId, 1);
            ui.AddUIItem(inventoryId, item);
            inventoryCount = inventory.Count;
        }
    }

    public void DropItem(string inventoryId, Item item)
    {
        if (inventory.ContainsKey(inventoryId) && inventory[inventoryId] > 1)
        {
            var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<BaseItem>();
            droppedItem.Initialize(item);
            inventory[inventoryId] -= 1;
            ui.DropUIItem(item);
        }
        else if (inventory.ContainsKey(inventoryId) && inventory[inventoryId] <= 1)
        {
            var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<BaseItem>();
            droppedItem.Initialize(item);
            inventory.Remove(inventoryId);
            ui.RemoveUIItem(item);
            inventoryCount = inventory.Count;
            //audioSource.PlayOneShot(dropItemAudio);
        }
    }

    public void EquipItem(string inventoryId, Item item)
    {
        currentlyEquipped = item;
        Debug.Log(item.id + " Equipped");
    }

    public bool CheckForKey(string itemId)
    {
        return inventory.ContainsKey(itemId);
    }
}
