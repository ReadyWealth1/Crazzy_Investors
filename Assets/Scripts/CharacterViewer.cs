using UnityEngine;
using TMPro;

public class CharacterViewer : MonoBehaviour
{
    [Header("Highlight Object")]
    public GameObject thumbnailHighlighter;  // The object we want to move

    [Header("Thumbnail Highlighters")]
    public GameObject boyHighlighterPosition;
    public GameObject girlHighlighterPosition;
    public GameObject newBoyHighlighterPosition;
    public GameObject newGirlHighlighterPosition;
    public GameObject EgyptQueenHighlighterPosition;
    public GameObject WitchHighlighterPosition;
    public GameObject GwenHighlighterPosition;
    public GameObject ElonHighlighterPosition;
    public GameObject MansaHighlighterPosition;
    public GameObject HotbHighlighterPosition;
    public GameObject HotgHighlighterPosition;
    public GameObject MJHighlighterPosition;
    public GameObject ChubbsHighlighterPosition;
    public GameObject OfficeGirlHighlighterPosition;

    [Header("Thumbnails")]
    public GameObject boyTick;
    public GameObject girlTick;
    public GameObject newBoyTick;
    public GameObject newGirlTick;
    public GameObject EgyptQueenTick;
    public GameObject GwenTick;
    public GameObject WitchTick;
    public GameObject ElonTick;
    public GameObject MansaTick;
    public GameObject HotbTick;
    public GameObject HotgTick;
    public GameObject MJTick;
    public GameObject ChubbsTick;
    public GameObject OfficeGirlTick;

    public GameStartManager gameStartManager;

    [Header("Character Models for Viewing")]
    public GameObject boyCharacterModel;
    public GameObject girlCharacterModel;
    public GameObject newBoyCharacterModel;
    public GameObject newGirlCharacterModel;
    public GameObject EgyptQueenCharacterModel;
    public GameObject WitchCharacterModel;
    public GameObject GwenCharacterModel;
    public GameObject ElonCharacterModel;
    public GameObject MansaCharacterModel;
    public GameObject HotbCharacterModel;
    public GameObject HotgCharacterModel;
    public GameObject MJCharacterModel;
    public GameObject ChubbsCharacterModel;
    public GameObject OfficeGirlCharacterModel;

    // Method to actually show/hide models
    private void ShowCharacterByKey(string characterKey)
    {
        // Disable all character models
        boyCharacterModel.SetActive(false);
        girlCharacterModel.SetActive(false);
        newBoyCharacterModel.SetActive(false);
        newGirlCharacterModel.SetActive(false);
        GwenCharacterModel.SetActive(false);
        EgyptQueenCharacterModel.SetActive(false);
        WitchCharacterModel.SetActive(false);
        ElonCharacterModel.SetActive(false);
        MansaCharacterModel.SetActive(false);
        HotbCharacterModel.SetActive(false);
        HotgCharacterModel.SetActive(false);
        MJCharacterModel.SetActive(false);
        ChubbsCharacterModel.SetActive(false);
        OfficeGirlCharacterModel.SetActive(false);

        // Move the highlighter to the correct position
        switch (characterKey)
        {
            case "boy":
                boyCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = boyHighlighterPosition.transform.position;
                break;

            case "girl":
                girlCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = girlHighlighterPosition.transform.position;
                break;

            case "newBoy":
                newBoyCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = newBoyHighlighterPosition.transform.position;
                break;

            case "newGirl":
                newGirlCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = newGirlHighlighterPosition.transform.position;
                break;

            case "Gwen":
                GwenCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = GwenHighlighterPosition.transform.position;
                break;

            case "EgyptQueen":
                EgyptQueenCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = EgyptQueenHighlighterPosition.transform.position;
                break;

            case "Witch":
                WitchCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = WitchHighlighterPosition.transform.position;
                break;

            case "Elon":
                ElonCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = ElonHighlighterPosition.transform.position;
                break;

            case "Mansa":
                MansaCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = MansaHighlighterPosition.transform.position;
                break;

            case "Hotb":
                HotbCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = HotbHighlighterPosition.transform.position;
                break;

            case "Hotg":
                HotgCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = HotgHighlighterPosition.transform.position;
                break;

            case "MJ":
                MJCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = MJHighlighterPosition.transform.position;
                break;

            case "Chubbs":
                ChubbsCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = ChubbsHighlighterPosition.transform.position;
                break;

            case "OfficeGirl":
                OfficeGirlCharacterModel.SetActive(true);
                thumbnailHighlighter.transform.position = OfficeGirlHighlighterPosition.transform.position;
                break;

            default:
                Debug.LogWarning($"Unknown character key: {characterKey}");
                break;
        }
    }
    // Called by each thumbnail button (e.g., boy, girl, newBoy, newGirl)
    // so the user can preview it visually.
    public void OnThumbnailClicked(string characterKey)
    {
        ShowCharacterByKey(characterKey);
    }

    // Called by your "Exit" button to restore the truly selected character visually
    public void ViewSelectedCharacter()
    {
        // 1) Figure out which character is truly selected in the manager
        string selectedKey = gameStartManager.GetCurrentlySelectedCharacter();

        // 2) Show that model visually
        ShowCharacterByKey(selectedKey);

        // 3) Also update GameStartManager’s "view" variables
        //    so the main action button sees the same character.
        if (gameStartManager.characterCosts.TryGetValue(selectedKey, out int cost))
        {
            gameStartManager.currentViewedCharacter = selectedKey;
            gameStartManager.currentViewedCharacterCost = cost;

            // 4) Force the main button to refresh
            gameStartManager.RefreshBigActionButton();
        }
    }
    public void RefreshThumbnailTicks()
    {
        // Boy (always owned)
        boyTick.SetActive(true);

        // Girl (always owned)
        girlTick.SetActive(true);

        // New Boy
        bool newBoyOwned = gameStartManager.IsCharacterBought("newBoy");
        newBoyTick.SetActive(newBoyOwned);

        // New Girl
        bool newGirlOwned = gameStartManager.IsCharacterBought("newGirl");
        newGirlTick.SetActive(newGirlOwned);

        // Gwen
        bool GwenOwned = gameStartManager.IsCharacterBought("Gwen");
        GwenTick.SetActive(GwenOwned);

        // Egypt Queen
        bool EgyptQueenOwned = gameStartManager.IsCharacterBought("EgyptQueen");
        EgyptQueenTick.SetActive(EgyptQueenOwned);

        // Witch
        bool WitchOwned = gameStartManager.IsCharacterBought("Witch");
        WitchTick.SetActive(WitchOwned);

        // Elon
        bool ElonOwned = gameStartManager.IsCharacterBought("Elon");
        ElonTick.SetActive(ElonOwned);

        // Mansa
        bool MansaOwned = gameStartManager.IsCharacterBought("Mansa");
        MansaTick.SetActive(MansaOwned);

        // Hotb
        bool HotbOwned = gameStartManager.IsCharacterBought("Hotb");
        HotbTick.SetActive(HotbOwned);

        // Hotg
        bool HotgOwned = gameStartManager.IsCharacterBought("Hotg");
        HotgTick.SetActive(HotgOwned);

        // MJ
        bool MJOwed = gameStartManager.IsCharacterBought("MJ");
        MJTick.SetActive(MJOwed);

        // Chubbs
        bool ChubbsOwned = gameStartManager.IsCharacterBought("Chubbs");
        ChubbsTick.SetActive(ChubbsOwned);

        // Office Girl
        bool OfficeGirlOwned = gameStartManager.IsCharacterBought("OfficeGirl");
        OfficeGirlTick.SetActive(OfficeGirlOwned);
    }


}
