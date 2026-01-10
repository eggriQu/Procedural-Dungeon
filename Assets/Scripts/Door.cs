using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGen;
    [SerializeField] private DungeonRoom dungeonRoom;
    [SerializeField] private Rigidbody rb;
    public PlayerController player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        dungeonRoom = GetComponentInParent<DungeonRoom>();
        dungeonGen.allRoomsSpawned.AddListener(Open);
        dungeonGen.allRoomsSpawned.AddListener(DisableRigidbody);

        //if (dungeonRoom.room.roomType == dungeonGen.endRoom)
        //{
        //    dungeonGen.endDoors.Add(gameObject);
        //}

        if (!enabled)
        {
            enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (player.currentRoom.roomClear)
        {
            transform.position = new Vector3(transform.position.x, 6.5f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Exit Wall"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void Open()
    {
        transform.position = new Vector3(transform.position.x, 5, transform.position.z);
    }

    public void DisableRigidbody()
    {
        rb.isKinematic = true;
    }
}
