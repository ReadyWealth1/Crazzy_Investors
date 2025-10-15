using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerTutorial : MonoBehaviour
{
    public GameObject[] EasytilePrefabs; // Array to hold easy tile prefabs

    private Vector3 nextSpawnPoint;
    private int tileCounter = 0; // Counter to keep track of the number of tiles spawned
    private Queue<GameObject> activeTiles = new Queue<GameObject>(); // Queue to keep track of active tiles

    private void Start()
    {
        ResetSpawner();
    }

    public void SpawnTile()
    {
        // Get the tile prefab based on the tileCounter, looping back to the start of the array if needed
        GameObject selectedTile = EasytilePrefabs[tileCounter % EasytilePrefabs.Length];

        // Instantiate the selected tile prefab at the next spawn point
        GameObject temp = Instantiate(selectedTile, nextSpawnPoint, Quaternion.identity);

        // Update the next spawn point to be the end of the current tile
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        // Increment the tile counter
        tileCounter++;

        // Add the new tile to the queue
        activeTiles.Enqueue(temp);

        // If there are more than 3 tiles, destroy the oldest one
        if (activeTiles.Count > 2)
        {
            Destroy(activeTiles.Dequeue());
        }
    }

    public void ResetSpawner()
    {
        // Clear the active tiles
        while (activeTiles.Count > 0)
        {
            Destroy(activeTiles.Dequeue());
        }

        // Reset variables
        nextSpawnPoint = Vector3.zero;
        tileCounter = 0;

        // Spawn initial tiles
        for (int i = 0; i < 2; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {

    }
}
