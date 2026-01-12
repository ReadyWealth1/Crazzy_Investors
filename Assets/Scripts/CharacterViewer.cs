using UnityEngine;
using UnityEngine.UI;

public class CharacterViewer : MonoBehaviour
{
    [Header("Highlight Object")]
    public GameObject thumbnailHighlighter;  // The object we want to move

    [Header("Thumbnail Highlighter Positions")]
    public GameObject ElonHighlighterPosition;
    public GameObject OfficeGirlHighlighterPosition;

    [Header("Character Images")]
    public Image ElonCharacterImage;      // UI Image component for Elon
    public Image OfficeGirlCharacterImage; // UI Image component for Office Girl

    [Header("Thumbnail Tick Objects")]
    public GameObject OfficeGirlTick;
    public GameObject ElonTick;

    public GameStartManager gameStartManager;

    // Method to show/hide character images
    private void ShowCharacterByKey(string characterKey)
    {
        // Disable all character images first
        ElonCharacterImage.gameObject.SetActive(false);
        OfficeGirlCharacterImage.gameObject.SetActive(false);

        // Show the selected character image and move highlighter
        switch (characterKey)
        {
            case "Elon":
                ElonCharacterImage.gameObject.SetActive(true);
                thumbnailHighlighter.transform.position = ElonHighlighterPosition.transform.position;
                break;

            case "OfficeGirl":
                OfficeGirlCharacterImage.gameObject.SetActive(true);
                thumbnailHighlighter.transform.position = OfficeGirlHighlighterPosition.transform.position;
                break;

            default:
                Debug.LogWarning($"Unknown character key: {characterKey}");
                // Default to Elon if unknown character
                ElonCharacterImage.gameObject.SetActive(true);
                thumbnailHighlighter.transform.position = ElonHighlighterPosition.transform.position;
                break;
        }
    }

    // Called by each thumbnail button
    public void OnThumbnailClicked(string characterKey)
    {
        ShowCharacterByKey(characterKey);
    }

    // Called by your "Exit" button to restore the truly selected character visually
    public void ViewSelectedCharacter()
    {
        string selectedKey = gameStartManager.GetCurrentlySelectedCharacter();
        ShowCharacterByKey(selectedKey);

        // Ensure the UI reflects the currently selected character
        if (gameStartManager.characterDisplayNames.TryGetValue(selectedKey, out string displayName))
        {
            gameStartManager.selectedCharacterNameText.text = displayName;
        }

        if (gameStartManager.characterCosts.TryGetValue(selectedKey, out int cost))
        {
            gameStartManager.currentViewedCharacter = selectedKey;
            gameStartManager.currentViewedCharacterCost = cost;
            gameStartManager.RefreshBigActionButton();
        }
    }

    public void RefreshThumbnailTicks()
    {
        // Elon (check if owned)
        bool ElonOwned = gameStartManager.IsCharacterBought("Elon");
        ElonTick.SetActive(ElonOwned);

        // Office Girl
        bool OfficeGirlOwned = gameStartManager.IsCharacterBought("OfficeGirl");
        OfficeGirlTick.SetActive(OfficeGirlOwned);
    }

    // Optional: Initialize with the currently selected character on start
    private void Start()
    {
        ViewSelectedCharacter();
    }
}