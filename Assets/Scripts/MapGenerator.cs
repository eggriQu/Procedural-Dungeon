using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGen;
    [SerializeField] private GameObject iconPrefab;
    public GameObject nextRoom;
    private RectTransform currentRect;

    public List<Texture2D> roomIcons;
    public int[,] grid;

    void Awake()
    {
        grid = new int[dungeonGen.roomCount, dungeonGen.roomCount];
        nextRoom = Instantiate(iconPrefab, transform);
        currentRect = nextRoom.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMap(GameObject selectedExit)
    {
        Rect spawnVector = currentRect.rect;
        Quaternion spawnRotation = new Quaternion(0, 0, selectedExit.transform.rotation.y, selectedExit.transform.rotation.w);
        nextRoom = Instantiate(iconPrefab, transform.position, spawnRotation, transform);
        if (selectedExit.transform.rotation.y == 0)
        {
            spawnVector.y = spawnVector.y + 1;
        }
        else if (selectedExit.transform.rotation.y == 90)
        {
            spawnVector.x = spawnVector.x + 1;
        }
        else if (selectedExit.transform.rotation.y == 180)
        {
            spawnVector.y = spawnVector.y  - 1;
        }
        else if (selectedExit.transform.rotation.y == 270)
        {
            spawnVector.x = spawnVector.x - 1;
        }
        //nextRoom = Instantiate(iconPrefab, new Vector3(spawnVector.x, spawnVector.y), spawnRotation, transform);
        currentRect = nextRoom.GetComponent<RectTransform>();
    }
}
