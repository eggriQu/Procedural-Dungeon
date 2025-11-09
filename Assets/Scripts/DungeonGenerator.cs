using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomTypes;
    [SerializeField] private GameObject masterRoom;
    public List<GameObject> openExits;
    [SerializeField] private int roomCount;
    public bool roomGenComplete;
    public GameObject selectedExitPoint;

    void Start()
    {
        Instantiate(masterRoom, gameObject.transform);
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
            Instantiate(roomTypes[selectedRoom], selectedExitPoint.transform.position, selectedExitPoint.transform.rotation, gameObject.transform);
            openExits.Remove(selectedExitPoint);
            roomCount--;
            yield return new WaitForSeconds(0.05f);
        }
        roomGenComplete = true;
    }

    void SelectExit()
    {
        int selectedExit = Random.Range(0, openExits.Count);
        selectedExitPoint = openExits[selectedExit];
    }
}
