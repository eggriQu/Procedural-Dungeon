using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[RequireComponent(typeof(Button))]
public class ItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image image;
    [SerializeField] Button button;
    [SerializeField] Item itemObj;
    [SerializeField] InventoryUI inventoryUI;
    public TextMeshProUGUI quantityText;

    public void Initialize(string inventoryId, Item item, Action<String, Item> removeItemAction)
    {
        image.sprite = item.icon;
        itemObj = item;
        inventoryUI = GameObject.Find("Inventory UI").GetComponent<InventoryUI>();
        transform.localScale = Vector3.one;
        button.onClick.AddListener(() => removeItemAction.Invoke(inventoryId, item));
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
