using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string id;
    public string description;
    public int health;
    public Item tool;
    public bool equippable;
    public Sprite icon;
    public Sprite inGameSprite;
    public GameObject prefab;
}
