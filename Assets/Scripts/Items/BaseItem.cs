using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface ILevelObject
{
    void OnClick(PlayerController player);
    void OnHover(PlayerController player);
    void OnExit(PlayerController player);
}

[RequireComponent(typeof(Collider))]
public class BaseItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ILevelObject
{
    [Header("State")]
    [SerializeField] protected float resourceHp;
    public Item item;
    private bool inRange;

    protected PlayerController player;
    protected Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        inventory = player.GetComponent<Inventory>();
    }

    public void Initialize(Item item)
    {
        this.item = item;
        Instantiate(item.prefab, transform.position, Quaternion.identity, GameManager.instance.levelItems.transform);
        Destroy(gameObject);
    }


    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (inRange)
        {
            OnClick(player);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover(player);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit(player);
    }

    public virtual void OnClick(PlayerController player)
    {
        inventory.PickupItem(this);
    }

    public virtual void OnHover(PlayerController player)
    {

    }

    public virtual void OnExit(PlayerController player)
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
