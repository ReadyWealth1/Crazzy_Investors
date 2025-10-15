using UnityEngine;

public class Reset_Manager : MonoBehaviour
{
    public GroundSpawnerTest groundSpawner;
    public Character character;
    public Assets assets;

    public void ResetGame()
    {
        // Reset the ground spawner
        groundSpawner.ResetSpawner();

        // Reset the character
        character.ResetCharacter();
        assets.ResetAsset();

        // Add other reset logic here if necessary
    }
}
