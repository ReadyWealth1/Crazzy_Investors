using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameOverVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;    // Reference to the VideoPlayer component (main game)
    public VideoPlayer tutorialVideoPlayer; // Reference to the VideoPlayer component (tutorial mode)
    public RawImage videoScreen;       // Reference to the RawImage that displays the video (main game)
    public RawImage tutorialVideoScreen; // Reference to the RawImage for the tutorial video
    public GameObject gameOverUI;      // Reference to any game over UI you might have (main game)
    public GameObject tutorialGameOverUI; // Reference to game over UI for tutorial mode
    public VideoClip boyGameOverVideo; // Video for boy character's game over
    public VideoClip girlGameOverVideo; // Video for girl character's game over

    public GroundSpawnerTest groundSpawnerTest; // Reference to the GroundSpawnerTest script

    // Call this method when the character goes bankrupt
    public void OnBankruptcy()
    {
        Debug.Log("OnBankruptcy called!");

        if (groundSpawnerTest == null)
        {
            Debug.LogError("GroundSpawnerTest reference is not assigned.");
            return;
        }

        // Check if tutorial mode is active
        bool isTutorialMode = GroundSpawnerTest.isTutorialMode;
        Debug.Log("Is Tutorial Mode: " + isTutorialMode);

        // Select the appropriate components
        VideoPlayer activeVideoPlayer = isTutorialMode ? tutorialVideoPlayer : videoPlayer;
        RawImage activeVideoScreen = isTutorialMode ? tutorialVideoScreen : videoScreen;
        GameObject activeGameOverUI = isTutorialMode ? tutorialGameOverUI : gameOverUI;

        if (activeVideoPlayer == null || activeVideoScreen == null)
        {
            Debug.LogError("Active VideoPlayer or RawImage is null!");
            return;
        }

        // Activate the game over UI
        activeGameOverUI?.SetActive(true);

        // Get the selected character
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter", "boy");
        Debug.Log("Selected Character: " + selectedCharacter);

        // Assign the correct video clip
        if (selectedCharacter == "boy")
        {
            activeVideoPlayer.clip = boyGameOverVideo;
        } 
        // Debug clip assignment
        Debug.Log($"Assigned Clip: {activeVideoPlayer.clip}");

        if (activeVideoPlayer.clip == null)
        {
            Debug.LogError("No video clip assigned to the VideoPlayer!");
            return;
        }

        // Set the RawImage texture
        activeVideoScreen.texture = activeVideoPlayer.targetTexture;
        activeVideoScreen.gameObject.SetActive(true);
        activeVideoPlayer.gameObject.SetActive(true);

        // Prepare and play the video
        Debug.Log("Preparing video...");
        activeVideoPlayer.Prepare();
        activeVideoPlayer.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log($"Video prepared for {vp.name}. Starting playback.");
        vp.Play();
       
    }


}
