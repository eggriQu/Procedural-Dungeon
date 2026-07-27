using UnityEngine;

public class TreeObject : BaseObject
{
    public override void OnBreak()
    {
        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            Instantiate(item.prefab, transform.position, Quaternion.identity, GameManager.instance.levelItems.transform);
        }
        base.OnBreak();
    }
}
