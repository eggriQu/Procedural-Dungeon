using Unity.VisualScripting;
using UnityEngine;

public interface ISpawner
{
    GameObject Spawn(GameObject[] caller, ObjectType type);
}

public enum ObjectType
{
    Hazard,
    Enemies
}

public abstract class SpawnerBase : MonoBehaviour, ISpawner
{
   [SerializeField] protected GameObject[] prefabs;

    public virtual GameObject Spawn(GameObject[] caller, ObjectType type)
    {
        return Instantiate(caller[0]);
    }
}

public class ObjectSpawner : SpawnerBase
{
    [SerializeField] private Room room;

    public override GameObject Spawn(GameObject[] caller, ObjectType type)
    {
        switch(type)
        {
            case ObjectType.Hazard:
                return Instantiate(caller[Random.Range(room.hazardElementFirst, room.hazardElementLast + 1)], transform);
            case ObjectType.Enemies:
                return Instantiate(caller[Random.Range(room.enemiesElementFirst, room.enemiesElementLast + 1)], transform);
        }
        return null;
    }

    void Awake()
    {
        prefabs = room.hazards;
        int spawnDecider = Random.Range(0, 2);
        if (spawnDecider == 0)
        {
            Spawn(prefabs, ObjectType.Hazard);
        }
        else if (spawnDecider == 1)
        {
            Spawn(prefabs, ObjectType.Enemies);
        }
    }

    void Update()
    {
        
    }
}
