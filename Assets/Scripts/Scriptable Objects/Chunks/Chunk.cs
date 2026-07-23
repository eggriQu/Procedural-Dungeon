using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Chunk", menuName = "Scriptable Objects/Chunk")]
public class Chunk : ScriptableObject
{
    public GameObject[] resources;
    public int elementFirst, elementLast;
    public float2 spawnRange;
}
