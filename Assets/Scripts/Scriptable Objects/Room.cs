using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Room")]
public class Room : ScriptableObject
{
    public GameObject roomType;
    public GameObject[] hazards;
    public int hazardElementFirst, hazardElementLast;
    public int enemiesElementFirst, enemiesElementLast;
}
