using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Room Objects")]
    [SerializeField] private List<GameObject> roomTypes;
    public GameObject endRoom;
    public GameObject masterRoom;
    public GameObject exitWall;
    public GameObject doorObject;
    public GameObject cameraObj;
    public PlayerController player;

    [Header("Room Gen Variables")]
    public List<GameObject> openExits;
    public List<GameObject> endDoors;
    public GameObject selectedExitPoint;
    public int roomCount;
    public bool roomGenComplete;
    public float roomGenTime;
    public UnityEvent allRoomsSpawned;

    void Start()
    {
        player.currentRoom = Instantiate(masterRoom, gameObject.transform).GetComponent<DungeonRoom>();
        StartCoroutine(SpawnRooms());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRooms()
    {
        //Instantiate(masterRoom, gameObject.transform);
        while (roomCount > 0)
        {
            int selectedRoom = Random.Range(0, roomTypes.Count);
            SelectExit();
            if (roomCount % 4 == 0) // Every 4 rooms spawn a hallway
            {
                Instantiate(roomTypes[SelectRandomHallway()], selectedExitPoint.transform.position, selectedExitPoint.transform.rotation, gameObject.transform);
            }
            else
            {
                Instantiate(roomTypes[selectedRoom], selectedExitPoint.transform.position, selectedExitPoint.transform.rotation, gameObject.transform);
            }
            openExits.Remove(selectedExitPoint);
            roomCount--;
            yield return new WaitForSeconds(roomGenTime);
        }
        StartCoroutine(SpawnEndRooms());
        roomGenComplete = true;
    }

    IEnumerator SpawnEndRooms()
    {
        while (openExits.Count > 0)
        {
            SelectExit();
            Instantiate(endRoom, selectedExitPoint.transform.position, selectedExitPoint.transform.rotation, gameObject.transform);
            openExits.Remove(selectedExitPoint);
            yield return new WaitForSeconds(roomGenTime);
        }
        allRoomsSpawned.Invoke();
    }

    void SelectExit()
    {
        int selectedExit = Random.Range(0, openExits.Count);
        selectedExitPoint = openExits[selectedExit];
    }

    int SelectRandomHallway()
    {
        return Random.Range(3, 7);
    }
}
