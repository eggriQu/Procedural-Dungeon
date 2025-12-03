using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGen;
    public List<GameObject> roomExits;
    public GameObject cameraPivot;
    public GameObject roomCamera;

    // Start is called before the first frame update
    void Awake()
    {
        dungeonGen = GetComponentInParent<DungeonGenerator>();
        if (roomExits.Count != 0)
        {
            for (int i = 0; i < roomExits.Count; i++)
            {
                dungeonGen.openExits.Add(roomExits[i]);
            }
        }
        
        if (gameObject == dungeonGen.masterRoom)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            roomCamera.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            roomCamera.SetActive(false);
        }
    }
}
