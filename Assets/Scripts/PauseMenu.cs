using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseGameUi;
    public GameStartManager StartManager; // Reference to GameStartManager
    public static PauseMenu instance;
    private HighScoreManager highScoreManager;

    private Coroutine activeCoroutine; // Track the currently running coroutine
    private static int pauseCounter = 0; // Counter to track how many components are keeping the game paused

    private bool isPauseButtonUsed = false; // Track if the Pause Button was used to show UI

    [SerializeField] public static GameObject PauseBtn;

    private void Awake()
    {
        // if (instance == null)
        /*{
            //    instance = this;
        }
        //  else if (instance != this)
        {
            //  Destroy(gameObject); // Ensure only one instance exists
        }*/
        instance = this;
        PauseBtn = GameObject.FindWithTag("PauseBtn");

        highScoreManager = Object.FindFirstObjectByType<HighScoreManager>();
    }
    /*  private void DisablePause()
      {
          if (Character.is1Dead == true)
          {
              PauseBtn.SetActive(false);
          }
      }*/

    void Start()
    {
        if (StartManager == null)
        {
            StartManager = Object.FindFirstObjectByType<GameStartManager>();
        }
    }

    // Pause via Pause Button (shows Pause UI)
    public void PauseByPauseButton()
    {
        // Increment pause counter
        pauseCounter++;

        // Stop any active coroutine to prevent interference
        StopActiveCoroutine();

        // Set flag to show Pause UI
        isPauseButtonUsed = true;

        // Always show the pause UI when this method is called
        PauseGameUi.SetActive(true);

        // Pause the game if it's not already paused
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
    }

    // Pause via Asset Button (no UI)
    public void PauseByAssetButton()
    {
        // Increment pause counter
        pauseCounter++;

        // Stop any active coroutine to prevent interference
        StopActiveCoroutine();

        // Pause the game if it's not already paused
        if (Time.timeScale != 0f)
        {

            Time.timeScale = 0f;
            Debug.Log("Time.timeScale" + Time.timeScale);
        }
    }

    // Pause via Portfolio Button (no UI)
    public void PauseByPortfolioButton()
    {
        // Increment pause counter
        pauseCounter++;

        // Stop any active coroutine to prevent interference
        StopActiveCoroutine();

        // Pause the game if it's not already paused
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
    }

    // Resume the game and decrement the counter
    public void Resume()
    {
        // Decrement pause counter
        pauseCounter--;

        // Ensure counter doesn't go below 0
        if (pauseCounter < 0) pauseCounter = 0;

        // If this was called after using the Pause Button, hide the UI
        if (isPauseButtonUsed && pauseCounter == 0)
        {
            PauseGameUi.SetActive(false);
            isPauseButtonUsed = false; // Reset the flag
        }

        // If the counter reaches zero, resume the game
        if (pauseCounter == 0)
        {
            // Stop any active coroutine to prevent interference
            StopActiveCoroutine();

            // Start the gradual resume process
            StartGradualResume();
        }
    }

    public void ExitToHomeScreen()
    {
        Time.timeScale = 1f;

        // Reset static game state
        Character.isDead = false;
        Character.StopAllState = false;
        Character.is1Dead = false;
        // Reset other statics you use for gameplay lock
        // Example:
        // GameManager.GameOver = false;

        if (highScoreManager != null)
        {
            highScoreManager.CheckForHighScore();
        }
        Character.isGameStart = false;  
        //JioWrapperJS.Instance.showInterstitial();
        SceneManager.LoadScene("new onwrinner samplescene");
    }

    private IEnumerator SlowDownAndPauseCoroutine(float targetTimeScale = 0.1f, float duration = 0.3f)
    {
        float initialTimeScale = Time.timeScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, elapsedTime / duration);
            yield return null;
        }


        // Ensure the game is paused
        activeCoroutine = null; // Clear the active coroutine reference
    }
    public void PauseGame()
    {
        StopActiveCoroutine();
        Debug.Log("PauseGame+++++");
        Time.timeScale = 0f;
        Debug.Log("PauseGame+++++" + Time.timeScale);
    }
    private IEnumerator ResumeCoroutine(float targetTimeScale = 1f, float duration = 2f)
    {
        float initialTimeScale = Time.timeScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, elapsedTime / duration);
            yield return null;
        }

        Time.timeScale = 1f;
        // Ensure the game is fully resumed
        activeCoroutine = null; // Clear the active coroutine reference
    }

    public void StartSlowDownAndPause()
    {
        StopActiveCoroutine(); // Stop any currently running coroutine
        activeCoroutine = StartCoroutine(SlowDownAndPauseCoroutine());
    }

    public void StartGradualResume()
    {
        if (pauseCounter == 0)
        {
            StopActiveCoroutine(); // Stop any currently running coroutine
            activeCoroutine = StartCoroutine(ResumeCoroutine());

        }
    }
    public void Lottery_StartGradualResume()
    {
        if (pauseCounter == 0)
        {
            StopActiveCoroutine(); // Stop any currently running coroutine
            activeCoroutine = StartCoroutine(ResumeCoroutine());

        }
    }

    private void StopActiveCoroutine()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }

    public static void SlowDownAndPause()
    {
        if (instance != null)
        {
            instance.StartSlowDownAndPause();
        }
        else
        {
            Debug.LogWarning("PauseMenu instance is not set.");
        }
    }

    public static void GradualResume()
    {
        if (instance != null)

        {
            instance.StartGradualResume();
        }
        else
        {
            Debug.LogWarning("PauseMenu instance is not set.");
        }
    }
}