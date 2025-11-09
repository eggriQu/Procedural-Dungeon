using UnityEngine;

public class RoomExit : MonoBehaviour
{
    [SerializeField] private DungeonRoom dungeonRoom;
    [SerializeField] private DungeonGenerator dungeonGen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
        dungeonRoom = GetComponentInParent<DungeonRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Room"))
        {
            //Debug.Log("Blah");
            dungeonRoom.roomExits.Remove(gameObject);
            dungeonGen.openExits.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
