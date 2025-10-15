using TMPro;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Coroutine incomeCoroutine;
    public static bool isGameOver = false;
    public static event System.Action OnGameOver;
    [SerializeField] public Slider Health;
    [SerializeField] private TMPPopupManager popupManager;
    public GameObject gameOverPanel;
    public GameObject coinOverPanel;
    public static float numberOfCoins;
    public TextMeshProUGUI CoinsText;
    public static int PassiveIncome;
    // public TextMeshProUGUI PassiveIncomeText;
    // public TextMeshProUGUI InflationRateText;
    // public TextMeshProUGUI CashFlowText_Red;
    // public TextMeshProUGUI CashFlowText_Green;
    // public static float inflationRate;
    // public static float increaseRate;
    // public static float CashFlow;
    public Character characterInstance;
    public static bool First_Zero = true;
    public static bool First_File_Zero = true;
    // public GameObject CashflowObj_red;
    // public GameObject CashflowObj_green;
    //public static float Casflow_Percentage;
    // [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private Image uiFill;
    private int remainingDuration;
    public static int Duration = 45;
    public Assets assetManager;

    // New fields for salary and job status
    public static int Salary;
    public static bool JobPresent;

    // New fields for UI screens
    public GameObject jobCounterScreen;
    public GameObject jobMissedScreen;
   /* public GameOverVideoPlayer GameOverVideoPlayer;
*/
   /* public GameObject boyBackpackObject;
    public GameObject girlBackpackObject;
    public GameObject newBoyBackpackObject;
    public GameObject newGirlBackpackObject;
    public GameObject GwenBackpackObject;
    public GameObject EgyptQueenBackpackObject;
    public GameObject WitchBackpackObject;*/
    public GameObject ElonBackpackObject;
/*    public GameObject MansaBackpackObject;
    public GameObject HotbBackpackObject;
    public GameObject HotgBackpackObject;
    public GameObject MJBackpackObject;
    public GameObject ChubbsBackpackObject;*/
    public GameObject OfficeGirlBackpackObject;


    public static float totalInflationCut = 0f; // Accumulate total inflation cut
    public TextMeshProUGUI TotalInflationCutText;
    // Property to get the total passive income

    public static int TotalPassiveIncome
    {
        get
        {
            return PassiveIncome + (JobPresent ? Salary : 0);
        }
    }

    private void OnEnable()
    {
        // Start only the repeating inflation coroutine
        StartCoroutine(UpdateIncomeAndTimer());

    }

    private void Start()
    {
      /*  incomeCoroutine = StartCoroutine(UpdateIncomeAndTimer());*/

        numberOfCoins = 1000000;
        PassiveIncome = 0;
        // inflationRate = 200f;
        //increaseRate = 75f;
        First_Zero = true;
        First_File_Zero = true;  // Reset this to true on scene restart
        remainingDuration = Duration;
        //assetManager = FindObjectOfType<Assets>();

        // Initialize salary and job status
        Salary = 45000; // Set your desired initial salary
        JobPresent = true;

        // Debug.Log($"Game Started: numberOfCoins = {numberOfCoins}, PassiveIncome = {PassiveIncome}, inflationRate = {inflationRate}, increaseRate = {increaseRate}, Salary = {Salary}, JobPresent = {JobPresent}");

        //StartCoroutine(UpdateIncomeAndTimer());

        // Initialize UI screens
        jobCounterScreen.SetActive(true);
        jobMissedScreen.SetActive(false);
        UpdateBackpackVisibility();
    }


   /* public void ShowGameOverScreen()
    {
        //GameOverVideoPlayer.OnBankruptcy();
        //gameOverPanel.SetActive(true);
        // Debug.Log("Game Over Screen Shown");
    }*/

    private void Update()
    {
        // Always show the final coin value, even after game over
        CoinsText.text = " " + NumberFormatter.FormatNumberIndianSystem(numberOfCoins);

        if (isGameOver)
            return;

        if (numberOfCoins < 0)
        {
            isGameOver = true;
            OnGameOver?.Invoke();
            if (incomeCoroutine != null)
                StopCoroutine(incomeCoroutine);

          // StartCoroutine(ShowGameOverScreenAfterDelay(2f));
            return;
        }

        if (Character.is1Dead == true)
        {
            TotalInflationCutText.text = "" + NumberFormatter.FormatNumberIndianSystem(totalInflationCut);
            Character.is1Dead = false;
        }

        if (FileCollector.currentFiles == 0 && !First_File_Zero)
        {
            Salary = 0;
            JobPresent = false;
            jobCounterScreen.SetActive(false);
            jobMissedScreen.SetActive(true);
        }
    }



    public static class NumberFormatter
    {
        public static string FormatNumberIndianSystem(float number)
        {
            if (number >= 10000000)
                return (number / 10000000).ToString("0.##") + " Cr"; // Crore
            if (number >= 100000)
                return (number / 100000).ToString("0.##") + " L"; // Lakh
            if (number >= 1000)
                return (number / 1000).ToString("0.##") + " K"; // Thousand
            return number.ToString("0");
        }
    }

    public void OnFileCountChanged(int currentFiles)
    {
        if (JobPresent)
        {
            if (currentFiles > 0)
            {
                jobCounterScreen.SetActive(true);
                jobMissedScreen.SetActive(false);
                First_File_Zero = false; // Set to false once a file is picked up
            }
            else if (currentFiles == 0 && !First_File_Zero)
            {
                Salary = 0;
                JobPresent = false;
                jobCounterScreen.SetActive(false);
                jobMissedScreen.SetActive(true);
                UpdateBackpackVisibility(); // Update backpack visibility
                //Debug.Log("File count changed: Salary set to 0, JobPresent = false");
            }
        }
    }


    public void OnJobCollected()
    {
        JobPresent = true;
        Salary = 45000; // Reset the salary
        FileCollector.ResetFiles(); // Reset the file counter to 0
        jobCounterScreen.SetActive(true);
        jobMissedScreen.SetActive(false);
        First_File_Zero = true; // Reset the flag when job is collected
        UpdateBackpackVisibility(); // Update backpack visibility
        //Debug.Log("Job collected: JobPresent = true, Salary reset, file counter reset to 0");
    }
    public void RestartGame()
    {
        // Reset states
        isGameOver = false;
        remainingDuration = Duration;
        totalInflationCut = 0;
        uiFill.fillAmount = 1f; // full again

        // Stop any old coroutine (if running) and restart
        if (incomeCoroutine != null)
            StopCoroutine(incomeCoroutine);

        incomeCoroutine = StartCoroutine(UpdateIncomeAndTimer());

        Debug.Log("Game Restarted!");
    }
    private IEnumerator UpdateIncomeAndTimer()
    {
        remainingDuration = Duration; // reset when coroutine starts

        while (!isGameOver)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);

            if (remainingDuration <= 0)
            {
                // Calculate net monthly change
                float monthlyIncome = 0f;
                float monthlyExpense = 0f;

                // 1. Credit Salary + Passive Income first
                if (JobPresent || PassiveIncome > 0)
                {
                    monthlyIncome = TotalPassiveIncome;
                    numberOfCoins += TotalPassiveIncome;
                }

                // 2. Apply Inflation
                float inflationPercentage = numberOfCoins >= 10000000f ? 0.4f : 0.2f;
                float inflationCut = numberOfCoins * inflationPercentage;
                monthlyExpense = inflationCut;

                totalInflationCut += inflationCut;
                TotalInflationCutText.text = NumberFormatter.FormatNumberIndianSystem(totalInflationCut);

                numberOfCoins -= (int)inflationCut;

                // Calculate net change for popup
                float netChange = monthlyIncome - monthlyExpense;

                // Show popup for monthly calculation
                string formattedValue = NumberFormatter.FormatNumberIndianSystem(Mathf.Abs(netChange));
                string sign = netChange >= 0 ? "+" : "-";
                Color popupColor = netChange >= 0 ? Color.green : Color.red;

                // Assuming you have popupManager reference in GameManager
                // You'll need to add this reference to your GameManager class
                if (popupManager != null)
                {
                    popupManager.ShowPopup($"{sign}{formattedValue}", popupColor, new Vector2(150, 800));
                }

                // 3. Update assets & portfolio
                assetManager.UpdateAssetValues();
                assetManager.UpdateTotalPortfolioValue();

                First_Zero = false;

                // 4. Reset timer
                remainingDuration = Duration;
            }
            else
            {
                remainingDuration--;
            }

            yield return new WaitForSeconds(1f);
        }
    }


    /* IEnumerator ShowGameOverScreenAfterDelay(float delay)
     {
         yield return new WaitForSeconds(delay);
         ShowGameOverScreen();
     }*/

    // Method to change job status
    public void SetJobStatus(bool isPresent)
    {
        JobPresent = isPresent;
        UpdateBackpackVisibility();
        Update(); // Update the values immediately
        //Debug.Log($"Job Status Changed: JobPresent = {JobPresent}, Salary = {Salary}");
    }
    private void UpdateBackpackVisibility()
    {
        bool shouldActivateBackpacks = JobPresent;

        // Boy
        /*if (boyBackpackObject != null)
        {
            boyBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Boy's backpack object is not assigned in the GameManager.");
        }

        // Girl
        if (girlBackpackObject != null)
        {
            girlBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Girl's backpack object is not assigned in the GameManager.");
        }

        // New Boy
        if (newBoyBackpackObject != null)
        {
            newBoyBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("New Boy's backpack object is not assigned in the GameManager.");
        }

        // New Girl
        if (newGirlBackpackObject != null)
        {
            newGirlBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("New Girl's backpack object is not assigned in the GameManager.");
        }

        // Gwen
        if (GwenBackpackObject != null)
        {
            GwenBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Gwen's backpack object is not assigned in the GameManager.");
        }

        // Egypt Queen
        if (EgyptQueenBackpackObject != null)
        {
            EgyptQueenBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Egypt Queen's backpack object is not assigned in the GameManager.");
        }

        // Witch
        if (WitchBackpackObject != null)
        {
            WitchBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Witch's backpack object is not assigned in the GameManager.");
        }*/

        // Elon
        if (ElonBackpackObject != null)
        {
            ElonBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Elon's backpack object is not assigned in the GameManager.");
        }

       /* // Mansa
        if (MansaBackpackObject != null)
        {
            MansaBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Mansa's backpack object is not assigned in the GameManager.");
        }

        // Hotb
        if (HotbBackpackObject != null)
        {
            HotbBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Hotb's backpack object is not assigned in the GameManager.");
        }

        // Hotg
        if (HotgBackpackObject != null)
        {
            HotgBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Hotg's backpack object is not assigned in the GameManager.");
        }

        // MJ
        if (MJBackpackObject != null)
        {
            MJBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("MJ's backpack object is not assigned in the GameManager.");
        }

        // Chubbs
        if (ChubbsBackpackObject != null)
        {
            ChubbsBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Chubbs' backpack object is not assigned in the GameManager.");
        }

        // Office Girl*/
        if (OfficeGirlBackpackObject != null)
        {
            OfficeGirlBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Office Girl's backpack object is not assigned in the GameManager.");
        }
    }

   

}