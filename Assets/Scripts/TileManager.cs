using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private float zSpawn = 69f;
    private float Spawn = -0.31f;
    private float tileLength = 105.78f;
    public int numberOfTiles = 2;
    public Transform PlayerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }


    int currentIndex = 0;

    void Update()
    {
        if (PlayerTransform.position.z - 40 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(currentIndex);
            DeleteTile();

            // Increment the index and reset it if it's at the end of the array
            currentIndex = (currentIndex + 1) % tilePrefabs.Length;
        }
    }
    public void SpawnTile(int tileIndex)
    {
        float newX = 1.95f;


        GameObject go = Instantiate(tilePrefabs[tileIndex], new Vector3(newX, transform.position.y, transform.position.z + zSpawn), transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
