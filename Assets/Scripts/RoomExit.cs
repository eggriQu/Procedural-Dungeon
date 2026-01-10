using UnityEngine;

public class RoomExit : MonoBehaviour
{
    [SerializeField] private DungeonRoom dungeonRoom;
    [SerializeField] private DungeonGenerator dungeonGen;

    void Awake()
    {
        dungeonRoom = GetComponentInParent<DungeonRoom>();
        dungeonGen = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
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
            dungeonRoom.roomExits.Remove(gameObject);
            dungeonGen.openExits.Remove(gameObject);
            if (!dungeonGen.roomGenComplete && gameObject != dungeonGen.selectedExitPoint)
            {
                Instantiate(dungeonGen.exitWall, gameObject.transform.position, gameObject.transform.rotation, dungeonRoom.transform);
            }
            else if (gameObject == dungeonGen.selectedExitPoint)
            {
                //DungeonRoom otherRoom = other.GetComponent<DungeonRoom>();
                Instantiate(dungeonGen.doorObject, gameObject.transform.position, gameObject.transform.rotation, dungeonRoom.transform);
            }
            Destroy(gameObject);
        }
    }
}
