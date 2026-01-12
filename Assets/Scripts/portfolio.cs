using System.Collections;
using UnityEngine;

public class portfolio : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private PauseMenu pauseMenu; // Automatically find PauseMenu if not assigned in Inspector

    public static bool isCanvasActive = false;

    private float warningCooldown = 30f; // Cooldown between warnings
    private float lastWarningTime;
    private bool isInitialDelayPassed = false;
    public GameObject WarningImg;
    Assets assets;
    private Character character;

    private void Start()
    {
        if (pauseMenu == null) // Attempt to find PauseMenu if not set in Inspector
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
            if (pauseMenu == null)
            {
                Debug.LogError("PauseMenu component not found. Ensure PauseMenu is added to the scene.");
            }
            else
            {
                Debug.Log("PauseMenu found: " + pauseMenu.name);
            }
        }

        StartCoroutine(InitialWarningDelay());
        character = FindObjectOfType<Character>();
        if (character == null)
        {
            Debug.LogWarning("Character component not found in the scene.");
        }
    }

    private IEnumerator InitialWarningDelay()
    {
        
        yield return new WaitForSeconds(30f); // Wait 30 seconds before enabling warnings
        isInitialDelayPassed = true;
        lastWarningTime = Time.time; // Start the timer after the initial delay
       
    }

    private void Update()
    {
        if (isInitialDelayPassed)
        {
            Warning();
        }
    }

    private void Warning()
    {
        // Debug current status
        //Debug.Log($"[Warning Check] Time: {Time.time}, LastWarningTime: {lastWarningTime}, Coins: {GameManager.numberOfCoins}, isPortfolioZero: {Assets.isPortfolioZero}");
/*
        if (character != null && character.isDead)
        {
            Debug.Log("Character is dead. Skipping warning.");
            return; // Stop warning if the character has already died
        }*/

        // Check if coin count is below threshold and cooldown has passed
        if (GameManager.numberOfCoins < 100000 && Time.time >= lastWarningTime + warningCooldown && Assets.isPortfolioZero == true)
        {
            Debug.Log("Warning conditions met. Activating warning canvas and pausing game.");
            // Show warning canvas
            canvas.SetActive(true);
            lastWarningTime = Time.time; // Reset the last warning time
            isCanvasActive = true;
            WarningImg.SetActive(true);
            if (pauseMenu != null)
            {
                pauseMenu.PauseGame(); // Pause the game
                Debug.Log("Game paused by PauseMenu.");
            }
            else
            {
                Debug.LogWarning("PauseMenu not assigned. Game will not pause.");
            }
        }
        
    }

    public void PortfolioActivate()
    {
        isCanvasActive = true;
        canvas.SetActive(true);
        Debug.Log("Portfolio activated.");
    }

    public void ToggleCanvas()
    {
        isCanvasActive = !isCanvasActive;
        canvas.SetActive(isCanvasActive);
        Debug.Log("Portfolio toggled. isCanvasActive: " + isCanvasActive);
    }

    public void PortfolioDeactivate()
    {
        isCanvasActive = false;
        canvas.SetActive(false);
        Debug.Log("Portfolio deactivated.");
    }
}
