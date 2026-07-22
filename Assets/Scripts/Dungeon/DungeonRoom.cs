using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class WorldChunk : MonoBehaviour
{
    [SerializeField] protected WorldGenerator worldGen;
    public List<GameObject> roomExits;

    public PlayerController player;
    public Chunk chunk;

    private Vector3 cameraPosOffset;

    // Start is called before the first frame update
    void Awake()
    {
        worldGen = GetComponentInParent<WorldGenerator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (roomExits.Count != 0)
        {
            for (int i = 0; i < roomExits.Count; i++)
            {
                worldGen.openExits.Add(roomExits[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
