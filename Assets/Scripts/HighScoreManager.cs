using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI highScoreTextGameOver;
    public TextMeshProUGUI scoreTextGamOver;

    private int currentScore = 0;
    private int highScore = 0;
    private bool isGameOver = false; // Add this flag

   /* private void OnEnable()
    {
        DataSaver.Instance.OnFirebaseDataLoaded += SetHighScore;
    }

    private void OnDisable()
    {
        DataSaver.Instance.OnFirebaseDataLoaded -= SetHighScore;
    }
*/
    private void Start()
    {
        // Load the high score from PlayerPrefs
        if (PlayerPrefs.GetInt("GuestLogin") == 1 || !PlayerPrefs.HasKey("GuestLogin"))
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        SetHighScore();
       // UpdateHighScoreUI();
    }

    public void SetHighScore()
    {
       
      
        if (PlayerPrefs.GetInt("GuestLogin") == 1)
        {
            highScore = PlayerPrefs.GetInt("HighScore");
           // DataSaver.Instance.GuestData.highScore = highScore;
             highScoreText.text = highScore.ToString();
        }
      //  else if (PlayerPrefs.GetInt("GoogleLoggedIn") == 1)
        {
           // highScore = DataSaver.Instance.UserData.highScore;
        }


        highScoreText.text = highScore.ToString();
        Debug.Log(" highScoreText.text" + highScoreText.text);
        Debug.Log(highScoreText.text + "highScoreText");
        //   highScoreText.text = highScore.ToString();

    }


    private void Update()
    {
        if (!isGameOver && Character.GameStarted == true) // Only update the score if the game is not over
        {
            // Update the current score during the game
            // This example assumes score increases with time; adjust as needed
            currentScore = (int)(Time.timeSinceLevelLoad * 10); // Example: 10 points per second
            UpdateScoreUI();
        }
    }

    public void CheckForHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            Debug.Log("highScore"+ highScore);
          //  PlayerPrefs.SetInt("HighScore", highScore);
            if (PlayerPrefs.GetInt("GuestLogin") == 1)
            {
                PlayerPrefs.SetInt("HighScore", highScore);
                Debug.Log("PlayerPrefs.SetInt highScore)");
            }
            else
            {
                DataSaver.Instance.UserData.highScore = highScore;
                DataSaver.Instance.SaveDataFn();
            }
            SetHighScore();
            //UpdateHighScoreUI();
        }
    }


    // New function to update UI when the game is over
    public void UpdateGameOverUI()
    {
        highScoreTextGameOver.text = highScore.ToString();
        scoreTextGamOver.text = currentScore.ToString();
        isGameOver = true; // Set the game over flag
    }

    private void UpdateScoreUI()
    {
        scoreText.text = currentScore.ToString();
    }

 /*   private void UpdateHighScoreUI()
    {
        //if (DataSaver.Instance.userId != null) 
        //{
        //    highScoreText.text = DataSaver.Instance.UserData.highScore.ToString();
        //}
        //else
        //{
        highScoreText.text = highScore.ToString();
        //    }

    }*/

    private void OnDestroy()
    {
        // Save the high score when the game object is destroyed
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
