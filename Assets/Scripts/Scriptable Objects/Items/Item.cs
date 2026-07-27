using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string id;
    public string description;
    public int health;
    public int tool;
    public Sprite icon;
    public GameObject prefab;
}
