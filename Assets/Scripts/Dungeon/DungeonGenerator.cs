using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Room Objects")]
    [SerializeField] private List<GameObject> roomTypes;
    public List<GameObject> chunkList;
    public GameObject endRoom;
    public GameObject masterRoom;
    public GameObject exitWall;
    public GameObject treeObject;
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
        StartCoroutine(SpawnChunks());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnChunks()
    {
        //Instantiate(masterRoom, gameObject.transform);
        while (roomCount > 0)
        {
            int selectedRoom = Random.Range(0, roomTypes.Count);
            SelectExit();
            GameObject newChunk = Instantiate(roomTypes[selectedRoom], selectedExitPoint.transform.position, selectedExitPoint.transform.rotation, gameObject.transform);
            chunkList.Add(newChunk);
            openExits.Remove(selectedExitPoint);
            roomCount--;
            yield return new WaitForSeconds(roomGenTime);
        }
        StartCoroutine(SpawnEndChunks());
        roomGenComplete = true;
    }

    IEnumerator SpawnEndChunks()
    {
        while (openExits.Count > 0)
        {
            SelectExit();
            GameObject newChunk = Instantiate(endRoom, selectedExitPoint.transform.position, selectedExitPoint.transform.rotation, gameObject.transform);
            chunkList.Add(newChunk);
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
}
