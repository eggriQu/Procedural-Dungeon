using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemUI : Selectable, IPointerClickHandler
{
    [Header("Click Events")]
    public UnityEvent OnLeftClick;
    public UnityEvent OnRightClick;

    [Header("References")]
    [SerializeField] protected Item itemObj;
    [SerializeField] protected InventoryUI inventoryUI;
    [SerializeField] protected Image equippedSprite;
    [SerializeField] protected bool isEquipped;
    public TextMeshProUGUI quantityText;

    public virtual void Initialize(string inventoryId, Item item, Action<String, Item> leftClickAction, Action<String, Item> rightClickAction)
    {
        image.sprite = item.icon;
        itemObj = item;
        inventoryUI = GameObject.Find("Inventory UI").GetComponent<InventoryUI>();
        transform.localScale = Vector3.one;

        OnLeftClick.AddListener(() => leftClickAction.Invoke(inventoryId, item));
        OnRightClick.AddListener(() => rightClickAction.Invoke(inventoryId, item));
        OnRightClick.AddListener(CheckQuantity);
    }

    protected virtual void CheckQuantity()
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
        OnLeftClick.RemoveAllListeners();
        OnRightClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DoStateTransition(SelectionState.Pressed, true);

        switch (eventData.button)
        {
            default:
            case PointerEventData.InputButton.Left:
                OnLeftClick?.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                OnRightClick?.Invoke();
                break;
        }
    }
}
