using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    //public TextMeshProUGUI highScoreTextGameOver;
    // public TextMeshProUGUI scoreTextGamOver;

    public int currentScore = 0;
    private int highScore = 0;
    private bool isGameOver = false;
    public float scoreCount = 0f;

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
       
        //   highScoreText.text = highScore.ToString();

    }


    private void Update()
    {
        if (!isGameOver && Character.isGameStart == true) // Only update the score if the game is not over
        {
           
            scoreCount += Time.deltaTime *10;
            //Debug.Log(currentScore + "==============");
            // Update the current score during the game
            // This example assumes score increases with time; adjust as needed
            currentScore = (int)(scoreCount ); // Example: 10 points per second
            //Debug.Log(currentScore+"==============");
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
        //highScoreTextGameOver.text = highScore.ToString();
       // scoreTextGamOver.text = currentScore.ToString();
        isGameOver = true; // Set the game over flag
        PlayerPrefs.SetInt("CurrentScore",currentScore);
        Debug.Log("CurrentScore"+ currentScore);
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
