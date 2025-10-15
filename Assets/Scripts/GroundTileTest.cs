using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileTest : MonoBehaviour
{
    private GroundSpawnerTest groundSpawner;

    private void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawnerTest>();

        if (groundSpawner == null)
        {
            Debug.LogError("GroundSpawnerTest not found in the scene!");
        }
        if (other.CompareTag("Player"))
        {
            if (groundSpawner != null)
            {
                // Check if we are in tutorial mode and call the appropriate method
                if (GroundSpawnerTest.isTutorialMode)
                {
                    groundSpawner.SpawnTileTutorial();
                }
                else
                {
                    groundSpawner.SpawnTile();
                }
            }

            // Destroy the tile after 2 seconds
            Destroy(gameObject, 2); 
        }
    }
}
