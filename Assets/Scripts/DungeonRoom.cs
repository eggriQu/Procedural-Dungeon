using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGen;
    [SerializeField] private BoxCollider cameraBounds;
    public List<GameObject> roomExits;

    public GameObject cameraPrefab;
    public CinemachineCamera roomCamera;
    public CinemachineConfiner3D roomConfiner;

    private Vector3 cameraPosOffset;

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

        cameraPosOffset = new Vector3(transform.position.x - 6, transform.position.y + 5.5f, transform.position.z - 6);
        roomCamera = Instantiate(cameraPrefab, cameraPosOffset, cameraPrefab.transform.rotation).GetComponent<CinemachineCamera>();
        roomConfiner = roomCamera.gameObject.GetComponent<CinemachineConfiner3D>();
        roomConfiner.BoundingVolume = cameraBounds;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            roomCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            roomCamera.gameObject.SetActive(false);
        }
    }
}
