using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private WorldGenerator worldGen;
    [SerializeField] private Transform parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObjects()
    {
        for (int i = 0; i < worldGen.chunkList.Count; i++)
        {
            GameObject chunkObj = worldGen.chunkList[i];
            Chunk currentChunk = chunkObj.GetComponent<WorldChunk>().chunk;
            for (int j = 0; j < 4; j++)
            {
                float xRange = Random.Range(-currentChunk.spawnRange.x, currentChunk.spawnRange.x);
                float zRange = Random.Range(-currentChunk.spawnRange.y, currentChunk.spawnRange.y);
                Vector3 chunkPos = chunkObj.transform.position;
                Vector3 spawnPos = chunkPos + new Vector3(xRange, 0, zRange);

                GameObject resource = currentChunk.resources[Random.Range(currentChunk.elementFirst, currentChunk.elementLast)];
                Instantiate(resource, spawnPos, new Quaternion(), parent);
            }
        }
    }
}
