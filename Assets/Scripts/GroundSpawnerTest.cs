using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnerTest : MonoBehaviour
{
    public GameObject[] EasytilePrefabs; // Array to hold easy tile prefabs
    public GameObject[] MediumtilePrefabs; // Array to hold medium tile prefabs
    public GameObject[] TutorialTiles; // Array of tutorial tiles to spawn in order

    private Vector3 nextSpawnPoint;
    private int tileCounter = 0; // Counter to keep track of the number of tiles spawned
    private Queue<GameObject> activeTiles = new Queue<GameObject>(); // Queue to keep track of active tiles

    public static bool isTutorialMode = false; // Flag to indicate if tutorial is active

    private void Start()
    {
        ResetSpawner();
    }

    public void SpawnTile()
    {
        // If tutorial mode is active, call SpawnTileTutorial
        if (isTutorialMode)
        {
            // Only spawn tutorial tiles if it's not already transitioning
            if (tileCounter < TutorialTiles.Length)
            {
                SpawnTileTutorial();
                return;
            }
        }

        GameObject[] currentTilePrefabs;

        // Choose the appropriate list based on the tile counter
        if (tileCounter < 5)
        {
            currentTilePrefabs = EasytilePrefabs;
        }
        else
        {
            currentTilePrefabs = MediumtilePrefabs;
        }

        // Randomly select a tile prefab from the appropriate list
        int randomIndex = Random.Range(0, currentTilePrefabs.Length);
        GameObject selectedTile = currentTilePrefabs[randomIndex];

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

    public void SpawnTileTutorial()
    {
        Debug.Log("Spawning tutorial tiles");

        if (tileCounter < TutorialTiles.Length)
        {
            // Spawn the tutorial tile based on the current tileCounter index
            GameObject selectedTile = TutorialTiles[tileCounter];

            // Instantiate the tutorial tile at the next spawn point
            GameObject temp = Instantiate(selectedTile, nextSpawnPoint, Quaternion.identity);

            // Update the next spawn point to the end of the current tile
            nextSpawnPoint = temp.transform.GetChild(1).transform.position;

            // Increment the tile counter
            tileCounter++;

            // Add the new tile to the queue
            activeTiles.Enqueue(temp);

            // Ensure only 3 tiles are active at any time
            if (activeTiles.Count > 2)
            {
                Destroy(activeTiles.Dequeue());
            }
        }
        else
        {
            // Transition to normal spawning after completing tutorial
            // Debug.Log("Tutorial sequence completed. Switching to normal mode.");
            isTutorialMode = false;
            PlayerPrefs.SetInt("FirstTimeOpen", 0);
            Debug.Log("Tutorial Disable");
            // Spawn one additional tile using the normal mode
            SpawnTile();
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
}