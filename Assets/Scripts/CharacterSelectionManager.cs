using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject boyCharacter;
    public GameObject girlCharacter;
    
    public void FlyflyflyManage()
    {
        // Check if the boy character exists and is active
        if (boyCharacter != null && boyCharacter.activeSelf)
        {
            Assets.Flyflyfly();
            Assets.IncrementMilestone();
        }
        // Check if the girl character exists and is active
        else if (girlCharacter != null && girlCharacter.activeSelf)
        {
            Assets.Flyflyfly();
            Assets.IncrementMilestone();
        }
        else
        {
            Debug.LogWarning("No active character found!");
        }

        // Deactivate this component to prevent further execution
        this.enabled = false;
    }
}
    