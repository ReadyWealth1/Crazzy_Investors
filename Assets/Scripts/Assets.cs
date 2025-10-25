using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Assets : MonoBehaviour
{
    // References to sell buttons
    /* public Button GoldBondSellButton;
     public Button StockSellButton;
     public Button FixedDepositSellButton;
     public Button RealEstateSellButton;
     public Button CryptoSellButton;
     public Button AntiqueSellButton;
     public Button MutualFundsSellButton;
     public Button LandSellButton;*/

    // UI TMP_Text elements for asset names
    /*public TMP_Text GoldBondNameText;
    public TMP_Text StockNameText;
    public TMP_Text FixedDepositNameText;
    public TMP_Text RealEstateNameText;
    public TMP_Text CryptoNameText;
    public TMP_Text AntiqueNameText;
    public TMP_Text MutualFundsNameText;
    public TMP_Text LandNameText;
    */
    // UI TMP_Text elements for invested amounts
    public TMP_Text GoldBondInvestedText;
    public TMP_Text StockInvestedText;
    public TMP_Text FixedDepositInvestedText;
    public TMP_Text RealEstateInvestedText;
    public TMP_Text CryptoInvestedText;
    public TMP_Text AntiqueInvestedText;
    public TMP_Text MutualFundsInvestedText;
    public TMP_Text LandInvestedText;

    // UI TMP_Text elements for passive income
    public TMP_Text GoldBondPassiveIncomeText;
    public TMP_Text StockPassiveIncomeText;
    public TMP_Text FixedDepositPassiveIncomeText;
    public TMP_Text RealEstatePassiveIncomeText;
    public TMP_Text CryptoPassiveIncomeText;
    public TMP_Text AntiquePassiveIncomeText;
    public TMP_Text MutualFundsPassiveIncomeText;
    public TMP_Text LandPassiveIncomeText;

    // UI TMP_Text elements for asset costs
    public TMP_Text GoldBondCostText;
    public TMP_Text StockCostText;
    public TMP_Text FixedDepositCostText;
    public TMP_Text RealEstateCostText;
    public TMP_Text CryptoCostText;
    public TMP_Text AntiqueCostText;
    public TMP_Text MutualFundsCostText;
    public TMP_Text LandCostText;

    // Costs for each asset
    public int GoldBondCost;
    public int StockCost;
    public int FixedDepositCost;
    public int RealEstateCost;
    public int CryptoCost;
    public int AntiqueCost;
    public int MutualFundsCost;
    public int LandCost;

    // Passive income for each asset
    public int GoldBondPassiveIncome;
    public int StockPassiveIncome;
    public int FixedDepositPassiveIncome;
    public int RealEstatePassiveIncome;
    public int CryptoPassiveIncome;
    public int AntiquePassiveIncome;
    public int MutualFundsPassiveIncome;
    public int LandPassiveIncome;
    //Passive income for asset buying page

    public TMP_Text GoldBondAssetPassiveIncomeText;
    public TMP_Text StockAssetPassiveIncomeText;
    public TMP_Text FixedDepositAssetPassiveIncomeText;
    public TMP_Text RealEstateAssetPassiveIncomeText;
    public TMP_Text CryptoAssetPassiveIncomeText;
    public TMP_Text AntiqueAssetPassiveIncomeText;
    public TMP_Text MutualFundsAssetPassiveIncomeText;
    public TMP_Text LandAssetPassiveIncomeText;

    public GameObject AssetCanvas;

    private bool CanvasShown = false;

    private float BuyCycle = 30f; // Duration of asset buy cycle in seconds
    private float AssetPanelDuration = 10f; // Duration for which the asset panel is shown

    // Portfolio value for each asset
    public static int GoldPortfolio;
    public static int StockPortfolio;
    public static int FixedDepositPortfolio;
    public static int RealEstatePortfolio;
    public static int CryptoPortfolio;
    public static int AntiquePortfolio;
    public static int MutualFundsPortfolio;
    public static int LandPortfolio;

    // Total passive income accumulated for each asset
    public int TotalGoldBondPassiveIncome;
    public int TotalStockPassiveIncome;
    public int TotalFixedDepositPassiveIncome;
    public int TotalRealEstatePassiveIncome;
    public int TotalCryptoPassiveIncome;
    public int TotalAntiquePassiveIncome;
    public int TotalMutualFundsPassiveIncome;
    public int TotalLandPassiveIncome;

    // Increment factors for asset costs
    private int GoldCostIncrement1 = 50000;
    private int StockCostIncrement2 = 100000;
    private int FixedDepositCostIncrement3 = 50000;
    private int RealEstateCostIncrement4 = 80000;
    private int CryptoCostIncrement5 = 30000;
    private int AntiqueCostIncrement6 = 40000;
    private int MutualFundsCostIncrement7 = 25000;
    private int LandCostIncrement8 = 100000;

    // Increment factors for passive income
    private int GoldPassiveIncomeIncrement1 = 0;
    private int StockPassiveIncomeIncrement2 = 2000;
    private int FixedDepositPassiveIncomeIncrement3 = 0;
    private int RealEstatePassiveIncomeIncrement4 = 0;
    private int CryptoPassiveIncomeIncrement5 = 0;
    private int AntiquePassiveIncomeIncrement6 = 0;
    private int MutualFundsPassiveIncomeIncrement7 = 0;
    private int LandPassiveIncomeIncrement8 = 0;

    private int lastPurchasedAssetCost; // Variable to store the cost of the last purchased asset
    public static int BankBalanceMilestone;
    public static bool first_canvasShown;
    public static int AddtoBankBalanceMilestone = 200000;
    private bool AssetspageFirstShown = true;
    private bool AssetspageSecondShown = false;
    public GameObject AssetButton;
    public bool Assetbtn_bool;
    public GameObject Assets1stPage;
    public GameObject Assets2ndPage;
    public CameraFollow CameraFollow;

    public TMP_Text TotalPortfolioValueText;
    public TMP_Text TotalPortfolioValueTextSum;
    public TMP_Text TotalPassiveIncomeText;
    private int TotalPortfolioValue;
    private int totalPassiveIncome;

    private TimedPassiveIncome timedPassiveIncome;

    private int goldBondPurchases = 0;
    private int stockPurchases = 0;
    private int fixedDepositPurchases = 0;
    private int realEstatePurchases = 0;
    private int cryptoPurchases = 0;
    private int antiquePurchases = 0;
    private int mutualFundsPurchases = 0;
    private int landPurchases = 0;
    [SerializeField] HighScoreManager HighScoreManager;
    public static bool isPortfolioZero = false;
    void Start()
    {
        first_canvasShown = false;
        BankBalanceMilestone = 1000001;

        GoldBondCost = 50000;
        StockCost = 200000;
        FixedDepositCost = 150000;
        RealEstateCost = 600000;
        CryptoCost = 300000;
        AntiqueCost = 400000;
        MutualFundsCost = 250000;
        LandCost = 800000;

        Show1stAssetsPage();
        UpdateUICosts(); // Ensure the UI is updated with initial costs
        UpdateUI();
        UpdateTotalPortfolioValue(); // Add this line to calculate the initial total value
        UpdateTotalPassiveIncome();

        GoldPortfolio = 0;
        StockPortfolio = 0;
        FixedDepositPortfolio = 0;
        RealEstatePortfolio = 0;
        CryptoPortfolio = 0;
        AntiquePortfolio = 0;
        MutualFundsPortfolio = 0;
        LandPortfolio = 0;
        timedPassiveIncome = gameObject.AddComponent<TimedPassiveIncome>();

    }

    void Update()
    {
        ActivateAssetSelectionBtn();
    }

    public void UpdateTotalPortfolioValue()
    {
        TotalPortfolioValue = GoldPortfolio + StockPortfolio + FixedDepositPortfolio +
                              RealEstatePortfolio + CryptoPortfolio + AntiquePortfolio +
                              MutualFundsPortfolio + LandPortfolio;
        if (TotalPortfolioValue == 0)
        {
            isPortfolioZero = false;

        }
    }
    public void Char_buy_Something()
    {

        Character.StartRunning();
        if (!Character.isGameStart)
        {
            Character.isGameStart = true;
            HighScoreManager.scoreCount = 0;
            Debug.Log("This will run only once!");
          //  JioWrapperJS.Instance.cacheInterstitial();
            
            
        }
    }

    public void ResetAsset()
    {
        first_canvasShown = false;
        BankBalanceMilestone = 1000001;

        GoldBondCost = 50000;
        StockCost = 200000;
        FixedDepositCost = 150000;
        RealEstateCost = 600000;
        CryptoCost = 300000;
        AntiqueCost = 400000;
        MutualFundsCost = 250000;
        LandCost = 1000000;

        Show1stAssetsPage();
        UpdateUICosts(); // Ensure the UI is updated with initial costs
        UpdateUI();


    }

    private void ActivateAssetSelectionBtn()
    {
        //Debug.Log("Coins: " + GameManager.numberOfCoins + ", Milestone: " + BankBalanceMilestone);
        // if (GameManager.numberOfCoins >= BankBalanceMilestone)
        {
            //     AssetButton.gameObject.SetActive(true);
            //Debug.Log("AssetButton Activated");
        }
    }

    public bool IsFirstCanvasShown()
    {
        return first_canvasShown;
    }

    public static void IncrementMilestone()
    {
        BankBalanceMilestone += AddtoBankBalanceMilestone;
        //Debug.Log("New BankBalanceMilestone: " + BankBalanceMilestone);
    }

    public void ShowAssetCanvasWithCurrentValues()
    {
        UpdateAssetValues(); // Update the values before showing the canvas
        ShowAssetCanvas(); // Show the canvas with updated values
    }

    public void UpdateUI()
    {
        // Update invested amounts
        GoldBondInvestedText.text = FormatAmount(GoldPortfolio);
        StockInvestedText.text = FormatAmount(StockPortfolio);
        FixedDepositInvestedText.text = FormatAmount(FixedDepositPortfolio);
        RealEstateInvestedText.text = FormatAmount(RealEstatePortfolio);
        CryptoInvestedText.text = FormatAmount(CryptoPortfolio);
        AntiqueInvestedText.text = FormatAmount(AntiquePortfolio);
        MutualFundsInvestedText.text = FormatAmount(MutualFundsPortfolio);
        LandInvestedText.text = FormatAmount(LandPortfolio);



        // Update passive income
        GoldBondPassiveIncomeText.text = FormatAmount(TotalGoldBondPassiveIncome);
        StockPassiveIncomeText.text = FormatAmount(TotalStockPassiveIncome);
        FixedDepositPassiveIncomeText.text = FormatAmount(TotalFixedDepositPassiveIncome);
        RealEstatePassiveIncomeText.text = FormatAmount(TotalRealEstatePassiveIncome);
        CryptoPassiveIncomeText.text = FormatAmount(TotalCryptoPassiveIncome);
        AntiquePassiveIncomeText.text = FormatAmount(TotalAntiquePassiveIncome);
        MutualFundsPassiveIncomeText.text = FormatAmount(TotalMutualFundsPassiveIncome);
        LandPassiveIncomeText.text = FormatAmount(TotalLandPassiveIncome);

        //Asset selection Passive income
        GoldBondAssetPassiveIncomeText.text = FormatAmount(GoldBondPassiveIncome);
        StockAssetPassiveIncomeText.text = FormatAmount(StockPassiveIncome);
        FixedDepositAssetPassiveIncomeText.text = FormatAmount(FixedDepositPassiveIncome);
        RealEstateAssetPassiveIncomeText.text = FormatAmount(RealEstatePassiveIncome);
        CryptoAssetPassiveIncomeText.text = FormatAmount(CryptoPassiveIncome);
        AntiqueAssetPassiveIncomeText.text = FormatAmount(AntiquePassiveIncome);
        MutualFundsAssetPassiveIncomeText.text = FormatAmount(MutualFundsPassiveIncome);
        LandAssetPassiveIncomeText.text = FormatAmount(LandPassiveIncome);
        /*    
    public TMP_Text FixedDepositAssetPassiveIncomeText;
    public TMP_Text RealEstateAssetPassiveIncomeText;
    public TMP_Text CryptoAssetPassiveIncomeText;
    public TMP_Text AntiqueAssetPassiveIncomeText;
    public TMP_Text MutualFundsAssetPassiveIncomeText;
    public TMP_Text LandAssetPassiveIncomeText;
            
                                                              public int GoldBondPassiveIncome;
    public int StockPassiveIncome;
    public int FixedDepositPassiveIncome;
    public int RealEstatePassiveIncome;
    public int CryptoPassiveIncome;
    public int AntiquePassiveIncome;
    public int MutualFundsPassiveIncome;
    public int LandPassiveIncome;*/

        // Update costs
        UpdateUICosts(); // Call the method to update costs in UI
        UpdateTotalPortfolioValue();
        TotalPortfolioValueText.text = FormatAmount(TotalPortfolioValue);
        TotalPortfolioValueTextSum.text = FormatAmount(TotalPortfolioValue);
        UpdateTotalPassiveIncome();
        TotalPassiveIncomeText.text = FormatAmount(totalPassiveIncome);
    }

    void UpdateUICosts()
    {
        // Debug.Log("Updating UI Costs...");
        // Update cost text
        GoldBondCostText.text = FormatAmount(GoldBondCost);
        StockCostText.text = FormatAmount(StockCost);
        FixedDepositCostText.text = FormatAmount(FixedDepositCost);
        RealEstateCostText.text = FormatAmount(RealEstateCost);
        CryptoCostText.text = FormatAmount(CryptoCost);
        AntiqueCostText.text = FormatAmount(AntiqueCost);
        MutualFundsCostText.text = FormatAmount(MutualFundsCost);
        LandCostText.text = FormatAmount(LandCost);

        // Debug.Log("GoldBondCostText: " + GoldBondCostText.text);
        // Debug.Log("StockCostText: " + StockCostText.text);
        // Debug.Log("FixedDepositCostText: " + FixedDepositCostText.text);
        // Debug.Log("RealEstateCostText: " + RealEstateCostText.text);
        // Debug.Log("CryptoCostText: " + CryptoCostText.text);
        //Debug.Log("AntiqueCostText: " + AntiqueCostText.text);
        // Debug.Log("MutualFundsCostText: " + MutualFundsCostText.text);
        //Debug.Log("LandCostText: " + LandCostText.text);
    }

    public void BuyAsset(int assetNumber)
    {
        int cost;
        int passiveIncome = 0;
        Character.CanInput = true;
        UpdateTotalPortfolioValue();
        UpdateTotalPassiveIncome();
        UpdateUI();
        
        isPortfolioZero = true;


        switch (assetNumber)
        {
            case 1:
                cost = GoldBondCost;
                passiveIncome = GoldBondPassiveIncome;
                break;
            case 2:
                cost = StockCost;
                passiveIncome = StockPassiveIncome;
                break;
            case 3:
                cost = FixedDepositCost;
                passiveIncome = FixedDepositPassiveIncome;
                break;
            case 4:
                cost = RealEstateCost;
                passiveIncome = RealEstatePassiveIncome;
                break;
            case 5:
                cost = CryptoCost;
                passiveIncome = CryptoPassiveIncome;
                break;
            case 6:
                cost = AntiqueCost;
                passiveIncome = AntiquePassiveIncome;
                break;
            case 7:
                cost = MutualFundsCost;
                passiveIncome = MutualFundsPassiveIncome;
                break;
            case 8:
                cost = LandCost;
                passiveIncome = LandPassiveIncome;
                break;
            default:
                Debug.LogError("Invalid asset number: " + assetNumber);
                return;
        }

        if (GameManager.numberOfCoins >= cost)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.Buy_sound);
            GameManager.numberOfCoins -= cost;
            lastPurchasedAssetCost = cost;

            switch (assetNumber)
            {
                case 1:
                    GoldPortfolio += cost;
                    GoldBondCost += GoldCostIncrement1;
                    GameManager.PassiveIncome += GoldBondPassiveIncome;

                    // Add timed passive income for Gold
                    //make red versions, make other things from notes

                    TotalGoldBondPassiveIncome += GoldBondPassiveIncome;

                    // Increment passive income only from the second purchase onwards
                    if (goldBondPurchases > 0)
                    {
                        GoldBondPassiveIncome += GoldPassiveIncomeIncrement1;
                    }
                    goldBondPurchases++;
                    break;
                case 2: // Stock
                    StockPortfolio += cost;
                    StockCost += StockCostIncrement2;
                    GameManager.PassiveIncome += StockPassiveIncome;

                    // Add timed passive income for Stock
                    timedPassiveIncome.AddTimedIncome("Stock", StockPassiveIncome, 30f);

                    TotalStockPassiveIncome += StockPassiveIncome;

                    // Increment passive income only from the second purchase onwards
                    if (stockPurchases > 0)
                    {
                        StockPassiveIncome += StockPassiveIncomeIncrement2;
                    }
                    stockPurchases++;
                    break;
                case 3:
                    FixedDepositPortfolio += cost;
                    FixedDepositCost += FixedDepositCostIncrement3;
                    GameManager.PassiveIncome += FixedDepositPassiveIncome;
                    TotalFixedDepositPassiveIncome += FixedDepositPassiveIncome;
                    if (fixedDepositPurchases > 0)
                    {
                        FixedDepositPassiveIncome += FixedDepositPassiveIncomeIncrement3;
                    }
                    fixedDepositPurchases++;
                    break;
                case 4:
                    RealEstatePortfolio += cost;
                    RealEstateCost += RealEstateCostIncrement4;
                    GameManager.PassiveIncome += RealEstatePassiveIncome;
                    TotalRealEstatePassiveIncome += RealEstatePassiveIncome;
                    if (realEstatePurchases > 0)
                    {
                        RealEstatePassiveIncome += RealEstatePassiveIncomeIncrement4;
                    }
                    realEstatePurchases++;
                    break;
                case 5:
                    CryptoPortfolio += cost;
                    CryptoCost += CryptoCostIncrement5;
                    GameManager.PassiveIncome += CryptoPassiveIncome;
                    TotalCryptoPassiveIncome += CryptoPassiveIncome;
                    if (cryptoPurchases > 0)
                    {
                        CryptoPassiveIncome += CryptoPassiveIncomeIncrement5;
                    }
                    cryptoPurchases++;
                    break;
                case 6:
                    AntiquePortfolio += cost;
                    AntiqueCost += AntiqueCostIncrement6;
                    GameManager.PassiveIncome += AntiquePassiveIncome;
                    TotalAntiquePassiveIncome += AntiquePassiveIncome;
                    if (antiquePurchases > 0)
                    {
                        AntiquePassiveIncome += AntiquePassiveIncomeIncrement6;
                    }
                    antiquePurchases++;
                    break;
                case 7:
                    MutualFundsPortfolio += cost;
                    MutualFundsCost += MutualFundsCostIncrement7;
                    GameManager.PassiveIncome += MutualFundsPassiveIncome;
                    TotalMutualFundsPassiveIncome += MutualFundsPassiveIncome;
                    if (mutualFundsPurchases > 0)
                    {
                        MutualFundsPassiveIncome += MutualFundsPassiveIncomeIncrement7;
                    }
                    mutualFundsPurchases++;
                    break;
                case 8:
                    LandPortfolio += cost;
                    LandCost += LandCostIncrement8;
                    GameManager.PassiveIncome += LandPassiveIncome;
                    TotalLandPassiveIncome += LandPassiveIncome;
                    if (landPurchases > 0)
                    {
                        LandPassiveIncome += LandPassiveIncomeIncrement8;
                    }
                    landPurchases++;
                    break;
            }

            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough coins to buy asset");
        }
      
        PauseMenu.instance.Resume();
    }

    public static void Flyflyfly()
    {
        // Start slowing down and pausing the game


        // Perform other actions as needed
        // For example:
        // Update UI elements, show asset details, etc.
        Assets assetManager = Object.FindFirstObjectByType<Assets>();
        if (!Assets.first_canvasShown)
        {
            assetManager.ShowAssetCanvasWithCurrentValues();
            Assets.first_canvasShown = true;
            CameraFollow.followSpeed = 1f;

            // Start lerping distanceBehind to -0.27f over 2 seconds
            CameraFollow cameraFollow = Object.FindFirstObjectByType<CameraFollow>();
            // cameraFollow.StartCoroutine(CameraFollow.LerpDistanceBehind(6f, 7f));

            Character.PlayAnimation("run");

        }
        else
        {

            assetManager.ShowAssetCanvasWithCurrentValues();
            //assetManager.IncrementMilestone();
            /*if (Character.isFlying)
            {
                Character.isFlying = false;
                Character.isDescending = true;
            }
            else
            {
                Character.isFlying = true;
                Character.isDescending = false;
                Character.jetpackTimer = Character.jetpackDuration;
                
            .PlayAnimation("Flying");

            }*/
        }
    }


    public void SellAsset(int assetNumber)
    {
        int investedAmount = 0;
        int passiveIncome = 0;
        UpdateTotalPortfolioValue();
        UpdateTotalPassiveIncome();
        UpdateUI();
        AudioManager.instance.PlaySFX(AudioManager.instance.Buy_sound);

        switch (assetNumber)
        {
            case 1:
                investedAmount = GoldPortfolio;
                passiveIncome = GoldBondPassiveIncome;
                GameManager.numberOfCoins += investedAmount;
                GameManager.PassiveIncome -= TotalGoldBondPassiveIncome;
                GoldPortfolio = 0;
                TotalGoldBondPassiveIncome = 0;
                break;
            case 2:
                investedAmount = StockPortfolio;
                passiveIncome = StockPassiveIncome;
                GameManager.numberOfCoins += investedAmount;
                GameManager.PassiveIncome -= TotalStockPassiveIncome;
                StockPortfolio = 0;
                TotalStockPassiveIncome = 0;
                break;
            case 3:
                investedAmount = FixedDepositPortfolio;
                passiveIncome = FixedDepositPassiveIncome;
                GameManager.numberOfCoins += investedAmount;
                GameManager.PassiveIncome -= TotalFixedDepositPassiveIncome;
                FixedDepositPortfolio = 0;
                TotalFixedDepositPassiveIncome = 0;
                break;
            case 4:
                investedAmount = RealEstatePortfolio;
                passiveIncome = RealEstatePassiveIncome;
                GameManager.numberOfCoins += investedAmount;
                GameManager.PassiveIncome -= TotalRealEstatePassiveIncome;
                RealEstatePortfolio = 0;
                TotalRealEstatePassiveIncome = 0;
                break;
            case 5:
                investedAmount = CryptoPortfolio;
                passiveIncome = CryptoPassiveIncome;
                GameManager.numberOfCoins += investedAmount;
                GameManager.PassiveIncome -= TotalCryptoPassiveIncome;
                CryptoPortfolio = 0;
                TotalCryptoPassiveIncome = 0;
                break;
            case 6:
                investedAmount = AntiquePortfolio;
                passiveIncome = AntiquePassiveIncome;
                GameManager.numberOfCoins += investedAmount;
                GameManager.PassiveIncome -= TotalAntiquePassiveIncome;
                AntiquePortfolio = 0;
                TotalAntiquePassiveIncome = 0;
                break;
            case 7:
                investedAmount = MutualFundsPortfolio;
                passiveIncome = MutualFundsPassiveIncome;
                GameManager.numberOfCoins += investedAmount;
                GameManager.PassiveIncome -= TotalMutualFundsPassiveIncome;
                MutualFundsPortfolio = 0;
                TotalMutualFundsPassiveIncome = 0;
                break;
            case 8:
                investedAmount = LandPortfolio;
                passiveIncome = LandPassiveIncome;
                GameManager.numberOfCoins += investedAmount;
                GameManager.PassiveIncome -= TotalLandPassiveIncome;
                LandPortfolio = 0;
                TotalLandPassiveIncome = 0;
                break;
            default:
                Debug.LogError("Invalid asset number: " + assetNumber);
                return;
        }

        UpdateUI();
    }

    void ShowAssetCanvas()
    {
        if (AssetspageFirstShown == true && AssetspageSecondShown == false)
        {
            first_canvasShown = true;
            AssetCanvas.SetActive(true);
            Assets1stPage.SetActive(true);
            Assets2ndPage.SetActive(false);
            CanvasShown = true;
            StartCoroutine(HideAssetCanvasAfterDelay(AssetPanelDuration));
        }

        if (AssetspageFirstShown == false && AssetspageSecondShown == true)
        {
            first_canvasShown = true;
            AssetCanvas.SetActive(true);
            Assets1stPage.SetActive(false);
            Assets2ndPage.SetActive(true);
            CanvasShown = true;
            StartCoroutine(HideAssetCanvasAfterDelay(AssetPanelDuration));
        }

    }
    private string FormatAmount(int amount)
    {
        if (amount >= 10000000)
        {
            return (amount / 10000000f).ToString("0.##") + " Cr";
        }
        else if (amount >= 100000)
        {
            return (amount / 100000f).ToString("0.##") + " L";
        }
        else if (amount >= 1000)
        {
            return (amount / 1000f).ToString("0.##") + " K";
        }
        else
        {
            return amount.ToString("N0");
        }
    }


    public IEnumerator HideAssetCanvasAfterDelay(float delay = 7f)
    {
        yield return new WaitForSeconds(delay);
        AssetCanvas.SetActive(false);
        CanvasShown = false;

        // Get the reference to the Character script
        Character character = Object.FindFirstObjectByType<Character>();
        if (character != null)
        {
            if (Character.isFlying)
            {
                Character.isFlying = false;
                Character.isDescending = true;
            }
        }
    }


    public void Show1stAssetsPage()
    {
        AssetspageFirstShown = true;
        AssetspageSecondShown = false;
        Assets1stPage.gameObject.SetActive(true);
        Assets2ndPage.gameObject.SetActive(false);


    }

    public void Show2ndAssetsPage()
    {
        AssetspageFirstShown = false;
        AssetspageSecondShown = true;
        Assets1stPage.gameObject.SetActive(false);
        Assets2ndPage.gameObject.SetActive(true);
    }
    void UpdateTotalPassiveIncome()
    {
        totalPassiveIncome = TotalGoldBondPassiveIncome +
                             TotalStockPassiveIncome +
                             TotalFixedDepositPassiveIncome +
                             TotalRealEstatePassiveIncome +
                             TotalCryptoPassiveIncome +
                             TotalAntiquePassiveIncome +
                             TotalMutualFundsPassiveIncome +
                             TotalLandPassiveIncome;
    }

    public void UpdateAssetValues()
    {
        // Update invested amounts
        GoldBondInvestedText.text = FormatAmount(GoldPortfolio);
        StockInvestedText.text = FormatAmount(StockPortfolio);
        FixedDepositInvestedText.text = FormatAmount(FixedDepositPortfolio);
        RealEstateInvestedText.text = FormatAmount(RealEstatePortfolio);
        CryptoInvestedText.text = FormatAmount(CryptoPortfolio);
        AntiqueInvestedText.text = FormatAmount(AntiquePortfolio);
        MutualFundsInvestedText.text = FormatAmount(MutualFundsPortfolio);
        LandInvestedText.text = FormatAmount(LandPortfolio);

        // Update passive income
        GoldBondPassiveIncomeText.text = FormatAmount(TotalGoldBondPassiveIncome);
        StockPassiveIncomeText.text = FormatAmount(TotalStockPassiveIncome);
        FixedDepositPassiveIncomeText.text = FormatAmount(TotalFixedDepositPassiveIncome);
        RealEstatePassiveIncomeText.text = FormatAmount(TotalRealEstatePassiveIncome);
        CryptoPassiveIncomeText.text = FormatAmount(TotalCryptoPassiveIncome);
        AntiquePassiveIncomeText.text = FormatAmount(TotalAntiquePassiveIncome);
        MutualFundsPassiveIncomeText.text = FormatAmount(TotalMutualFundsPassiveIncome);
        LandPassiveIncomeText.text = FormatAmount(TotalLandPassiveIncome);

        // Update cost
        UpdateUICosts();
        UpdateTotalPortfolioValue();
        TotalPortfolioValueText.text = FormatAmount(TotalPortfolioValue);
        TotalPortfolioValueTextSum.text = FormatAmount(TotalPortfolioValue);

    }
}