using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGen;
    public List<GameObject> roomExits;

    // Start is called before the first frame update
    void Awake()
    {
        dungeonGen = GetComponentInParent<DungeonGenerator>();
        for (int i = 0; i < roomExits.Count; i++)
        {
            dungeonGen.openExits.Add(roomExits[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
