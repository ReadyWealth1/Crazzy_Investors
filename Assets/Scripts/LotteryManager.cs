using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LotteryManager : MonoBehaviour
{
    public GameObject lotteryPanel;
    public Button[] lotteryButtons;
    public TMP_Text[] lotteryButtonTexts;
    public TMP_Text timerText;
    public GameObject timerPanel;
    private int chosenNumber;
    public TmpLongPopup popupManager;

    void Start()
    {
        if (popupManager == null)
        {
            popupManager = FindObjectOfType<TmpLongPopup>();
        }

        // (Assignment checks omitted for brevity)

        // Hide the lottery and timer panels at the start
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
            int number = randomNumber; // Capture the current value of randomNumber
            lotteryButtons[i].onClick.AddListener(() => OnLotteryButtonClick(number));
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

        if (isWinner)
        {
            popupManager.ShowPopup($"Your Number {chosenNumber} won, you got 25000", Color.green, new Vector3(450, 1700, 0));
            GameManager.numberOfCoins += 25000;
        }
        else
        {
            popupManager.ShowPopup($"Your Number {chosenNumber} did not win", Color.red, new Vector3(500, 1700, 0));
        }
        timerPanel.SetActive(false);
    }
}