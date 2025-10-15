using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject[] EasytilePrefabs; // Array to hold easy tile prefabs
    public GameObject[] MediumtilePrefabs; // Array to hold medium tile prefabs

    private Vector3 nextSpawnPoint;
    private int tileCounter = 0; // Counter to keep track of the number of tiles spawned

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
