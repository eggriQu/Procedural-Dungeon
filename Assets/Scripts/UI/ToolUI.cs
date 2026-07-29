using System;
using UnityEngine;

public class ToolUI : ItemUI
{
    public override void Initialize(string inventoryId, Item item, Action<string, Item> leftClickAction)
    {
        image.sprite = item.icon;
        itemObj = item;
        inventoryUI = GameObject.Find("Inventory UI").GetComponent<InventoryUI>();
        transform.localScale = Vector3.one;
        button.onClick.AddListener(() => leftClickAction.Invoke(inventoryId, item));
        button.onClick.AddListener(OnEquip);
    }

    void OnEquip()
    {
        equippedSprite.enabled = true;
    }
}
