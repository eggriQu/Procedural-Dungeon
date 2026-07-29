using UnityEngine;

public class BaseObject : BaseItem
{
    [SerializeField] protected float objectHp;
    [SerializeField] protected Item requiredTool;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        inventory = player.GetComponent<Inventory>();

        objectHp = item.health;
        requiredTool = item.tool;
    }

    public override void OnClick(PlayerController player)
    {
        if (player.inventory.currentlyEquipped == requiredTool)
        {
            objectHp -= 5;
        }

        if (objectHp < 1)
        {
            OnBreak();
        }
    }

    public virtual void OnBreak()
    {
        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            Instantiate(item.prefab, transform.position, Quaternion.identity, GameManager.instance.levelItems.transform);
        }
        Destroy(gameObject);
    }
}
