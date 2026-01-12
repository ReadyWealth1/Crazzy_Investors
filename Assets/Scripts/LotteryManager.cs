using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class LotteryManager : MonoBehaviour
{
    public GameObject lotteryPanel;
    public Button[] lotteryButtons;
    public TMP_Text[] lotteryButtonTexts;
    public TMP_Text timerText;
    public GameObject timerPanel;
    private int chosenNumber;
    [SerializeField]private TmpLongPopup popupManager;

    void Start()
    {
        

        
        lotteryPanel.SetActive(false);
        timerPanel.SetActive(false);

        // Initially setup lottery buttons without setting the numbers yet.
        for (int i = 0; i < lotteryButtons.Length; i++)
        {
            if (lotteryButtons[i] == null || lotteryButtonTexts[i] == null)
            {
                Debug.LogError("A lottery button or text is not assigned!");
                continue;
            }
            // Remove any existing listeners to prevent duplicate calls
            lotteryButtons[i].onClick.RemoveAllListeners();
        }
    }
    private void OnEnable()
    {
        GameManager.OnGameOver += CloseLotteryPanel;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= CloseLotteryPanel;
    }
    public void CloseLotteryPanel()
    {
        // Refresh the lottery button numbers every time the panel is shown
        RefreshLotteryButtons();
        lotteryPanel.SetActive(false);
    }


    public void ShowLotteryPanel()
    {
        // Refresh the lottery button numbers every time the panel is shown
        RefreshLotteryButtons();
        lotteryPanel.SetActive(true);
    }

    void RefreshLotteryButtons()
    {
        for (int i = 0; i < lotteryButtons.Length; i++)
        {
            int randomNumber = Random.Range(100, 1000);
            lotteryButtonTexts[i].text = randomNumber.ToString();

            // Remove old listeners before adding new ones
            lotteryButtons[i].onClick.RemoveAllListeners();

            // Capture current number in local variable
            int capturedNumber = randomNumber;
            lotteryButtons[i].onClick.AddListener(() => OnLotteryButtonClick(capturedNumber));
        }
    }


    void OnLotteryButtonClick(int number)
    {
        chosenNumber = number;
        lotteryPanel.SetActive(false);
        timerPanel.SetActive(true);
        StartCoroutine(TimerCountdown(5));
        if (GroundSpawnerTest.isTutorialMode == true)
        {
            PauseMenu.instance.Resume();
        }
    }

    IEnumerator TimerCountdown(int seconds)
    {
        int timeLeft = seconds;
        while (timeLeft > 0)
        {
            timerText.text = timeLeft + "s";
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

        bool isWinner = Random.value < 0.25f; // 25% probability of winning

        // Define screen position for popup (150 px from left, 200 px from top)
        Vector2 popupScreenPos = new Vector2(150, 800);

        if (isWinner)
        {
            popupManager.ShowPopup(
                $"Your Number {chosenNumber} won, you got 25000",
                Color.green,
                popupScreenPos
            );
            GameManager.numberOfCoins += 25000;
        }
        else
        {
            popupManager.ShowPopup(
                $"Your Number {chosenNumber} did not win",
                Color.red,
                popupScreenPos
            );
        }

        timerPanel.SetActive(false);
    }

}