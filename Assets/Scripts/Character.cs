using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
//using CandyCoded.HapticFeedback;
using static GameManager;
using UnityEngine.UI;
using EZCameraShake;
using JetBrains.Annotations;
//using com.jiogames.wrapper;
using DG.Tweening.Core.Easing;


[System.Serializable]
public enum SIDE { Left, Mid, Right }
public enum HitX { Left, Mid, Right, None }
public enum HitY { Up, Mid, Down, Low, None }
public enum HitZ { Forward, Mid, Backward, None }

public class Character : MonoBehaviour
{
    public static bool is1Dead = false;
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown;
    public float XValue = 2f;
    private CharacterController m_char;
    public static Animator m_Animator;
    private float x;
    public float SpeedDodge;
    public float JumpPower = 7f;
    private float y;
    public bool InJump;
    public bool InRoll;
    private float ColHeight;
    private float ColCenterY;
    public float StartingSpeed = 0f;
    public float FwdSpeed = 0f;
    internal float RollCounter;
    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;
    public static bool isDead = false;
    public bool isWarningShown = false;
    public GameManager gameOverInstance;
    public static bool StopAllState = false;
    public static bool CanInput = true;
    public Collider CollisionCol;
    public float SpeedIncrement = 0f;
    public float MaxSpeed = 0;
    public float numberOfCoinsforDeath;
    public TextMeshProUGUI CoinsOverText;

    public static bool isFlying = false;
    public static bool isDescending = false;
    public TMPPopupManager popupManager;
    public TmpLongPopup LongpopupManager;
    public static int HandleRepairValue = 15000;
    public static int HandleDinnerValue = 5000;
    public static int HandleHospitalValue = 50000;
    public static int HandleWineValue = 3000;
    public static int HandleBabyValue = 100000;
    public static int HandleShoppingCartValue = 5000;
    public static int HandleVacationValue = 5000;
    public static int HandleCasinoValue = 50000;
    public static int HandleGoldRisesValue = 50000;
    public static int HandleAcreLandValue = 50000;
    public static int HandleStockValue = 20000;
    public static int HandleDepositValue = 20000;
    public static int HandleInterestPayoutValue = 7000;
    public static int HandleBorrowHitsValue = 15000;
    public static int HandleInflationMoneyValue = 20000;
    public static int HandleBirthdayPartyValue = 5000;
    public static int HandleAnniversaryValue = 10000;
    public static int HandleCoffeeValue = 400;
    public static int HandleRepaintHouseValue = 50000;
    public static int HandlePhoneValue = 40000;
    public static int HandleWatchValue = 8000;
    public static int HandleClothesValue = 5000;
    public static int HandleMovieValue = 1000;
    public static int HandleShoesValue = 4000;
    public static int HandleDentistValue = 3000;
    public static int HandleCarRepairValue = 10000;
    public static int HandleCharityValue = 5000;
    public static int HandleCryptoFallsValue = 5000;
    public static int HandleCryptoRisesValue = 5000;
    public static int HandleMutualFallsValue = 5000;
    public static int HandleMutualRisesValue = 5000;
    public static int HandleAntiqueValue = 5000;
    public static int HandleRealEstateValue = 5000;
    public static int HandlePotholeValue = 10000;
    public static int HandlePhysicalHitValue = 10000;

    private bool hasHitPhysicalObject = false;


    private HighScoreManager highScoreManager;

    private LotteryManager lotteryManager;
    public Vector3 startPosition = new Vector3(0, 0, 0);
    private int PopUpX = 150; 
    private int PopUpY =800;
    private int PopUpZ = 0;
    //public GameOverVideoPlayer gameOverVideoPlayer;

    public Slider healthSlider;
    private Coroutine depleteCoroutine;
    private float originalPhysicalExpense;
    [SerializeField] private float powerUpDuration = 30f;
    public bool isNextJumpQueued = false; // Flag to queue the second jump
    public bool isHealthInsuranceActive = false; // Track if the power-up is active
    private float basePhysicalExpense = 0.2f;
    public float PhysicalExpense { get; private set; }
    public float maxPhysicalExpense = 0.5f;
    public float increment = 0.05f;
    [SerializeField] private float duration = 45f;
    private int totalExpensesHit = 0; // Tracks the number of expenses hit
    public Animator GameOverAnimator;
    private int totalExpenseAmount = 0; // Tracks the cumulative expense amount

    public TextMeshProUGUI totalExpensesHitText;
    public TextMeshProUGUI totalExpenseAmountText;
    private float TotalLoss;
    public TextMeshProUGUI TotalLossText;
    public GroundSpawnerTest GroundSpawnerTest;
    private Coroutine increaseExpenseCoroutine;
    private bool GreaterGravity = false;
    public static bool GameStarted = false;
    public static bool isGameStart = false;



    /* [Header("CameraShaker")]
     private Transform cameraTransform = default;
     public Vector3 originalPosCam = default; // Fixed typo from 'orignalPosCam'
     public float shakeFrequency = 0.1f; // Frequency of shake (smaller values for subtle shake)
     public float shakeIntensity = 0.1f; // Intensity of shake (adjust for minimal effect)
     bool startShake = false;
     float shakeDuration = 0.2f; // Total duration of the shake
     float shakeCurrentTime = 0;*/


    void Start()
    {
        Time.timeScale = 0.2f;
        PhysicalExpense = 0.2f;
        increaseExpenseCoroutine = StartCoroutine(IncreasePhysicalExpenseOverTime());
        /*cameraTransform = GameObject.FindWithTag("MainCamera").transform;
        originalPosCam = cameraTransform.position;*/
  
        transform.position = Vector3.zero;
        startPosition = Vector3.zero;

        m_Animator = GetComponent<Animator>();
        m_char = GetComponent<CharacterController>();
        if (m_char == null)
        {
            Debug.LogError("CharacterController component is missing!");
            return;
        }
        if (popupManager == null)
        {
            popupManager = FindFirstObjectByType<TMPPopupManager>();
        }

        if (m_Side == SIDE.Mid)
            NewXPos = 0f;
        else if (m_Side == SIDE.Left || m_Side == SIDE.Right)
            NewXPos = XValue;
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        CanInput = false;
        PlayAnimation("Idle");

        lotteryManager = FindFirstObjectByType<LotteryManager>();

        if (lotteryManager == null)
        {
            Debug.LogError("LotteryManager not found in the scene!");
        }
        //StartCoroutine(StartRunning());
        healthSlider.gameObject.SetActive(false);
        healthSlider.value = 1f;
        StartCoroutine(DelayedPause());
    }
    private IEnumerator DelayedPause()
    {

        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;

    }
    private IEnumerator IncreasePhysicalExpenseOverTime()
    {
        while (basePhysicalExpense < maxPhysicalExpense)
        {
            yield return new WaitForSeconds(duration);

            basePhysicalExpense = Mathf.Min(basePhysicalExpense + increment, maxPhysicalExpense);
            UpdateEffectiveExpense();

            Debug.Log("Base Expense increased to: " + basePhysicalExpense);
            Debug.Log("Effective Physical Expense: " + PhysicalExpense);
        }
    }
    private void UpdateEffectiveExpense()
    {
        PhysicalExpense = isHealthInsuranceActive
            ? basePhysicalExpense * 0.5f
            : basePhysicalExpense;
    }
    public static void StartRunning()
    {
        GameStarted = true;
        Time.timeScale = 1f;
       
        //yield return new WaitForSeconds(10f); // Adjust the delay as needed
        CanInput = true;
        PlayAnimation("run");
    }
    private void Awake()
    {
        // Already finding highScoreManager here — add reset logic too
        highScoreManager = FindFirstObjectByType<HighScoreManager>();

        // ✅ Reset all run-state variables for fresh start
        isDead = false;
        StopAllState = false;
        CanInput = false; // stays false until StartRunning()
        m_Animator = GetComponent<Animator>();
        // Find the HighScoreManager instance in the scene during the Awake phase
        highScoreManager = FindFirstObjectByType<HighScoreManager>();
    }

    void Update()
    {
       
        /*//Camera Shake 
        if (startShake)
        {
            if (shakeCurrentTime < shakeDuration)
            {
                shakeCurrentTime += Time.deltaTime;
                CamShake();
            }
            else
            {
                StopShake();
            }
        }*/

        HandleBridge();
        // Debug.Log("Total Expenses Hit: " + totalExpensesHit);
        if (FwdSpeed < MaxSpeed)
            FwdSpeed += SpeedIncrement * Time.deltaTime;
        CollisionCol.isTrigger = !CanInput;
        if (!CanInput)
        {
            m_char.Move(Vector3.down * 10f * Time.deltaTime);
            return;
        }
        if (isDead)
        {
            return;
        }
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.swipeLeft && CanInput;
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || SwipeManager.swipeRight && CanInput;
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || SwipeManager.swipeUp && CanInput;
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || SwipeManager.swipeDown && CanInput;

        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, FwdSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * SpeedDodge);
        m_char.Move(moveVector);

        if (!isFlying)
        {
            if (SwipeLeft)
            {
                if (m_Side == SIDE.Mid)
                {
                    NewXPos = -XValue;
                    m_Side = SIDE.Left;
                    if (InJump)
                        PlayAnimation("Jump");
                    else if (!InRoll)
                        PlayAnimation("dodgeLeft");
                }
                else if (m_Side == SIDE.Right)
                {
                    NewXPos = 0;
                    m_Side = SIDE.Mid;
                    if (InJump)
                        PlayAnimation("Jump");
                    else if (!InRoll)
                        PlayAnimation("dodgeLeft");
                }
            }
            else if (SwipeRight)
            {
                if (m_Side == SIDE.Mid)
                {
                    NewXPos = XValue;
                    m_Side = SIDE.Right;
                    if (InJump)
                        PlayAnimation("Jump");
                    else if (!InRoll)
                        PlayAnimation("dodgeRight");
                }
                else if (m_Side == SIDE.Left)
                {
                    NewXPos = 0;
                    m_Side = SIDE.Mid;
                    if (InJump)
                        PlayAnimation("Jump");
                    else if (!InRoll)
                        PlayAnimation("dodgeRight");
                }
            }
        }

        if (!isFlying)
        {
            Jump();
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        Roll();

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        GameOver();

        /* if (isFlying)
         {
             Fly();
         }
         else if (isDescending)
         {
             Descend();
         }
         if (isJetpackActive)
         {
             HandleJetpack();
         }*/
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            TestPopup();
        }*/
    }


    /*
        public void StartShaking()
        {
            shakeCurrentTime = 0;
            startShake = true;
        }

        public void CamShake()
        {
            // Smooth shake around the original position
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            cameraTransform.position = originalPosCam + shakeOffset;
        }

        public void StopShake()
        {
            // Reset camera position to the original
            cameraTransform.position = originalPosCam;
            startShake = false;
        }

    */

    public IEnumerator DeathPlayer(string anim)
    {
        StopAllState = true;
        m_Animator.Play(anim);
        yield return new WaitForSeconds(0.2f);
        CanInput = false;
        isDead = true;
        //Debug.Log("DeathPlayer");
    }

    private bool wasOnGround = true;

    public static void PlayAnimation(string anim)
    {
        if (StopAllState) return;
        m_Animator.Play(anim);
    }


    public void Jump()
    {
        if (m_char.isGrounded)
        {
            if (SwipeUp && !isFlying)
            {
                y = JumpPower;
                m_Animator.CrossFadeInFixedTime("Jump", 0.1f);
                InJump = true;
                isNextJumpQueued = false; // Reset the flag when a new jump starts
            }
            else if (isNextJumpQueued)
            {
                // Trigger the queued jump when grounded
                y = JumpPower;
                m_Animator.CrossFadeInFixedTime("Jump", 0.1f);
                InJump = true;
                isNextJumpQueued = false; // Reset after executing the queued jump
            }
            else
            {
                InJump = false; // Reset InJump when grounded
            }
        }
        else
        {
            // Queue the next jump if a swipe up is detected while already jumping
            if (SwipeUp && !isFlying)
            {
                isNextJumpQueued = true;
            }

            y -= JumpPower * 2 * Time.deltaTime; // Simulate gravity while in the air
        }
    }



    public void Descend()
    {
        if (m_char.isGrounded)
        {
            y = 0;
            isDescending = false;
            InJump = false; // Reset InJump when grounded
            PlayAnimation("run"); // Return to normal state
        }
        else
        {
            y -= JumpPower * 2 * Time.deltaTime; // Simulate a normal fall
        }
    }

    public void Roll()
    {
        if (isFlying) return; // Prevent rolling while flying

        RollCounter -= Time.deltaTime;
        if (RollCounter <= 0f)
        {
            RollCounter = 0f;
            m_char.center = new Vector3(0, ColCenterY, 0);
            m_char.height = ColHeight;
            InRoll = false;
        }
        if (SwipeDown)
        {
            RollCounter = 0.2f;
            y -= 10f;
            m_char.center = new Vector3(0, ColCenterY / 2f, 0);
            m_char.height = ColHeight / 2f;
            m_Animator.CrossFadeInFixedTime("roll", 0.1f);
            InRoll = true;
            InJump = false;
        }
    }







    public HitX GetHitX(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
        float average = ((min_x + max_x) / 2f - char_bounds.min.x) / char_bounds.size.x;

        HitX hit;

        if (average > 0.66f)
            hit = HitX.Right;

        else if (average < 0.33f)
            hit = HitX.Left;

        else
            hit = HitX.Mid;

        return hit;
    }

    public HitY GetHitY(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, char_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, char_bounds.max.y);
        float average = ((min_y + max_y) / 2f - char_bounds.min.y) / char_bounds.size.y;

        HitY hit;

        if (average < 0.17f)
            hit = HitY.Low;

        else if (average < 0.33f)
            hit = HitY.Down;

        else if (average < 0.66f)
            hit = HitY.Mid;

        else
            hit = HitY.Up;

        return hit;
    }

    public HitZ GetHitZ(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_z = Mathf.Max(col_bounds.min.z, char_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, char_bounds.max.z);
        float average = ((min_z + max_z) / 2f - char_bounds.min.z) / char_bounds.size.z;

        HitZ hit;

        if (average < 0.33f)
            hit = HitZ.Backward;

        else if (average < 0.33f)
            hit = HitZ.Mid;

        else
            hit = HitZ.Forward;

        return hit;
    }

    public void OnCharacterColliderHit(Collider col)
    {
        hitX = GetHitX(col);

        if (hitY == HitY.Mid && hitX == HitX.Mid)
        {
            //isDead = true;
            //Debug.Log("Hit from front");
            // GameOver gameOverInstance = new GameOver();
            // gameOverInstance.ShowGameOverScreen();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isDead || StopAllState ) return;
        switch (other.tag)// MUTUAL FUNDS ,CRYPTO CURRENCY,ARTIFACTS, real estate. make real estate office space like, and for mutual funds too make a 3d object like
        {
            case "Health":
                HandleHealth(other);
                break;
            case "GoldRises":
                HandleGoldRises(other);
                break;
            case "AcreLand":
                HandleAcreLand(other);
                break;
            case "Stock":
                HandleStock(other);
                break;
            case "Deposit":
                HandleDeposit(other);
                break;
            case "InterestPayout":
                HandleInterestPayout(other);
                break;
            case "BorrowHits":
                HandleBorrowHits(other);
                break;
            case "InflationMoney":
                HandleInflationMoney(other);
                break;
            case "BirthdayParty":
                HandleBirthdayParty(other);
                break;
            case "Anniversary":
                HandleAnniversary(other);
                break;
            case "Coffee":
                HandleCoffee(other);
                break;
            case "RepaintHouse":
                HandleRepaintHouse(other);
                break;
            case "Phone":
                HandlePhone(other);
                break;
            case "Watch":
                HandleWatch(other);
                break;
            case "Clothes":
                HandleClothes(other);
                break;
            case "Shoes":
                HandleShoes(other);
                break;
            case "Casino":
                HandleCasino(other);
                break;
            case "Dentist":
                HandleDentist(other);
                break;
            case "Pothole":
                HandlePothole(other);
                break;
            case "CarRepair":
                HandleCarRepair(other);
                break;
            case "Movie":
                HandleMovie(other);
                break;
            case "Charity":
                HandleCharity(other);
                break;
            case "ShoppingCart":
                HandleShoppingCart(other);
                break;
            case "Vacation":
                HandleVacation(other);
                break;
            case "Baby":
                HandleBaby(other);
                break;
            case "Wine":
                HandleWine(other);
                break;
            case "Hospital":
                HandleHospital(other);
                break;
            case "Dinner":
                HandleDinner(other);
                break;
            case "Repair":
                HandleRepair(other);
                break;
            case "Lottery":
                lotteryManager.ShowLotteryPanel();
                Destroy(other.gameObject);
                GameManager.numberOfCoins -= 5000;
                break;
            case "CryptoRises":
                HandleCryptoRises(other);
                break;
            case "CryptoFalls":
                HandleCryptoFalls(other);
                break;
            case "MutualFalls":
                HandleMutualFalls(other);
                break;
            case "MutualRises":
                HandleMutualRises(other);
                break;
            case "Antique":
                HandleAntique(other);
                break;
            case "RealEstate":
                HandleRealEstate(other);
                break;
            case "add_jump":
                HandleAddJump(other);
                break;

            case "substract_jump":
                HandleSubstractJump(other);
                break;
            case "lower_jump":
                HandleSubstractJump(other);
                break;
            case "Greater_Gravity":
                HandleHigherGravity(other);
                break;
            case "Normal_Gravity":
                HandleBridgeNormalGravity(other);
                break;
            default:
                // Optionally, handle unknown tags
                break;
        }
    }


    private void HandleAddJump(Collider other)
    {

        JumpPower += 3;

    }
    private void HandleLowerJump(Collider other)
    {

        JumpPower += 3;

    }
    private void HandleSubstractJump(Collider other)
    {

        JumpPower -= 3;

    }
    private void HandleBridgeNormalGravity(Collider other)
    {
        GreaterGravity = false;
        Debug.Log(GreaterGravity + "================GreaterGravity status");
    }
    private void HandleHigherGravity(Collider other)
    {
        GreaterGravity = true;

        Debug.Log(GreaterGravity + "================GreaterGravity status");

    }
    private void HandleBridge()
    {
        if (GreaterGravity && InJump)
        {
            Debug.Log("HandleBridge actiivated");
            StartCoroutine(WaitForGravity());
        }
    }
    private IEnumerator WaitForGravity()
    {
        yield return new WaitForSeconds(1f);

        Descend();
    }
    private void HandleNormalGravity(Collider other)
    {

        JumpPower -= 3;

    }


    private void HandleHealth(Collider other)
    {
        isHealthInsuranceActive = true; // Always set to true
        UpdateEffectiveExpense();

        healthSlider.value = 1f;
        healthSlider.gameObject.SetActive(true);

        // Refresh the power-up duration by restarting the coroutine
        if (depleteCoroutine != null)
            StopCoroutine(depleteCoroutine);
        depleteCoroutine = StartCoroutine(DepleteHealthOverTime());

        Destroy(other.gameObject);
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += StopHealthSlider;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= StopHealthSlider;
    }
    private void StopHealthSlider()
    {
        isHealthInsuranceActive = false;
    }
    private IEnumerator DepleteHealthOverTime()
    {
        float elapsedTime = 0f;
        float startValue = healthSlider.value;

        while (elapsedTime < powerUpDuration && isHealthInsuranceActive)
        {

            elapsedTime += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(startValue, 0f, elapsedTime / powerUpDuration);
            yield return null;
        }

        healthSlider.value = 1f;
        healthSlider.gameObject.SetActive(false);

        isHealthInsuranceActive = false;
        UpdateEffectiveExpense();

        depleteCoroutine = null;
    }
    private void HandleRealEstate(Collider other)
    {

        HandleRealEstateValue = Random.Range((int)(Assets.RealEstatePortfolio * 0.2f), (int)(Assets.RealEstatePortfolio * 0.7f) + 1);
        GameManager.numberOfCoins += HandleRealEstateValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleRealEstateValue);

        popupManager.ShowPopup($"+{HandleRealEstateValue}", Color.green, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleAntique(Collider other)
    {
        HandleAntiqueValue = Random.Range(
            (int)(Assets.AntiquePortfolio * -0.08f),
            (int)(Assets.AntiquePortfolio * 0.3f) + 1
        );

        GameManager.numberOfCoins += HandleAntiqueValue;

        string formattedValue = NumberFormatter.FormatNumberIndianSystem(Mathf.Abs(HandleAntiqueValue));
        string sign = HandleAntiqueValue >= 0 ? "+" : "-";
        Color popupColor = HandleAntiqueValue >= 0 ? Color.green : Color.red;

        popupManager.ShowPopup($"{sign}{formattedValue}", popupColor, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleMutualFalls(Collider other)
    {
        /*HandleMutualFallsValue = (int)(Assets.MutualFundsPortfolio * 0.04f); // 10 percent of the current number of coins
        GameManager.numberOfCoins -= HandleMutualFallsValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleMutualFallsValue);

        popupManager.ShowPopup($"-{HandleMutualFallsValue}", Color.red, new Vector3(PopUpX, PopUpY, PopUpZ));
        Destroy(other.gameObject);*/
        HandleMutualRisesValue = Random.Range(
            (int)(Assets.MutualFundsPortfolio * -0.02f),
            (int)(Assets.MutualFundsPortfolio * 0.04f) + 1
        );

        GameManager.numberOfCoins += HandleMutualRisesValue;

        string formattedValue = NumberFormatter.FormatNumberIndianSystem(Mathf.Abs(HandleMutualRisesValue));
        string sign = HandleMutualRisesValue >= 0 ? "+" : "-";
        Color popupColor = HandleMutualRisesValue >= 0 ? Color.green : Color.red;

        popupManager.ShowPopup($"{sign}{formattedValue}", popupColor, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }
    private void HandleMutualRises(Collider other)
    {
        HandleMutualRisesValue = Random.Range(
            (int)(Assets.MutualFundsPortfolio * -0.02f),
            (int)(Assets.MutualFundsPortfolio * 0.04f) + 1
        );

        GameManager.numberOfCoins += HandleMutualRisesValue;

        string formattedValue = NumberFormatter.FormatNumberIndianSystem(Mathf.Abs(HandleMutualRisesValue));
        string sign = HandleMutualRisesValue >= 0 ? "+" : "-";
        Color popupColor = HandleMutualRisesValue >= 0 ? Color.green : Color.red;

        popupManager.ShowPopup($"{sign}{formattedValue}", popupColor, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleCryptoFalls(Collider other)
    {
        totalExpensesHit++;
        HandleCryptoFallsValue = (int)(Assets.CryptoPortfolio * 0.2f); // 10 percent of the current number of coins
        GameManager.numberOfCoins -= HandleCryptoFallsValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleCryptoFallsValue);
        totalExpenseAmount += HandleCryptoFallsValue;
        popupManager.ShowPopup($"-{HandleCryptoFallsValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleCryptoRises(Collider other)
    {
        HandleCryptoRisesValue = (int)(Assets.CryptoPortfolio * 0.2f); // 10 percent of the current number of coins
        GameManager.numberOfCoins += HandleCryptoRisesValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleCryptoRisesValue);

        popupManager.ShowPopup($"+{HandleCryptoRisesValue}", Color.green, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleRepair(Collider other)
    {
        totalExpensesHit++;
        //
        //Handheld.Vibrate();
        PlayAnimation("HitOnHead");
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        GameManager.numberOfCoins -= HandleRepairValue;

        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleRepairValue);
        totalExpenseAmount += HandleRepairValue;

        popupManager.ShowPopup($"-{formattedRepairValue}", Color.red, new Vector2(PopUpX, PopUpY));

        Destroy(other.gameObject);
    }



    private void HandleDinner(Collider other)
    {
        totalExpensesHit++;

        // ////Handheld.Vibrate();();();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        // StartShaking();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleDinnerValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleDinnerValue);
        totalExpenseAmount += HandleDinnerValue;
        popupManager.ShowPopup($"-{HandleDinnerValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleHospital(Collider other)
    {
        totalExpensesHit++;
        PlayAnimation("HitOnHead");
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        // StartShaking();     // Check if the health insurance power-up is active
        if (isHealthInsuranceActive)
        {
            // Subtract only 80% of the hospital value
            GameManager.numberOfCoins -= HandleHospitalValue * 0.2f;

            string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleHospitalValue);
            totalExpenseAmount += HandleHospitalValue;
            LongpopupManager.ShowPopup(
    $"<color=green>Health Insurance 80% saved.</color>\n<color=red>Charged {(HandleHospitalValue * 0.2f):N0}.</color>",
    Color.green,
    new Vector3(600, 1700, 0)
);



        }
        else
        {

            // Subtract the full hospital value
            GameManager.numberOfCoins -= HandleHospitalValue;
            string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleHospitalValue);
            popupManager.ShowPopup($"-{HandleHospitalValue}", Color.red, new Vector2(PopUpX, PopUpY));
        }

        // Format and display the deducted value


        // Destroy the hospital expense object
        Destroy(other.gameObject);
    }


    private void HandleWine(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //  //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleWineValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleWineValue);
        totalExpenseAmount += HandleWineValue;
        popupManager.ShowPopup($"-{HandleWineValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleBaby(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        // //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleBabyValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleBabyValue);
        totalExpenseAmount += HandleBabyValue;
        popupManager.ShowPopup($"-{HandleBabyValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleShoppingCart(Collider other)
    {
        totalExpensesHit++;
        ////Handheld.Vibrate();();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleShoppingCartValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleShoppingCartValue);
        totalExpenseAmount += HandleShoppingCartValue;
        popupManager.ShowPopup($"-{HandleShoppingCartValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleVacation(Collider other)
    {
        totalExpensesHit++;
        HandleVacationValue = Random.Range(20000, 40001);
        ////Handheld.Vibrate();();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        // StartShaking();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleVacationValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleVacationValue);
        totalExpenseAmount += HandleVacationValue;

        popupManager.ShowPopup($"-{HandleVacationValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleCasino(Collider other)
    {
        totalExpensesHit++;
        HandleCasinoValue = Random.Range(10000, 20001);
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        // StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleCasinoValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleCasinoValue);
        totalExpenseAmount += HandleCasinoValue;
        popupManager.ShowPopup($"-{HandleCasinoValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleGoldRises(Collider other)
    {
        HandleGoldRisesValue = (int)(Assets.GoldPortfolio * 0.1f); // 10 percent of the current number of coins
        GameManager.numberOfCoins += HandleGoldRisesValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleGoldRisesValue);

        popupManager.ShowPopup($"+{HandleGoldRisesValue}", Color.green, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleAcreLand(Collider other)
    {
        HandleAcreLandValue = Random.Range((int)(Assets.LandPortfolio * 0.2f), (int)(Assets.LandPortfolio * 0.2f) + 1);
        GameManager.numberOfCoins += HandleAcreLandValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleAcreLandValue);

        popupManager.ShowPopup($"+{HandleAcreLandValue}", Color.green, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }


    private void HandleStock(Collider other)
    {
        HandleStockValue = Random.Range(
            (int)(Assets.StockPortfolio * -0.2f),
            (int)(Assets.StockPortfolio * 0.2f) + 1
        );

        GameManager.numberOfCoins += HandleStockValue;

        string formattedValue = NumberFormatter.FormatNumberIndianSystem(Mathf.Abs(HandleStockValue));
        string sign = HandleStockValue >= 0 ? "+" : "-";
        Color popupColor = HandleStockValue >= 0 ? Color.green : Color.red;

        popupManager.ShowPopup($"{sign}{formattedValue}", popupColor, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }


    private void HandleDeposit(Collider other)
    {
        HandleDepositValue = (int)(Assets.FixedDepositPortfolio * 0.08f);
        GameManager.numberOfCoins += HandleDepositValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleDepositValue);

        popupManager.ShowPopup($"+{HandleDepositValue}", Color.green, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleInterestPayout(Collider other)
    {
        HandleInterestPayoutValue = (int)(Assets.FixedDepositPortfolio * 0.08f);
        GameManager.numberOfCoins += HandleInterestPayoutValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleInterestPayoutValue);

        popupManager.ShowPopup($"+{HandleInterestPayoutValue}", Color.green, new Vector2(PopUpX, PopUpY));

        Destroy(other.gameObject);
    }

    private void HandleBorrowHits(Collider other)
    {
        totalExpensesHit++;
        HandleBorrowHitsValue = Random.Range((int)(GameManager.numberOfCoins * 0.05f), (int)(GameManager.numberOfCoins * 0.2f) + 1);
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleBorrowHitsValue);

        GameManager.numberOfCoins -= HandleBorrowHitsValue;
        totalExpenseAmount += HandleBorrowHitsValue;
        popupManager.ShowPopup($"-{HandleBorrowHitsValue}", Color.red, new Vector2(PopUpX, PopUpY));

        Destroy(other.gameObject);
    }

    private void HandleInflationMoney(Collider other)
    {
        totalExpensesHit++;
        HandleBorrowHitsValue = Random.Range((int)(GameManager.numberOfCoins * 0.08f), (int)(GameManager.numberOfCoins * 0.3f) + 1);
        //Handheld.Vibrate();();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleInflationMoneyValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleInflationMoneyValue);
        totalExpenseAmount += HandleInflationMoneyValue;
        popupManager.ShowPopup($"-{HandleInflationMoneyValue}", Color.red, new Vector2(PopUpX, PopUpY));

        Destroy(other.gameObject);
    }

    private void HandleBirthdayParty(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleBirthdayPartyValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleBirthdayPartyValue);
        totalExpenseAmount += HandleBirthdayPartyValue;
        popupManager.ShowPopup($"-{HandleBirthdayPartyValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleAnniversary(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        // StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleAnniversaryValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleAnniversaryValue);
        totalExpenseAmount += HandleAnniversaryValue;
        popupManager.ShowPopup($"-{HandleAnniversaryValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleCoffee(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
      //  CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleCoffeeValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleCoffeeValue);
        totalExpenseAmount += HandleCoffeeValue;
        popupManager.ShowPopup($"-{HandleCoffeeValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleRepaintHouse(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleRepaintHouseValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleRepaintHouseValue);
        totalExpenseAmount += HandleRepaintHouseValue;
        popupManager.ShowPopup($"-{HandleRepaintHouseValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandlePhone(Collider other)
    {
        totalExpensesHit++;
        HandlePhoneValue = Random.Range(15000, 60001);
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandlePhoneValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandlePhoneValue);
        totalExpenseAmount += HandlePhoneValue;
        popupManager.ShowPopup($"-{HandlePhoneValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleWatch(Collider other)
    {
        totalExpensesHit++;
        int calculatedValueforWatch = (int)(GameManager.numberOfCoins * 0.2f); // 20% of the current number of coins
        HandleWatchValue = Mathf.Min(calculatedValueforWatch, 25000); // Choose the lower value between 20% of coins and 25,000
                                                                      //Handheld.Vibrate();();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleWatchValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleWatchValue);
        totalExpenseAmount += HandleWatchValue;
        popupManager.ShowPopup($"-{HandleWatchValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }


    private void HandleClothes(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleClothesValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleClothesValue);
        totalExpenseAmount += HandleClothesValue;
        popupManager.ShowPopup($"-{HandleClothesValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleMovie(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleMovieValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleMovieValue);
        totalExpenseAmount += HandleMovieValue;
        popupManager.ShowPopup($"-{HandleMovieValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleShoes(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleShoesValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleShoesValue);

        popupManager.ShowPopup($"-{HandleShoesValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }


    private void HandlePothole(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandlePotholeValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandlePotholeValue);
        //Handheld.Vibrate();();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        totalExpenseAmount += HandlePotholeValue;
        popupManager.ShowPopup($"-{HandlePotholeValue}", Color.red, new Vector2(PopUpX, PopUpY));
        // Destroy(other.gameObject);
    }
    private void HandleDentist(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleDentistValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleDentistValue);
        //Handheld.Vibrate();();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();

        totalExpenseAmount += HandleDentistValue;
        popupManager.ShowPopup($"-{HandleDentistValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }

    private void HandleCarRepair(Collider other)
    {
        totalExpensesHit++;
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        //Handheld.Vibrate();();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleCarRepairValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleCarRepairValue);
        totalExpenseAmount += HandleCarRepairValue;
        popupManager.ShowPopup($"-{HandleCarRepairValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }
    private void HandleCharity(Collider other)
    {
        totalExpensesHit++;
        int calculatedValueforCharity = (int)(GameManager.numberOfCoins * 0.02f); // 20% of the current number of coins
        HandleCharityValue = Mathf.Min(calculatedValueforCharity, 5000); // Choose the lower value between 20% of coins and 25,000
                                                                         //Handheld.Vibrate();();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        PlayAnimation("HitOnHead");
        GameManager.numberOfCoins -= HandleCharityValue;
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandleCharityValue);
        totalExpenseAmount += HandleCharityValue;
        popupManager.ShowPopup($"-{HandleCharityValue}", Color.red, new Vector2(PopUpX, PopUpY));
        Destroy(other.gameObject);
    }
    public void HandlePhysicalHit(Collider other)
    {
        totalExpensesHit++;
        if (hasHitPhysicalObject)
        {

            return; // Ignore if already hit
        }

        if (GroundSpawnerTest.isTutorialMode) // Assuming you have access to this property
        {

            return; // Skip further logic if tutorial mode is active
        }
        hasHitPhysicalObject = true; // Set the flag to true upon hitting

        int calculatedValueforPhysicalHit = (int)(GameManager.numberOfCoins * PhysicalExpense); // Uses dynamic PhysicalExpense
        HandlePhysicalHitValue = Mathf.Max(calculatedValueforPhysicalHit, 100000); // Choose the lower value between PhysicalExpense of coins and 100,000


        totalExpenseAmount += HandlePhysicalHitValue;

        // Start coroutine to handle the delayed hit effect
        StartCoroutine(DelayedHitEffect());

        // Optional: Trigger immediate effects like vibration or animations
        // Handheld.Vibrate();
        if ((PlayerPrefs.GetInt("Vibration", 1) == 1))
        {
            //HapticFeedback.HeavyFeedback();
            WebGLVibration.VibrateDevice(200);
        }
       // CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        //StartShaking();
        // PlayAnimation("HitOnHead");
    }

    private IEnumerator DelayedHitEffect()
    {
        yield return new WaitForSeconds(1f);  // Wait for 1 second

        GameManager.numberOfCoins -= HandlePhysicalHitValue; // Deduct coins, even if it goes below zero
        string formattedRepairValue = NumberFormatter.FormatNumberIndianSystem(HandlePhysicalHitValue);


        popupManager.ShowPopup($"-{HandlePhysicalHitValue}", Color.red, new Vector2(PopUpX, PopUpY));

        // Reset the flag after deduction
        hasHitPhysicalObject = false;
    }


    /* void ShowDamage(string text)
     {
         if (floatingTextPrefab)
         {
             GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
             prefab.GetComponentInChildren<TextMesh>().text = text;
         }
     }*/





    private void GameOver()
    {
        if (GameManager.numberOfCoins <= 0 && GameManager.First_Zero == false)
        {
            TotalLoss = totalInflationCut + totalExpenseAmount;
            isDead = true;
            isWarningShown = true;

            StartCoroutine(DeathPlayer("defeated"));

            StartCoroutine(ShowGameOverScreenAfterDelay(0.3f));
            CoinsOverText.gameObject.SetActive(true);   
            GameOverAnimator.SetTrigger("Over");

            if (highScoreManager != null)
            {
                highScoreManager.CheckForHighScore();
            }
            highScoreManager.UpdateGameOverUI();
            
            totalExpensesHitText.text = "" + totalExpensesHit;
            totalExpenseAmountText.text = "" + NumberFormatter.FormatNumberIndianSystem(totalExpenseAmount);
            TotalLossText.text = "" + NumberFormatter.FormatNumberIndianSystem(TotalLoss);
            is1Dead = true;
            PauseMenu.PauseBtn.SetActive(false);
        }
    }

    private IEnumerator ShowGameOverScreenAfterDelay(float delay)
    {
        Debug.Log("GameOver true");
        yield return new WaitForSeconds(delay);
        Debug.Log(" After delay GameOver true");
        yield return new WaitForSeconds(1);
        //gameOverVideoPlayer.gameOverUI.SetActive(true);
        yield return new WaitForSeconds(1);
        // IronSourceAdsManager.instance.ShowInterstitial();
        //JioWrapperJS.Instance.postScore(PlayerPrefs.GetInt("CurrentScore"));
        Debug.Log(PlayerPrefs.GetInt("CurrentScore")+"===========Current Score ");
       // JioWrapperJS.Instance.showInterstitial();
            
        // Directly activate the game over UI
    }

    public void ResetCharacter()
    {
        transform.position = startPosition;
        GameManager.numberOfCoins = 1000000;
        m_Side = SIDE.Mid;
        NewXPos = 0f;
        SwipeLeft = false;
        SwipeRight = false;
        SwipeUp = false;
        SwipeDown = false;
        InJump = false;
        InRoll = false;

        // ✅ Reset death & state locks
        isDead = false;
        StopAllState = false;
        CanInput = true;

        GameManager.isGameOver = false;
       // Debug.Log(isGameOver + "1 isGameOver = false====");
        // Optional: Reset forward speed
        FwdSpeed = StartingSpeed;

        m_char.enabled = true; // if you disabled it at death
    }


    // for physical hit
    public void OnTriggerEnterPhysicalRight(Collider other)
    {


        if (!isFlying)
        {
            if (other.CompareTag("PhysicalToRight"))
            {
                if (m_Side == SIDE.Mid)
                {
                    // Move left to the left lane from the middle
                    NewXPos = -XValue;
                    m_Side = SIDE.Left;
                    if (InJump)
                        PlayAnimation("Jump");
                    else if (!InRoll)
                        PlayAnimation("dodgeRightNew");
                }
                else if (m_Side == SIDE.Right)
                {
                    // Move to the middle lane from the right
                    NewXPos = 0;
                    m_Side = SIDE.Mid;
                    if (InJump)
                        PlayAnimation("Jump");
                    else if (!InRoll)
                        PlayAnimation("dodgeRightNew");
                }

            }

        }
    }

    public void OnTriggerEnterPhysicalLeft(Collider other)
    {

        if (!isFlying)
        {
            if (other.CompareTag("PhysicalToLeft"))
            {
                if (m_Side == SIDE.Mid)
                {
                    // Move right to the right lane from the middle
                    NewXPos = XValue;
                    m_Side = SIDE.Right;
                    if (InJump)
                        PlayAnimation("Jump");
                    else if (!InRoll)
                        PlayAnimation("dodgeRightNew");
                }
                else if (m_Side == SIDE.Left)
                {
                    // Move to the middle lane from the left
                    NewXPos = 0;
                    m_Side = SIDE.Mid;
                    if (InJump)
                        PlayAnimation("Jump");
                    else if (!InRoll)
                        PlayAnimation("dodgeRightNew");
                }

            }


        }
    }

    private bool hasChangedSide = false;


    public void OnTriggerStay(Collider other)
    {
        if (isDead || StopAllState) return;


        if (!isFlying && other.CompareTag("PBRE") && !hasChangedSide)
        {
            if (m_Side == SIDE.Mid)
            {
                // Move right from the middle lane
                NewXPos = XValue;
                m_Side = SIDE.Right;
                hasChangedSide = true;
                PlayAnimationIfNotJumpingOrRolling("dodgeBack");
                StartCoroutine(ResetSideChangeFlag(0.1f));
            }
            else if (m_Side == SIDE.Left)
            {
                // Move to the middle lane from the left
                NewXPos = 0;
                m_Side = SIDE.Mid;
                hasChangedSide = true;
                PlayAnimationIfNotJumpingOrRolling("dodgeBack");
                StartCoroutine(ResetSideChangeFlag(0.1f));
            }
        }

        if (!isFlying && other.CompareTag("PBLE") && !hasChangedSide)
        {
            if (m_Side == SIDE.Mid)
            {
                // Move left from the middle lane
                NewXPos = -XValue;
                m_Side = SIDE.Left;
                hasChangedSide = true;
                PlayAnimationIfNotJumpingOrRolling("dodgeBack");
                StartCoroutine(ResetSideChangeFlag(0.1f));
            }
            else if (m_Side == SIDE.Right)
            {
                // Move to the middle lane from the right
                NewXPos = 0;
                m_Side = SIDE.Mid;
                hasChangedSide = true;
                PlayAnimationIfNotJumpingOrRolling("dodgeBack");
                StartCoroutine(ResetSideChangeFlag(0.1f));
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PBRE") || other.CompareTag("PBLE"))
        {
            // Optionally reset the flag when leaving the trigger area
            hasChangedSide = false;
        }
    }

    private IEnumerator ResetSideChangeFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasChangedSide = false;
    }

    private void PlayAnimationIfNotJumpingOrRolling(string animation)
    {
        if (InJump)
            PlayAnimation("Jump");
        else if (!InRoll)
            PlayAnimation(animation);
    }
    public void isButtonClicking()
    {
        Debug.Log("PortOff Btn is clicked");
    }
    /*public void OnTriggerEnterPhysicalBackLefttEmpty(Collider other)
    {
        {
            Debug.Log("OnTriggerEnterPhysicalBackLefttEmpty called with collider: " + other.name);
            if (!isFlying)
            {
                if (other.CompareTag("PBLE"))
                {
                    if (m_Side == SIDE.Mid)
                    {
                        // Move left to the left lane from the middle
                        NewXPos = -XValue;
                        m_Side = SIDE.Left;
                        if (InJump)
                            PlayAnimation("Jump");
                        else if (!InRoll)
                            PlayAnimation("dodgeBack");
                    }
                    else if (m_Side == SIDE.Right)
                    {
                        // Move to the middle lane from the right  
                        NewXPos = 0;
                        m_Side = SIDE.Mid;
                        if (InJump)
                            PlayAnimation("Jump");
                        else if (!InRoll)
                            PlayAnimation("dodgeBack");
                    }
                    *//*else if (m_Side == SIDE.Left)
                    {
                        // Stay in the left lane since the obstacle is to the right
                        NewXPos = -XValue;
                        m_Side = SIDE.Left;
                        if (InJump)
                            PlayAnimation("Jump");
                        else if (!InRoll)
                            PlayAnimation("dodgeLeft");
                    }*//*
                }
               
            }
        }
    }*/
}