using System;
using UnityEngine;

public class ToolUI : ItemUI
{
    public override void Initialize(string inventoryId, Item item, Action<string, Item> leftClickAction, Action<String, Item> rightClickAction)
    {
        image.sprite = item.icon;
        itemObj = item;
        inventoryUI = GameObject.Find("Inventory UI").GetComponent<InventoryUI>();
        transform.localScale = Vector3.one;

        OnLeftClick.AddListener(() => leftClickAction.Invoke(inventoryId, item));
        OnLeftClick.AddListener(OnEquip);
        OnRightClick.AddListener(() => rightClickAction.Invoke(inventoryId, item));
        OnRightClick.AddListener(CheckQuantity);
    }

    protected override void CheckQuantity()
    {
        if (inventoryUI.CheckForKey(itemObj))
        {

        }
        else
        {
            inventoryUI.UnequipItem();
            Destroy(gameObject);
        }
    }

    void OnEquip()
    {
        isEquipped = !isEquipped;
        if (isEquipped)
        {
            equippedSprite.enabled = true;
        }
        else
        {
            equippedSprite.enabled = false;
        }
    }
}
