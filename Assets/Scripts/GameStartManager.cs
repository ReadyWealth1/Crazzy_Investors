using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using com.jiogames.wrapper;
using DG.Tweening.Core.Easing;

public class GameStartManager : MonoBehaviour
{
    // public static GameStartManager Instance { get; private set; }
    [Header("Character Display Name")]
    public TextMeshProUGUI selectedCharacterNameText; // Assign in inspector
    public Dictionary<string, string> characterDisplayNames = new Dictionary<string, string>()
{
   /* { "boy", "Zen" },
    { "girl", "Luna" },
    { "newBoy", "NewZen" },
    { "newGirl", "NewLuna" },
    { "Gwen", "Gwen" },
    { "Witch", "Drucilia" },
    { "EgyptQueen", "Amelia" },*/
    { "Elon", "Melon Tusk" },
   /* { "MJ", "MJ" },
    { "Hotb", "Sal" },
    { "Hotg", "Ruby" },
    { "Chubbs", "Chubbs" },*/
    { "OfficeGirl", "Natasha" },
   // { "Mansa", "Mansa Musa" }
};

    public CharacterViewer characterViewer;
    [Header("Costs for Characters")]
/*    public int newBoyCost = 1000;
    public int newGirlCost = 2000;
    public int GwenCost = 3000;
    public int WitchCost = 3000;
    public int EgyptQueenCost = 3000;*/
    public int ElonCost = 3000;
 /*   public int MansaCost = 3000;
    public int HotbCost = 3000;
    public int HotgCost = 3000;
    public int MJCost = 3000;
    public int ChubbsCost = 3000;*/
    public int OfficeGirlCost = 3000;

    [Header("UI Elements")]
    public GameObject homePageUI;
    public GameObject inGameUI;
    public Button startButton;
    public bool isfristtimeOpen = false;
    [Header("Character Buttons")]
   /* public Button boyButton;
    public Button girlButton;
    public Button newBoyButton;
    public Button newGirlButton;
    public Button GwenButton;
    public Button WitchButton;
    public Button EgyptQueenButton;*/
    public Button ElonButton;
 /*   public Button MansaButton;
    public Button HotbButton;
    public Button HotgButton;
    public Button MJButton;
    public Button ChubbsButton;*/
    public Button OfficeGirlButton;




    [Header("Menu Characters (Start Menu)")]
   /* public GameObject boyCharacterMenu;
    public GameObject girlCharacterMenu;
    public GameObject newBoyCharacterMenu;
    public GameObject newGirlCharacterMenu;
    public GameObject GwenCharacterMenu;
    public GameObject WitchCharacterMenu;
    public GameObject EgyptQueenCharacterMenu;*/
    public GameObject ElonCharacterMenu;
    /*public GameObject MansaCharacterMenu;
    public GameObject HotbCharacterMenu;
    public GameObject HotgCharacterMenu;
    public GameObject MJCharacterMenu;
    public GameObject ChubbsCharacterMenu;*/
    public GameObject OfficeGirlCharacterMenu;


    [Header("In-Game Characters")]
   /* public GameObject boyCharacterGame;
    public GameObject girlCharacterGame;
    public GameObject newBoyCharacterGame;
    public GameObject newGirlCharacterGame;
    public GameObject GwenCharacterGame;
    public GameObject WitchCharacterGame;
    public GameObject EgyptQueenCharacterGame;*/
    public GameObject ElonCharacterGame;
  /*  public GameObject MansaCharacterGame;
    public GameObject HotbCharacterGame;
    public GameObject HotgCharacterGame;
    public GameObject MJCharacterGame;
    public GameObject ChubbsCharacterGame;*/
    public GameObject OfficeGirlCharacterGame;


    [Header("Tutorial Characters")]
  /*  public GameObject tutorialBoyCharacter;
    public GameObject tutorialGirlCharacter;
    public GameObject tutorialNewBoyCharacter;
    public GameObject tutorialNewGirlCharacter;
    public GameObject tutorialGwenCharacter;
    public GameObject tutorialWitchCharacter;
    public GameObject tutorialEgyptQueenCharacter;
    */
    public GameObject tutorialElonCharacter;
/*    public GameObject tutorialMansaCharacter;
    public GameObject tutorialHotbCharacter;
    public GameObject tutorialHotgCharacter;
    public GameObject tutorialMJCharacter;
    public GameObject tutorialChubbsCharacter;*/
    public GameObject tutorialOfficeGirlCharacter;


    [Header("Character Position Targets")]
/*    public GameObject boyPositionTarget;
    public GameObject girlPositionTarget;
    public GameObject newBoyPositionTarget;
    public GameObject newGirlPositionTarget;
    public GameObject GwenPositionTarget;
    public GameObject WitchPositionTarget;
    public GameObject EgyptQueenPositionTarget;*/
    public GameObject ElonPositionTarget;
  /*  public GameObject MansaPositionTarget;
    public GameObject HotbPositionTarget;
    public GameObject HotgPositionTarget;
    public GameObject MJPositionTarget;
    public GameObject ChubbsPositionTarget;*/
    public GameObject OfficeGirlPositionTarget;


    [Header("Camera and Follow Scripts")]
    [SerializeField] private CameraFollow cameraFollow; // Main game camera
    [SerializeField] private CameraFollow mainCameraFollow; // Reference to the main game camera
    [SerializeField] private CameraFollow tutorialCameraFollow; // Reference to the tutorial camera

    [Header("Animation Controllers")]
    public Animator startButtonAnimator;
    public Animator GlossaryAnimator;
    public Animator Character_Selector_Animator;
    public Animator TItle_Animator;
    public Animator Settings_Animator;
    public Animator Highscore_Animator;

    [Header("Other References")]
    public GameObject selectSphere; // Reference to the SelectSphere GameObject
    public GameObject tutorialGameObject; // Reference to the tutorial game object
    public GroundSpawnerTest groundSpawner; // Ground spawner reference
    private GameObject selectedCharacter;
    private Transform selectedCharacterTransform;
    public Canvas CharSelectCanvas;



    /*    public  TextMeshProUGUI gemText;
        public  TextMeshProUGUI gemText_InChar_View;*/

    // The single big button and its text
    public GameObject Diamond;
    public Button actionButton;
    public TextMeshProUGUI actionButtonText;

    // ADDED: these track which character is currently "viewed" for the single button logic
    public string currentViewedCharacter = "";
    public int currentViewedCharacterCost = 0;
    private int diamonds ;
    private bool inHomeScreen = true;

    [SerializeField] private Tutorial_Object_Manager tutorialObjectManager;
    public void QuitApp()
    {
        /*  if (PlayerPrefs.GetInt("GoogleLoggedIn") == 1)
          {
              DataSaver.Instance.SaveDataFn();
          }*/

        Application.Quit();
    }
    /* private void Awake()
     {
         if (Instance == null)
         {
             Instance = this;
             DontDestroyOnLoad(gameObject);

         }
         *//* else
          {
              Destroy(gameObject);
          }*//*
     }*/

    /*   private void OnEnable()
       {
           DataSaver.Instance.OnFirebaseDataLoaded += LoadUserData;
       }

       private void OnDisable()
       {
           DataSaver.Instance.OnFirebaseDataLoaded -= LoadUserData;
       }
   */
    private void LoadUserData()
    {
        /*if (PlayerPrefs.GetInt("GoogleLoggedIn") == 1 && DataSaver.Instance.UserData != null)
        {
            diamonds = DataSaver.Instance.UserData.diamonds;
            UiManager.Instance.UpdateNameText(DataSaver.Instance.UserData.userName);
            UiManager.Instance.DiamondUpdate(diamonds);
        }
        else
        {
            Debug.LogWarning("UserData is null or not Google logged in!");
        }*/
    }

    void Start()
    {
        //  cameraFollow = FindAnyObjectByType<CameraFollow>();
        //  mainCameraFollow = GameObject.FindWithTag("MainCamera")?.GetComponent<CameraFollow>();
        //  tutorialCameraFollow = GameObject.FindWithTag("TutorialCameraFollow")?.GetComponent<CameraFollow>();
        FristtimeOpen();

       

            //  diamonds = DataSaver.Instance.GuestData.diamonds;
            UiManager.Instance.DiamondUpdate(PlayerPrefs.GetInt("UserGems"));
        
        //else if (PlayerPrefs.HasKey("GuestLogin"))
        //{
        //    Debug.Log("Game Start manager user id on start: " + DataSaver.Instance.userId);

        //    diamonds = DataSaver.Instance.UserData.diamonds;
        //    UiManager.Instance.DiamondUpdate(diamonds);
        //    UiManager.Instance.UpdateNameText(DataSaver.Instance.UserData.userName);
        //}
        //character = GetComponent<Character>();

        if (!PlayerPrefs.HasKey("SelectedCharacter"))
        {
            PlayerPrefs.SetString("SelectedCharacter", "Elon");
            PlayerPrefs.SetInt("ElonBought", 1);
            PlayerPrefs.Save();
        }
        string initialChar = PlayerPrefs.GetString("SelectedCharacter", "Elon");

        if (characterDisplayNames.TryGetValue(initialChar, out string initialName))
        {
            selectedCharacterNameText.text = initialName;
        }


        if (actionButton != null)

            actionButton.onClick.AddListener(OnActionButtonClicked);

        // 1) (Optional) Load user coins from PlayerPrefs if you're persisting them
        // userCoins = PlayerPrefs.GetInt("UserCoins", 5000);

        // 2) Update coin text if you have a coinText

       
        startButton.onClick.AddListener(StartTutorialCall);
        // 3) Start button logic
        if (startButton != null)
         {
             if (PlayerPrefs.GetInt("FirstTimeOpen") == 1)
             {
                 startButton.onClick.AddListener(StartTutorial);
             }
             else
             {
                 startButton.onClick.AddListener(StartGame);
             }
         }

        // 4) Set up newGirl & newBoy purchase logic
     /*   if (newGirlButton != null)
        {
            newGirlButton.onClick.AddListener(() => OnThumbnailClicked("newGirl"));
        }
        if (newBoyButton != null)
        {
            newBoyButton.onClick.AddListener(() => OnThumbnailClicked("newBoy"));
        }
        if (boyButton != null)
        {
            boyButton.onClick.AddListener(() => OnThumbnailClicked("boy"));
        }
        if (girlButton != null)
        {
            girlButton.onClick.AddListener(() => OnThumbnailClicked("girl"));
        }
        if (GwenButton != null)
        {
            GwenButton.onClick.AddListener(() => OnThumbnailClicked("Gwen"));
        }
        if (WitchButton != null)
        {
            WitchButton.onClick.AddListener(() => OnThumbnailClicked("Witch"));
        }
        if (EgyptQueenButton != null)
        {
            EgyptQueenButton.onClick.AddListener(() => OnThumbnailClicked("EgyptQueen"));
        }*/
        if (ElonButton != null)
        {
            ElonButton.onClick.AddListener(() => OnThumbnailClicked("Elon"));
        }
     /*   if (MansaButton != null)
        {
            MansaButton.onClick.AddListener(() => OnThumbnailClicked("Mansa"));
        }
        if (HotbButton != null)
        {
            HotbButton.onClick.AddListener(() => OnThumbnailClicked("Hotb"));
        }
        if (HotgButton != null)
        {
            HotgButton.onClick.AddListener(() => OnThumbnailClicked("Hotg"));
        }
        if (MJButton != null)
        {
            MJButton.onClick.AddListener(() => OnThumbnailClicked("MJ"));
        }
        if (ChubbsButton != null)
        {
            ChubbsButton.onClick.AddListener(() => OnThumbnailClicked("Chubbs"));
        }*/
        if (OfficeGirlButton != null)
        {
            OfficeGirlButton.onClick.AddListener(() => OnThumbnailClicked("OfficeGirl"));
        }


        // 6) Hide/show the correct UI screens at the start
        homePageUI.SetActive(true);
        PlayerPrefs.SetInt("isHomeOpen", 1);
        // IronSourceAdsManager.instance.ShowBanner();
        inGameUI.SetActive(false);
        //StartCoroutine(ShowBannerWithDelay());
        tutorialGameObject.SetActive(false);
        PlayerPrefs.SetInt("GuestLogin", 1);
        // 7) Hide tutorial chars if not needed
        //tutorialBoyCharacter.SetActive(false);
        //
        //
        //tutorialGirlCharacter.SetActive(false);

        PlayerPrefs.SetInt("ElonBought", 1);

        // 8) Load whichever character was saved

        LoadSelectedCharacter();

        // 9) Refresh button states
        RefreshAllCharacterButtons();
        // characterViewer.RefreshThumbnailTicks();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inHomeScreen == true)
        {
            Toast.Instance.Open_Quit_Panel();
        }
    }
    /* public static UpdateGems(int diamondCount)
     {
         gemText_InChar_View.text = diamondCount.ToString();
     }*/
    /*public void LeaveToHomeScreen()
    {
        homePageUI.SetActive(true);
        inGameUI.SetActive(false);
        tutorialGameObject.SetActive(false);
    }*/
    public void StartTutorialCall()
    {
      /*  JioWrapperJS.Instance.cacheInterstitial();*/

        if (PlayerPrefs.GetInt("FirstTimeOpen") == 1)
        {
           
            StartTutorial();
            // startButton.onClick.AddListener(StartTutorial);

        }
        else
        {
         
            StartGame();
            //startButton.onClick.AddListener(StartGame);

        }
    }
    public void FristtimeOpen()
    {
        if(PlayerPrefs.GetInt("isfristtimeOpen")==0)
        {
            PlayerPrefs.SetInt("GuestLogin", 1);
            PlayerPrefs.SetInt("FirstTimeOpen", 1);
            Debug.Log("====FirstTimeOpen Call", this);
            //  Toast.Instance.ShowSpinMessage("you got" + 20 + " Diamonds");
          
            //  PlayerPrefs.SetInt("FirstTimeOpen", 1);
            DataSaver.Instance.GuestData.diamonds = 0;
          
            UiManager.Instance.DiamondUpdate(0);
            isfristtimeOpen = true; 
            PlayerPrefs.SetInt("isfristtimeOpen",1);
        }
        else
        {
            Debug.Log("already opende");
        }
       
    }
    public void ResetAllPurchasesAndGems()
    {
        // reset user gems in PlayerPrefs
      

        // reset purchased flags
        PlayerPrefs.DeleteKey("newBoyBought");
        PlayerPrefs.DeleteKey("newGirlBought");
        PlayerPrefs.DeleteKey("WitchBought");
        PlayerPrefs.DeleteKey("GwenBought");
        PlayerPrefs.DeleteKey("EgyptQueenBought");
        PlayerPrefs.DeleteKey("ElonBought");
        PlayerPrefs.DeleteKey("MansaBought");
        PlayerPrefs.DeleteKey("HotbBought");
        PlayerPrefs.DeleteKey("HotgBought");
        PlayerPrefs.DeleteKey("MJBought");
        PlayerPrefs.DeleteKey("ChubbsBought");
        PlayerPrefs.DeleteKey("OfficeGirlBought");

        // PlayerPrefs.DeleteKey("boyBought"); 
        // PlayerPrefs.DeleteKey("girlBought"); if needed

        // reset selected char
        PlayerPrefs.SetString("SelectedCharacter", "Elon");
        PlayerPrefs.Save();

        /* if (PlayerPrefs.GetInt("GuestLogin") == 1)
         {
             DataSaver.Instance.UserData.diamonds = 5000; // or 0, whatever your default is
         }
         else
         {
             DataSaver.Instance.GuestData.diamonds = 5000; // or 0, whatever your default is
         }*/

        Debug.Log("All purchases and gems have been reset!");
        RefreshAllCharacterButtons();
    }
    IEnumerator ShowBannerWithDelay()
    {
        yield return new WaitForSeconds(2f); // Wait before showing banner
                                             // IronSourceAdsManager.instance.ShowBanner();
    }

    // ADDED: A simple function if you have small thumbnail buttons
    // for each character. They call this with their own key + cost.
    public void OnThumbnailClicked(string charKey)
    {
        // Look up the cost in the dictionary
        if (characterCosts.TryGetValue(charKey, out int cost))
        {
            currentViewedCharacter = charKey;
            currentViewedCharacterCost = cost;
            RefreshBigActionButton();
        }
        else
        {
            Debug.LogError($"[OnThumbnailClicked] No entry in dictionary for key '{charKey}'");
        }
        if (selectedCharacterNameText != null)
        {
            if (characterDisplayNames.TryGetValue(charKey, out string displayName))
            {
                selectedCharacterNameText.text = displayName;
            }
            else
            {
                selectedCharacterNameText.text = "Adventurer";
            }
        }
    }

    [Header("Character Costs")]

    public Dictionary<string, int> characterCosts = new Dictionary<string, int>
    {
     { "boy", 0 },
    { "girl", 0 },
    { "newBoy", 0 },
    { "newGirl", 0 },
    { "Gwen", 0 },
    { "Witch", 0 },
    { "EgyptQueen", 0 },
    { "Elon", 0 },
    { "Mansa", 0 },
    { "Hotb", 0},
    { "Hotg", 0 },
    { "MJ", 0 },
    { "Chubbs", 0 },
    { "OfficeGirl", 500 }

    };
    private Character character;

    public string GetCurrentlySelectedCharacter()
    {
        // Return whichever is stored, default to "boy" if none
        return PlayerPrefs.GetString("SelectedCharacter", "Elon");
    }


    public void RefreshBigActionButton()
    {
        // If no character is viewed, disable or hide the button
        if (string.IsNullOrEmpty(currentViewedCharacter))
        {
            actionButtonText.text = "";
            actionButton.interactable = false;
            return;
        }

        // Check if purchased
        bool isPurchased = IsCharacterBought(currentViewedCharacter)
                           || currentViewedCharacterCost == 0;

        // Who's currently selected?
        string currentSelected = PlayerPrefs.GetString("SelectedCharacter", "boy");

        if (!isPurchased)
        {
            // Not purchased => show cost
            actionButtonText.text = $"{currentViewedCharacterCost}";
            actionButton.interactable = true;
            Diamond.gameObject.SetActive(true);
        }
        else
        {
            // If purchased
            if (currentSelected == currentViewedCharacter)
            {
                // Already selected
                actionButtonText.text = "Selected";
                actionButton.interactable = false;
            }
            else
            {
                // Owned but not currently selected
                actionButtonText.text = "Select";
                actionButton.interactable = true;
            }
            Diamond.gameObject.SetActive(false);
        }
    }

    private void OnActionButtonClicked()
    {
        // If no character is viewed, do nothing
        if (string.IsNullOrEmpty(currentViewedCharacter)) return;

        string buttonLabel = actionButtonText.text;

        if (buttonLabel.Contains($"{currentViewedCharacterCost}"))
        {
            // The user wants to buy
            AttemptToBuy(currentViewedCharacter, currentViewedCharacterCost);
        }
        else if (buttonLabel == "Select")
        {
            // The user wants to select
            SelectCharacter(currentViewedCharacter);
            // Optionally refresh
            RefreshBigActionButton();
        }
        else if (buttonLabel == "Selected")
        {
            Debug.Log("Character is already selected!");
        }
    }
    private void AttemptToBuy(string charKey, int cost)
    {
       /* if (PlayerPrefs.GetInt("GuestLogin") == 1)
        {*/
            diamonds = PlayerPrefs.GetInt("UserGems");

        //}
        /*else
        {
            diamonds = DataSaver.Instance.UserData.diamonds;
        }*/
        if (diamonds >= cost)
        {
            // subtract the cost

            // Save new gem total to PlayerPrefs
            /*if (PlayerPrefs.GetInt("GuestLogin") == 1)
            {*/
                diamonds = PlayerPrefs.GetInt("UserGems");
                diamonds -= cost;
                PlayerPrefs.SetInt("UserGems", diamonds);
                //DataSaver.Instance.GuestData.diamonds = diamonds;

                PlayerPrefs.Save();
                UiManager.Instance.DiamondUpdate(diamonds);
            //}
            /* else
             {
                 diamonds = DataSaver.Instance.UserData.diamonds;
                 diamonds -= cost;
                 UiManager.Instance.DiamondUpdate(diamonds);
                 DataSaver.Instance.UserData.diamonds = diamonds;
             }*/




            // Mark the char as purchased
            PlayerPrefs.SetInt($"{charKey}Bought", 1);
            PlayerPrefs.Save();

            // Optionally select it right away
            SelectCharacter(charKey);

            RefreshBigActionButton();
            characterViewer.RefreshThumbnailTicks();
        }
        else
        {
            Toast.Instance.ShowAdsPopup();
            Debug.Log("Not enough gems to buy " + charKey);
        }
    }
    /*
        private void OnNewBoyButtonClicked()
        {
            // If not purchased yet, either show buy UI or buy directly:
            if (!IsCharacterBought("newBoy"))
            {
                // Show purchase logic or a confirmation popup:
                // E.g. if user has enough coins:
                if (userGems >= newBoyCost)
                {
                    // Deduct cost
                    userGems -= newBoyCost;
                    PlayerPrefs.SetInt("UserGems", userGems);

                    // Mark as purchased
                    PlayerPrefs.SetInt("newBoyBought", 1);
                    PlayerPrefs.Save();

                    // Now that it's purchased, let them select
                    SelectCharacter(newBoyCharacterMenu, newBoyCharacterGame.transform, "newBoy", newBoyPositionTarget.transform.position);
                }
                else
                {
                    Debug.Log("Not enough coins to buy newBoy!");
                    // Or open a "not enough coins" message
                }
            }
            else
            {
                // Already purchased => just select
                SelectCharacter(newBoyCharacterMenu, newBoyCharacterGame.transform, "newBoy", newBoyPositionTarget.transform.position);
            }

            RefreshAllCharacterButtons();
        }*/

    /*
        private void OnNewGirlButtonClicked()
        {
            // Check if "newGirl" is already purchased
            if (!IsCharacterBought("newGirl"))
            {
                // Not purchased yet => attempt to buy
                if (userGems >= newGirlCost)
                {
                    // Deduct coins
                    userGems -= newGirlCost;
                    PlayerPrefs.SetInt("UserGems", 5000);

                    // Mark as purchased => MUST set to 1
                    PlayerPrefs.SetInt("newGirlBought", 1);
                    PlayerPrefs.Save();

                    // Now that it's purchased, we can select it
                    SelectCharacter(newGirlCharacterMenu, newGirlCharacterGame.transform,
                                    "newGirl", newGirlPositionTarget.transform.position);
                }
                else
                {
                    Debug.Log("Not enough coins to buy newGirl!");
                    // Optional: Show a popup or UI indicating insufficient coins
                }
            }
            else
            {
                // Already purchased => just select
                SelectCharacter(newGirlCharacterMenu, newGirlCharacterGame.transform,
                                "newGirl", newGirlPositionTarget.transform.position);
            }

            // Refresh UI states
            RefreshAllCharacterButtons();
        }
    */
    public bool IsCharacterBought(string charKey)
    {
        // We'll match our PlayerPrefs usage:
        // "newBoy" => "newBoyBought" 
        // "newGirl" => "newGirlBought"
        // "boy" => free? or "boyBought" if you want to track that
        // "girl" => free? or "girlBought"
        // Return true if stored int = 1
        return PlayerPrefs.GetInt($"{charKey}Bought", 0) == 1;
    }

    public void StartTutorial()
    {
        if (selectedCharacter == null || selectedCharacterTransform == null)
        {
            Debug.LogError("No character selected!");
            return;
        }

        if (groundSpawner != null)
        {
            GroundSpawnerTest.isTutorialMode = true;
            tutorialObjectManager.DisableTutorialObjectsIfInTutorialMode();
            Debug.Log("Tutorial mode enabled!");
        }
        else
        {
            Debug.LogError("GroundSpawnerTest reference is missing!");
        }

        /*if (startButtonAnimator != null)
        {
            startButtonAnimator.SetTrigger("Start");
        }*/


        PlayerPrefs.Save();

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartGame);

        TransitionToTutorialUI();// problem here
        LoadSelectedCharacter();
    }

    public void EndTutorial()
    {
        if (groundSpawner != null)
        {
            GroundSpawnerTest.isTutorialMode = false;
            Debug.Log("Tutorial mode disabled!");
        }

        StartGame();
    }

    private void TransitionToTutorialUI()
    {
        //yield return new WaitForSeconds(3.5f);

        homePageUI.SetActive(false);
        Debug.Log("HomePageUi Off");

        //IronSourceAdsManager.instance.HideBanner();
        //IronSourceAdsManager.instance.DestroyBanner();
        tutorialGameObject.SetActive(true);
        Debug.Log("tutorialGameObject.SetActive(true);");

        //tutorialBoyCharacter.SetActive(false);
        //tutorialGirlCharacter.SetActive(false);
        //tutorialNewGirlCharacter.SetActive(false);
        //tutorialNewBoyCharacter.SetActive(false);
        Debug.Log("tutorial(some)Character.SetActive(false);till newboy");
        //tutorialGwenCharacter.SetActive(false);
        //tutorialEgyptQueenCharacter.SetActive(false);
        //tutorialWitchCharacter.SetActive(false);
        //tutorialElonCharacter.SetActive(false);
        //tutorialMansaCharacter.SetActive(false);

        //tutorialHotbCharacter.SetActive(false);
        //tutorialHotgCharacter.SetActive(false);
        //tutorialMJCharacter.SetActive(false);
        //tutorialChubbsCharacter.SetActive(false);
        //tutorialOfficeGirlCharacter.SetActive(false);

       /* if (selectedCharacterTransform == boyCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(true);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialBoyCharacter.transform;
        }*/
        /*else if (selectedCharacterTransform == girlCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(true);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialGirlCharacter.transform;
        }
        else if (selectedCharacterTransform == newGirlCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(true);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialNewGirlCharacter.transform;
        }
        else if (selectedCharacterTransform == newBoyCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(true);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialNewBoyCharacter.transform;
        }
        else if (selectedCharacterTransform == GwenCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(true);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialGwenCharacter.transform;
        }
        else if (selectedCharacterTransform == EgyptQueenCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(true);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialEgyptQueenCharacter.transform;
        }
        else if (selectedCharacterTransform == WitchCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(true);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialWitchCharacter.transform;
        }*/
        if (selectedCharacterTransform == ElonCharacterGame.transform)
        {
          /*  tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);*/
            tutorialElonCharacter.SetActive(true);
            /*tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);*/
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialElonCharacter.transform;
        }
        /*else if (selectedCharacterTransform == MansaCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(true);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialMansaCharacter.transform;
        }
        else if (selectedCharacterTransform == HotbCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(true);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialHotbCharacter.transform;
        }
        else if (selectedCharacterTransform == HotgCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(true);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialHotgCharacter.transform;
        }
        else if (selectedCharacterTransform == MJCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(true);
            tutorialChubbsCharacter.SetActive(false);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialMJCharacter.transform;
        }
        else if (selectedCharacterTransform == ChubbsCharacterGame.transform)
        {
            tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);
            tutorialElonCharacter.SetActive(false);
            tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(true);
            tutorialOfficeGirlCharacter.SetActive(false);
            tutorialCameraFollow.target = tutorialChubbsCharacter.transform;
        }*/
        else if (selectedCharacterTransform == OfficeGirlCharacterGame.transform)
        {
            /*tutorialBoyCharacter.SetActive(false);
            tutorialGirlCharacter.SetActive(false);
            tutorialNewGirlCharacter.SetActive(false);
            tutorialNewBoyCharacter.SetActive(false);
            tutorialGwenCharacter.SetActive(false);
            tutorialEgyptQueenCharacter.SetActive(false);
            tutorialWitchCharacter.SetActive(false);*/
            tutorialElonCharacter.SetActive(false);
            /*tutorialMansaCharacter.SetActive(false);
            tutorialHotbCharacter.SetActive(false);
            tutorialHotgCharacter.SetActive(false);
            tutorialMJCharacter.SetActive(false);
            tutorialChubbsCharacter.SetActive(false);*/
            tutorialOfficeGirlCharacter.SetActive(true);
            tutorialCameraFollow.target = tutorialOfficeGirlCharacter.transform;
        }



        Debug.Log("after all else ifs");
        mainCameraFollow.enabled = false;
        Debug.Log("mainCameraFollow.enabled = false;");

        tutorialCameraFollow.enabled = true;
        Debug.Log("tutorialCameraFollow.enabled = true;");

    }

    public void StartGame()
    {
        if (selectedCharacter == null || selectedCharacterTransform == null)
        {
            Debug.LogError("No character selected!");
            return;
        }

        if (startButtonAnimator != null)
        {
            startButtonAnimator.SetTrigger("Start");
            Debug.Log("  startButtonAnimator.SetTrigger(\"Start\");");
        }
        inHomeScreen = false;
        StartCoroutine(TransitionToInGameUI());
        ResetAssets();
    }

    private IEnumerator TransitionToInGameUI()
    {
        yield return new WaitForSeconds(3.5f);

        homePageUI.SetActive(false);
        inGameUI.SetActive(true);
        PlayerPrefs.SetInt("isHomeOpen", 0);
        //IronSourceAdsManager.instance.DestroyBanner();


        DeactivateUnselectedCharacters();
    }

    private void DeactivateUnselectedCharacters()
    {/*
        if (selectedCharacterTransform == boyCharacterGame.transform)
        {
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == girlCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == newGirlCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == newBoyCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == GwenCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == EgyptQueenCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == WitchCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }*/
         if (selectedCharacterTransform == ElonCharacterGame.transform)
        {
           /* boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);*/
            OfficeGirlCharacterGame?.SetActive(false);
        }
        /*else if (selectedCharacterTransform == MansaCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == HotbCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == HotgCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }
        else if (selectedCharacterTransform == MJCharacterGame.transform)
        {
            boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);
            ElonCharacterGame?.SetActive(false);
            MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);
            OfficeGirlCharacterGame?.SetActive(false);
        }*/
        else if (selectedCharacterTransform == OfficeGirlCharacterGame.transform)
        {
           /* boyCharacterGame?.SetActive(false);
            girlCharacterGame?.SetActive(false);
            newGirlCharacterGame?.SetActive(false);
            newBoyCharacterGame?.SetActive(false);
            EgyptQueenCharacterGame?.SetActive(false);
            GwenCharacterGame?.SetActive(false);
            WitchCharacterGame?.SetActive(false);*/
            ElonCharacterGame?.SetActive(false);
           /* MansaCharacterGame?.SetActive(false);
            HotbCharacterGame?.SetActive(false);
            HotgCharacterGame?.SetActive(false);
            MJCharacterGame?.SetActive(false);
            ChubbsCharacterGame?.SetActive(false);*/
        }
    }


    private void SelectCharacter(string charKey)
    {
        switch (charKey)
        {
            /*case "boy":
                SelectCharacter(boyCharacterMenu, boyCharacterGame.transform, "boy", boyPositionTarget.transform.position);
                break;

            case "girl":
                SelectCharacter(girlCharacterMenu, girlCharacterGame.transform, "girl", girlPositionTarget.transform.position);
                break;

            case "newBoy":
                SelectCharacter(newBoyCharacterMenu, newBoyCharacterGame.transform, "newBoy", newBoyPositionTarget.transform.position);
                break;

            case "newGirl":
                SelectCharacter(newGirlCharacterMenu, newGirlCharacterGame.transform, "newGirl", newGirlPositionTarget.transform.position);
                break;

            case "Gwen":
                SelectCharacter(GwenCharacterMenu, GwenCharacterGame.transform, "Gwen", GwenPositionTarget.transform.position);
                break;

            case "EgyptQueen":
                SelectCharacter(EgyptQueenCharacterMenu, EgyptQueenCharacterGame.transform, "EgyptQueen", EgyptQueenPositionTarget.transform.position);
                break;

            case "Witch":
                SelectCharacter(WitchCharacterMenu, WitchCharacterGame.transform, "Witch", WitchPositionTarget.transform.position);
                break;
*/
            case "Elon":
                SelectCharacter(ElonCharacterMenu, ElonCharacterGame.transform, "Elon", ElonPositionTarget.transform.position);
                break;

            /*case "Mansa":
                SelectCharacter(MansaCharacterMenu, MansaCharacterGame.transform, "Mansa", MansaPositionTarget.transform.position);
                break;

            case "Hotb":
                SelectCharacter(HotbCharacterMenu, HotbCharacterGame.transform, "Hotb", HotbPositionTarget.transform.position);
                break;

            case "Hotg":
                SelectCharacter(HotgCharacterMenu, HotgCharacterGame.transform, "Hotg", HotgPositionTarget.transform.position);
                break;

            case "MJ":
                SelectCharacter(MJCharacterMenu, MJCharacterGame.transform, "MJ", MJPositionTarget.transform.position);
                break;

            case "Chubbs":
                SelectCharacter(ChubbsCharacterMenu, ChubbsCharacterGame.transform, "Chubbs", ChubbsPositionTarget.transform.position);
                break;*/

            case "OfficeGirl":
                SelectCharacter(OfficeGirlCharacterMenu, OfficeGirlCharacterGame.transform, "OfficeGirl", OfficeGirlPositionTarget.transform.position);
                break;

            default:
                Debug.LogWarning("Unknown charKey: " + charKey);
                break;
        }
    }


    private void SelectCharacter(
    GameObject characterMenu,
    Transform characterTransform,
    string characterType,
    Vector3 spherePosition)
    {
        // If the character is not bought, we do NOT want to allow selection.
        // But you already handle the buy logic in the button click above.
        if (!IsCharacterBought(characterType) && (characterType == "newBoy" || characterType == "newGirl"))
        {
            Debug.Log("This character is not purchased yet. Cannot select!");
            return;
        }

        selectedCharacter = characterMenu;
        selectedCharacterTransform = characterTransform;
        character = selectedCharacterTransform.GetComponent<Character>();
        if (character == null)
        {
            Debug.LogError($"Character component not found on {characterTransform.name}");
        }

        // Enable/disable the correct character menu objects, e.g.:
       /* boyCharacterMenu.SetActive(characterMenu == boyCharacterMenu);
        girlCharacterMenu.SetActive(characterMenu == girlCharacterMenu);
        newBoyCharacterMenu.SetActive(characterMenu == newBoyCharacterMenu);
        newGirlCharacterMenu.SetActive(characterMenu == newGirlCharacterMenu);
        GwenCharacterMenu.SetActive(characterMenu == GwenCharacterMenu);
        EgyptQueenCharacterMenu.SetActive(characterMenu == EgyptQueenCharacterMenu);
        WitchCharacterMenu.SetActive(characterMenu == WitchCharacterMenu);*/
        ElonCharacterMenu.SetActive(characterMenu == ElonCharacterMenu);
       /* MansaCharacterMenu.SetActive(characterMenu == MansaCharacterMenu);
        HotbCharacterMenu.SetActive(characterMenu == HotbCharacterMenu);
        HotgCharacterMenu.SetActive(characterMenu == HotgCharacterMenu);
        MJCharacterMenu.SetActive(characterMenu == MJCharacterMenu);
        ChubbsCharacterMenu.SetActive(characterMenu == ChubbsCharacterMenu);*/
        OfficeGirlCharacterMenu.SetActive(characterMenu == OfficeGirlCharacterMenu);


        // Move your selection sphere, etc.
        //LeanTween.move(selectSphere, spherePosition, 0.2f);
        selectSphere.transform.position = spherePosition;

        // Attach the camera follow
        cameraFollow.target = characterTransform;
        character = selectedCharacterTransform.GetComponent<Character>();
        // Save which character is selected in PlayerPrefs
        PlayerPrefs.SetString("SelectedCharacter", characterType);
        PlayerPrefs.Save();

        // Update UI
        RefreshAllCharacterButtons();

    }
    public void RefreshAllCharacterButtons()
    {
/*        // 1) newBoy
        RefreshButtonState(newBoyButton, "newBoy", newBoyCost);

        // 2) newGirl
        RefreshButtonState(newGirlButton, "newGirl", newGirlCost);

        // 3) classic boy
        RefreshButtonState(boyButton, "boy", 0);   // 0 cost => free or auto-bought

        // 4) classic girl
        RefreshButtonState(girlButton, "girl", 0); // same as above*/
/*
        RefreshButtonState(GwenButton, "Gwen", GwenCost);
        RefreshButtonState(WitchButton, "Witch", WitchCost);
        RefreshButtonState(EgyptQueenButton, "EgyptQueen", EgyptQueenCost);*/
        RefreshButtonState(ElonButton, "Elon", ElonCost);
  /*      RefreshButtonState(MansaButton, "Mansa", MansaCost);
        RefreshButtonState(HotbButton, "Hotb", HotbCost);
        RefreshButtonState(HotgButton, "Hotg", HotgCost);
        RefreshButtonState(MJButton, "MJ", MJCost);
        RefreshButtonState(ChubbsButton, "Chubbs", ChubbsCost);*/
        RefreshButtonState(OfficeGirlButton, "OfficeGirl", OfficeGirlCost);
    }



    private void RefreshButtonState(Button button, string characterType, int cost)
    {
        // We find the Text component inside the Button
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText == null) return;

        // Is the character purchased?
        bool purchased = IsCharacterBought(characterType) || cost == 0;

        // Who's selected now?
        string currentSelected = PlayerPrefs.GetString("SelectedCharacter", "boy");

        if (!purchased)
        {
            // Not purchased => show "Cost: XXXX"
            buttonText.text = $"Cost: {cost}";
        }
        else
        {
            // purchased => check if it's currently selected
            if (currentSelected == characterType)
            {
                // show "Selected"
                buttonText.text = "Selected";
            }
            else
            {
                // otherwise "Select"
                buttonText.text = "Select";
            }
        }
    }


    private void LoadSelectedCharacter()
    {
        string selectedCharacterType = PlayerPrefs.GetString("SelectedCharacter", "boy");
/*
        if (selectedCharacterType == "boy")

            SelectCharacter(ElonCharacterMenu, ElonCharacterMenu.transform, "Elon", ElonPositionTarget.transform.position);
*/
    /*    else if (selectedCharacterType == "girl")
            SelectCharacter(girlCharacterMenu, girlCharacterGame.transform, "girl", girlPositionTarget.transform.position);
        else if (selectedCharacterType == "newGirl")
            SelectCharacter(newGirlCharacterMenu, newGirlCharacterGame.transform, "newGirl", newGirlPositionTarget.transform.position);
        else if (selectedCharacterType == "newBoy")
            SelectCharacter(newBoyCharacterMenu, newBoyCharacterGame.transform, "newBoy", newBoyPositionTarget.transform.position);
        else if (selectedCharacterType == "Gwen")
            SelectCharacter(GwenCharacterMenu, GwenCharacterGame.transform, "Gwen", GwenPositionTarget.transform.position);
        else if (selectedCharacterType == "EgyptQueen")
            SelectCharacter(EgyptQueenCharacterMenu, EgyptQueenCharacterGame.transform, "EgyptQueen", EgyptQueenPositionTarget.transform.position);
        else if (selectedCharacterType == "Witch")
            SelectCharacter(WitchCharacterMenu, WitchCharacterGame.transform, "Witch", WitchPositionTarget.transform.position);*/
        if (selectedCharacterType == "Elon")
            SelectCharacter(ElonCharacterMenu, ElonCharacterGame.transform, "Elon", ElonPositionTarget.transform.position);
       /* else if (selectedCharacterType == "Mansa")
            SelectCharacter(MansaCharacterMenu, MansaCharacterGame.transform, "Mansa", MansaPositionTarget.transform.position);
        else if (selectedCharacterType == "Hotb")
            SelectCharacter(HotbCharacterMenu, HotbCharacterGame.transform, "Hotb", HotbPositionTarget.transform.position);
        else if (selectedCharacterType == "Hotg")
            SelectCharacter(HotgCharacterMenu, HotgCharacterGame.transform, "Hotg", HotgPositionTarget.transform.position);
        else if (selectedCharacterType == "MJ")
            SelectCharacter(MJCharacterMenu, MJCharacterGame.transform, "MJ", MJPositionTarget.transform.position);
        else if (selectedCharacterType == "Chubbs")
            SelectCharacter(ChubbsCharacterMenu, ChubbsCharacterGame.transform, "Chubbs", ChubbsPositionTarget.transform.position);*/
        else if (selectedCharacterType == "OfficeGirl")
            SelectCharacter(OfficeGirlCharacterMenu, OfficeGirlCharacterGame.transform, "OfficeGirl", OfficeGirlPositionTarget.transform.position);
    }


    private void ResetAssets()
    {
        Assets.GoldPortfolio = 0;
        Assets.StockPortfolio = 0;
        Assets.FixedDepositPortfolio = 0;
        Assets.RealEstatePortfolio = 0;
        Assets.CryptoPortfolio = 0;
        Assets.AntiquePortfolio = 0;
        Assets.MutualFundsPortfolio = 0;
        Assets.LandPortfolio = 0;
        character.isWarningShown = false;
        GameManager.isGameOver = false;

    }

    public void ChangeSortOrderToFive()
    {

        CharSelectCanvas.sortingOrder = 5;


    }

    public void ResetPurchasedCharacters()
    {
        PlayerPrefs.DeleteKey("newBoyBought");
        PlayerPrefs.DeleteKey("newGirlBought");
        PlayerPrefs.DeleteKey("GwenBought");
        PlayerPrefs.DeleteKey("WitchBought");
        PlayerPrefs.DeleteKey("EgyptQueenBought");
        PlayerPrefs.DeleteKey("ElonBought");
        PlayerPrefs.DeleteKey("MansaBought");
        PlayerPrefs.DeleteKey("HotbBought");
        PlayerPrefs.DeleteKey("HotgBought");
        PlayerPrefs.DeleteKey("MJBought");
        PlayerPrefs.DeleteKey("ChubbsBought");
        PlayerPrefs.DeleteKey("OfficeGirlBought");

        // If you also want to reset classic boy/girl (if they have a cost):
        // PlayerPrefs.DeleteKey("boyBought");
        // PlayerPrefs.DeleteKey("girlBought");

        // Reset selected character to "boy" or nothing
        PlayerPrefs.SetString("SelectedCharacter", "boy");

        PlayerPrefs.Save();
        Debug.Log("Character purchases reset!");

        // Then refresh the UI
        RefreshAllCharacterButtons();
    }



}