using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomTypes;
    [SerializeField] private GameObject masterRoom;
    public List<GameObject> openExits;
    [SerializeField] private int roomCount;

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
            int selectedExit = Random.Range(0, openExits.Count);
            Instantiate(roomTypes[selectedRoom], openExits[selectedExit].transform.position, openExits[selectedExit].transform.rotation, gameObject.transform);
            openExits.Remove(openExits[selectedExit]);
            roomCount--;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
