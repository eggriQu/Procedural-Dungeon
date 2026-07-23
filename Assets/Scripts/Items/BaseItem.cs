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
    [Header("Settings")]
    [SerializeField] bool autoStart;
    [SerializeField] float enabledPickupDelay = 3.0f;

    [Header("State")]
    [SerializeField] protected float resourceHp;
    public Item item;
    public bool pickedUp;
    private bool inRange;

    protected PlayerController player;
    protected Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        inventory = player.GetComponent<Inventory>();

        if (autoStart && item == null)
        {
            Initialize(item);
        }
    }

    public void Initialize(Item item)
    {
        this.item = item;
        var droppedItem = Instantiate(item.prefab, transform);
        droppedItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        StartCoroutine(EnablePickup(enabledPickupDelay));
    }

    IEnumerator EnablePickup(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider>().enabled = true;
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
            inventory.PickupItem(this);
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
