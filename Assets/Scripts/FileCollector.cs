using UnityEngine;
using TMPro;
using UnityEngine.UI; // Add this if you're using Unity's UI Image

public class FileCollector : MonoBehaviour
{
    public TMP_Text fileCounterText; // Reference to the TextMeshPro Text
    public int maxFiles = 3; // Maximum number of files
    public static int currentFiles; // Current number of files collected

    // Reference to the GameManager
    public GameManager gameManager;

    // Add public Sprite variables for different file states
    public Sprite fileSprite3; // Image for 3 files collected
    public Sprite fileSprite2; // Image for 2 files collected
    public Sprite fileSprite1; // Image for 1 file collected

    // Reference to the Image or SpriteRenderer component to change images
    public Image fileImage; // For UI (use this if the image is part of UI)
    // public SpriteRenderer fileSpriteRenderer; // For in-game objects (use this if not part of UI)

    // Update the UI text and the image
    void UpdateFileCounter()
    {
        // Update file count text
        fileCounterText.text = currentFiles + "/" + maxFiles;

        // Change image based on current file count
        if (currentFiles == 3)
        {
            fileImage.sprite = fileSprite3; // For UI elements
            // fileSpriteRenderer.sprite = fileSprite3; // For in-game objects
        }
        else if (currentFiles == 2)
        {
            fileImage.sprite = fileSprite2; // For UI elements
            // fileSpriteRenderer.sprite = fileSprite2; // For in-game objects
        }
        else if (currentFiles == 1)
        {
            fileImage.sprite = fileSprite1; // For UI elements
            // fileSpriteRenderer.sprite = fileSprite1; // For in-game objects
        }
        // Debug.Log($"File Counter Updated: {currentFiles}/{maxFiles}");
    }

    // Method to collect a file
    void CollectFile()
    {
        if (GameManager.JobPresent && currentFiles < maxFiles)
        {
            currentFiles++;
            GameManager.First_File_Zero = false; // Set to false after collecting the first file
            UpdateFileCounter();
            gameManager.OnFileCountChanged(currentFiles); // Notify GameManager of the new file count
            // Debug.Log($"File collected: currentFiles = {currentFiles}");
        }
    }

    // Method to handle missing a file
    void MissFile()
    {
        if (GameManager.JobPresent)
        {
            if (currentFiles > 0)
            {
                currentFiles--;
            }
            else
            {
                // If the player misses files without collecting any, First_File_Zero should be false
                GameManager.First_File_Zero = false;
            }
            UpdateFileCounter();
            gameManager.OnFileCountChanged(currentFiles); // Notify GameManager
            // Debug.Log($"File missed: currentFiles = {currentFiles}");
        }
    }

    // Method to reset the file counter (called when job is collected)
    public static void ResetFiles()
    {
        currentFiles = 3; // Start with the maximum number of files
        // Debug.Log("File counter reset to 3");
    }

    // OnTriggerEnter to detect collision with file objects or trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("file"))
        {
            CollectFile();
            Destroy(other.gameObject); // Optionally destroy the file object after collection
        }
        else if (other.CompareTag("MissedFileTrigger"))
        {
            MissFile(); // Miss a file if triggered
        }
        else if (other.CompareTag("Job"))
        {
            currentFiles = maxFiles; // Set file counter to max when job is collected
            UpdateFileCounter(); // Update the file counter UI
            gameManager.OnJobCollected(); // Handle job collection
            Destroy(other.gameObject); // Optionally destroy the job object after collection
            // Debug.Log("Job object collected and file counter set to max");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FileCollector.ResetFiles(); // Initialize the file counter to 3
        UpdateFileCounter(); // Update the file counter text
    }
}
