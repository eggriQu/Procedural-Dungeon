using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Room Objects")]
    [SerializeField] private List<GameObject> roomTypes;
    [SerializeField] private GameObject endRoom;
    public GameObject masterRoom;
    public GameObject exitWall;
    public GameObject doorObject;

    [Header("Room Gen Variables")]
    public List<GameObject> openExits;
    public GameObject selectedExitPoint;
    [SerializeField] private int roomCount;
    public bool roomGenComplete;
    public float roomGenTime;

    void Start()
    {
        Instantiate(masterRoom, transform);
        StartCoroutine(SpawnRooms());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRooms()
    {
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
