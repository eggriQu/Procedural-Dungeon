using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] protected DungeonGenerator dungeonGen;
    [SerializeField] private BoxCollider cameraBounds;
    public List<GameObject> roomExits;
    public GameObject respawnPoint;

    public GameObject cameraPrefab;
    public CinemachineCamera roomCamera;
    public CinemachineConfiner3D roomConfiner;

    public PlayerController player;
    public UnityEvent roomEntered;
    public UnityEvent roomExited;
    public bool roomClear;
    public Room room;
    public Chunk chunk;

    private Vector3 cameraPosOffset;

    // Start is called before the first frame update
    void Awake()
    {
        dungeonGen = GetComponentInParent<DungeonGenerator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (roomExits.Count != 0)
        {
            for (int i = 0; i < roomExits.Count; i++)
            {
                dungeonGen.openExits.Add(roomExits[i]);
            }
        }
        
        if (gameObject == dungeonGen.endRoom)
        {
            
        }

        if (cameraPrefab != null)
        {
            cameraPosOffset = new Vector3(transform.position.x - 4.5f, transform.position.y + 6, transform.position.z - 4.5f);
            roomCamera = Instantiate(cameraPrefab, cameraPosOffset, cameraPrefab.transform.rotation).GetComponent<CinemachineCamera>();
            roomConfiner = roomCamera.gameObject.GetComponent<CinemachineConfiner3D>();
            roomConfiner.BoundingVolume = cameraBounds;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && cameraPrefab != null)
        {
            roomCamera.gameObject.SetActive(true);
            if(dungeonGen.roomGenComplete)
            {
                roomEntered.Invoke();
                player.currentRoom = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && cameraPrefab != null)
        {
            roomCamera.gameObject.SetActive(false);
            if (dungeonGen.roomGenComplete)
            {
                roomExited.Invoke();
            }
        }
    }
}
