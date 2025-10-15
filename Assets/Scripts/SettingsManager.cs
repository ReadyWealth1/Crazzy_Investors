using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPage; // The settings page with sliders
    [SerializeField] private Transform[] parentObjects; // Possible parents for the settings page

    private int currentParentIndex = 0; // Track the current parent

    private void Start()
    {
        // Ensure the settings page is correctly parented on start
        settingsPage.transform.SetParent(parentObjects[currentParentIndex], false);
        settingsPage.SetActive(false); // Hide settings page initially
    }

    public void OpenSettings(int parentIndex)
    {
        if (parentIndex < 0 || parentIndex >= parentObjects.Length)
        {
            Debug.LogError("Invalid parent index");
            return;
        }

        // Reparent the settings page to the desired parent
        settingsPage.transform.SetParent(parentObjects[parentIndex], false);
        settingsPage.SetActive(true);
        currentParentIndex = parentIndex;
    }

    public void CloseSettings()
    {
        settingsPage.SetActive(false);
    }
    public void OnOpenSettingsFromStartMenu()
    {
        OpenSettings(0); // Opens settings in main menu context
    }

    public void OnOpenSettingsFromPauseMenu()
    {
        OpenSettings(1); // Opens settings in pause menu context
    }

    public void OnCloseSettings()
    {
        CloseSettings();
    }
}