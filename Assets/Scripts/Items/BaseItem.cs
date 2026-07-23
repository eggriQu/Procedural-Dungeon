using UnityEngine;
using UnityEngine.EventSystems;

public interface ILevelObject
{
    void OnClick(PlayerController player);
    void OnHover(PlayerController player);
    void OnExit(PlayerController player);
}

public class BaseItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ILevelObject
{
    protected PlayerController player;
    [SerializeField] protected Resource resource;
    [SerializeField] protected float resourceHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick(player);
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
}
