using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
    public GameOverVideoPlayer GameOverVideoPlayer;

    public GameObject boyBackpackObject;
    public GameObject girlBackpackObject;
    public GameObject newBoyBackpackObject;
    public GameObject newGirlBackpackObject;
    public GameObject GwenBackpackObject;
    public GameObject EgyptQueenBackpackObject;
    public GameObject WitchBackpackObject;
    public GameObject ElonBackpackObject;
    public GameObject MansaBackpackObject;
    public GameObject HotbBackpackObject;
    public GameObject HotgBackpackObject;
    public GameObject MJBackpackObject;
    public GameObject ChubbsBackpackObject;
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



    private void Start()
    {
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

        StartCoroutine(UpdateIncomeAndTimer());

        // Initialize UI screens
        jobCounterScreen.SetActive(true);
        jobMissedScreen.SetActive(false);
        UpdateBackpackVisibility();
    }


    public void ShowGameOverScreen()
    {
        //GameOverVideoPlayer.OnBankruptcy();
        //gameOverPanel.SetActive(true);
        // Debug.Log("Game Over Screen Shown");
    }

    private void Update()
    {
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
            // Debug.Log("Job lost: Salary set to 0, JobPresent = false");
        }

        //CashFlow = TotalPassiveIncome - inflationRate;
        /*  if (inflationRate + TotalPassiveIncome != 0 && TotalPassiveIncome != 0)
          {
         //     Casflow_Percentage = ((TotalPassiveIncome) / (inflationRate + TotalPassiveIncome)) * 100;
          }
          else
          {
              Casflow_Percentage = CashFlow;
          }*/

        CoinsText.text = " " + NumberFormatter.FormatNumberIndianSystem(numberOfCoins);
        //InflationRateText.text = " " + Mathf.Round(inflationRate);
        //PassiveIncomeText.text = "" + Mathf.Round(TotalPassiveIncome);
        //CashFlowText_Red.text = "" + Mathf.Round((CashFlow));
        //CashFlowText_Green.text = "" + Mathf.Round((CashFlow));
        /*
                if (CashFlow >= 0)
                {
                    CashflowObj_green.SetActive(true);
                    CashflowObj_red.SetActive(false);
                }
                else
                {
                    CashflowObj_red.SetActive(true);
                    CashflowObj_green.SetActive(false);
                }
        */
        //Debug.Log($"First_File_Zero:{First_File_Zero},Update: numberOfCoins = {numberOfCoins}, PassiveIncome = {PassiveIncome}, TotalPassiveIncome = {TotalPassiveIncome}, Salary = {Salary}, JobPresent = {JobPresent}, inflationRate = {inflationRate}, CashFlow = {CashFlow}, Casflow_Percentage = {Casflow_Percentage}");
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
    private IEnumerator UpdateIncomeAndTimer()
    {
        while (true)
        {
            //uiText.text = $"{remainingDuration}"; // Display time in seconds
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);

            if (remainingDuration <= 0)
            {
                // Use 50% inflation if coins >= 1 crore, else 20%
                float inflationPercentage = numberOfCoins >= 10000000f ? 0.4f : 0.2f;

                float inflationCut = numberOfCoins * inflationPercentage;
                totalInflationCut += inflationCut;

                numberOfCoins -= (int)inflationCut;

                assetManager.UpdateAssetValues();
                assetManager.UpdateTotalPortfolioValue();
                First_Zero = false;
                remainingDuration = Duration;
            }

            else
            {
                remainingDuration--;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator ShowGameOverScreenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowGameOverScreen();
    }

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
        if (boyBackpackObject != null)
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
        }

        // Elon
        if (ElonBackpackObject != null)
        {
            ElonBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Elon's backpack object is not assigned in the GameManager.");
        }

        // Mansa
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

        // Office Girl
        if (OfficeGirlBackpackObject != null)
        {
            OfficeGirlBackpackObject.SetActive(shouldActivateBackpacks);
        }
        else
        {
            Debug.LogWarning("Office Girl's backpack object is not assigned in the GameManager.");
        }
    }

    public void TestIncreaseCoins()
    {
        numberOfCoins += 200000;
    }

}