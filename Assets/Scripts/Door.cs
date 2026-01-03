using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGen;
    [SerializeField] private DungeonRoom room;
    [SerializeField] private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
        room = GetComponentInParent<DungeonRoom>();
        dungeonGen.allRoomsSpawned.AddListener(Open);
        dungeonGen.allRoomsSpawned.AddListener(DisableRigidbody);
        room.roomEntered.AddListener(Close);
        room.roomCleared.AddListener(Open);
    }

    // Update is called once per frame
    void Update()
    {
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
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
        if (room.roomClear)
        {
            StartCoroutine(OpenCoroutine());
        }
    }

    public void Close()
    {
        if (!room.roomClear)
        {
            StartCoroutine(CloseCoroutine());
        }
    }

    public void DisableRigidbody()
    {
        rb.isKinematic = true;
    }

    IEnumerator OpenCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Translate(Vector3.up * 2.5f);
    }

    IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Translate(Vector3.down * 2.5f);
    }
}
