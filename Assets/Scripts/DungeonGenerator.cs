using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomTypes;
    public List<GameObject> openExits;

    void Start()
    {
        Instantiate(roomTypes[0], gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnNextRoom();
        }
    }

    void SpawnNextRoom()
    {
        int selectedExit = Random.Range(0, roomTypes.Count-1);
        //Instantiate(roomTypes[0], openExits[selectedExit].transform);
        Instantiate(roomTypes[0], openExits[selectedExit].transform.position, openExits[selectedExit].transform.rotation, gameObject.transform);
        openExits.Remove(openExits[selectedExit]);
    }
}
