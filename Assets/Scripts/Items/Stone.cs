using UnityEngine;

public class Stone : BaseItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnClick(PlayerController player)
    {
        if (player.tool == 1)
        {
            resourceHp -= 5;
        }

        if (resourceHp < 1)
        {
            inventory.PickupItem(this);
            Destroy(gameObject);
        }
    }
}
