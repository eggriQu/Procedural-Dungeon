using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGen;
    [SerializeField] private DungeonRoom room;
    [SerializeField] private Rigidbody rb;
    public PlayerController player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        room = GetComponentInParent<DungeonRoom>();
        dungeonGen.allRoomsSpawned.AddListener(Open);
        dungeonGen.allRoomsSpawned.AddListener(DisableRigidbody);
    }

    // Update is called once per frame
    void Update()
    {
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (player.currentRoom.roomClear)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
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
