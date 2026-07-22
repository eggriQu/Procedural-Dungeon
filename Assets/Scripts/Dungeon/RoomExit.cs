using UnityEngine;

public class RoomExit : MonoBehaviour
{
    [SerializeField] private WorldChunk worldChunk;
    [SerializeField] private WorldGenerator worldGen;

    void Awake()
    {
        worldChunk = GetComponentInParent<WorldChunk>();
        worldGen = GameObject.Find("World Generator").GetComponent<WorldGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Room"))
        {
            worldChunk.roomExits.Remove(gameObject);
            worldGen.openExits.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
