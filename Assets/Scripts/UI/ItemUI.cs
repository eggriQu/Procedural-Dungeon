using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[RequireComponent(typeof(Button))]
public class ItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Image image;
    [SerializeField] protected Button button;
    [SerializeField] protected Item itemObj;
    [SerializeField] protected InventoryUI inventoryUI;
    [SerializeField] protected Image equippedSprite;
    public TextMeshProUGUI quantityText;

    public virtual void Initialize(string inventoryId, Item item, Action<String, Item> leftClickAction)
    {
        image.sprite = item.icon;
        itemObj = item;
        inventoryUI = GameObject.Find("Inventory UI").GetComponent<InventoryUI>();
        transform.localScale = Vector3.one;
        button.onClick.AddListener(() => leftClickAction.Invoke(inventoryId, item));
        button.onClick.AddListener(CheckQuantity);
    }

    void CheckQuantity()
    {
        if (inventoryUI.CheckForKey(itemObj))
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Destroy()
    {
        button.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
