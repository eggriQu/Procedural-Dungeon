using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGen;
    [SerializeField] private BoxCollider cameraBounds;
    public UnityEvent roomEntered;
    public List<GameObject> roomExits;
    public GameObject cameraPrefab;
    public CinemachineCamera roomCamera;
    public CinemachineConfiner3D roomConfiner;
    private GameManager gameManager;

    private Vector3 cameraPosOffset;

    void Awake()
    {
        dungeonGen = GetComponentInParent<DungeonGenerator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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

        cameraPosOffset = new Vector3(transform.position.x - 11, transform.position.y + 10.5f, transform.position.z - 11);
        roomCamera = Instantiate(cameraPrefab, cameraPosOffset, cameraPrefab.transform.rotation).GetComponent<CinemachineCamera>();
        roomConfiner = roomCamera.gameObject.GetComponent<CinemachineConfiner3D>();
        roomConfiner.BoundingVolume = cameraBounds;
    }

    // Update is called once per frame
    void Update()
    {
        enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            roomEntered.Invoke();
            roomCamera.gameObject.SetActive(true);
            //panTilt.TiltAxis.Value += transform.localRotation.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            roomCamera.gameObject.SetActive(false);
        }
    }

    public void TestMethod()
    {
        Debug.Log("Yessir");
    }
}
