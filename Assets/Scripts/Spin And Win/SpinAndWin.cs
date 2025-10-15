using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using TMPro;
using System.Collections;

public enum RewardType
{
    Diamonds,
    Character,
    BadLuck
}

[System.Serializable]
public struct Reward
{
    public RewardType rewardType;
    public int amount;
    public float probability;
}

public class SpinAndWin : MonoBehaviour
{
    private float genSpeed;
    private float subSpeed;
    private bool isSpin;
    private int numberOfSegments = 10;
    private int diamondCount;
    private float spinTime;
    private const float maxSpinTime = 10f;
    private Coroutine timerCoroutine;

    private const string LastSpinTimeKey = "LastSpinTime";
    private const string CharacterWonKey = "CharacterWon_";
    private const string SpinCountKey = "SpinCount";
    private const int MinSpinsBeforeCharacter = 6;
    private const double LockDurationHours = 12.0f;

    [Header("Buttons")]
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button spinBtn;
    [SerializeField] private GameObject spinPanel;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI lockMessageText;

    [Header("Audio")]
    [SerializeField] private AudioSource spinAudioSource;
    [SerializeField] private AudioClip spinClip;

    [Header("Rewards")]
    [SerializeField] private Reward[] rewards;

    private void Start()
    {
        closeBtn.onClick.AddListener(Close);
        spinBtn.onClick.AddListener(SpinWheel);
        StartTimerUpdate();
    }

    private void Update()
    {
        if (isSpin)
        {
            spinTime += Time.deltaTime;

            if (spinTime >= maxSpinTime || genSpeed <= 0)
            {
                genSpeed = 0;
                isSpin = false;
                DetermineReward();

                if (spinAudioSource != null)
                {
                    spinAudioSource.Stop();
                }
            }
            else
            {
                transform.Rotate(0, 0, -genSpeed * Time.deltaTime);
                genSpeed -= subSpeed * Time.deltaTime;
                if (genSpeed < 0) genSpeed = 0;

                if (spinAudioSource != null)
                {
                    spinAudioSource.pitch = Mathf.Lerp(0.5f, 1.5f, genSpeed / 400f);
                    spinAudioSource.volume = Mathf.Lerp(0.2f, 1f, genSpeed / 400f);
                }
            }
            closeBtn.gameObject.SetActive(!isSpin);
            spinBtn.interactable = false;
        }
    }

    private void OnEnable()
    {
        StartTimerUpdate();
    }

    private void OnDisable()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private void StartTimerUpdate()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(UpdateSpinAvailabilityTimer());
    }

    private IEnumerator UpdateSpinAvailabilityTimer()
    {
        while (!IsSpinAvailable())
        {
            DateTime lastSpinTime = DateTime.Parse(PlayerPrefs.GetString(LastSpinTimeKey));
            TimeSpan timeUntilNextSpin = lastSpinTime.AddHours(LockDurationHours) - DateTime.Now;
            lockMessageText.text = $"{timeUntilNextSpin.Hours}h : {timeUntilNextSpin.Minutes}m : {timeUntilNextSpin.Seconds}s";
            spinBtn.interactable = false;
            yield return new WaitForSeconds(1f);
        }

        lockMessageText.text = "Spin";
        spinBtn.interactable = true;
    }

    private bool IsSpinAvailable()
    {
        if (!PlayerPrefs.HasKey(LastSpinTimeKey))
            return true;

        DateTime lastSpinTime = DateTime.Parse(PlayerPrefs.GetString(LastSpinTimeKey));
        TimeSpan timeSinceLastSpin = DateTime.Now - lastSpinTime;

        return timeSinceLastSpin.TotalHours >= LockDurationHours;
    }

    private void SaveLastSpinTime()
    {
        PlayerPrefs.SetString(LastSpinTimeKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    public void SpinWheel()
    {
        if (!isSpin && IsSpinAvailable())
        {
            genSpeed = Random.Range(300f, 400f);
            subSpeed = Random.Range(20f, 40f);
            isSpin = true;
            spinTime = 0;
            spinBtn.interactable = false;

            if (spinAudioSource != null && spinClip != null)
            {
                spinAudioSource.clip = spinClip;
                spinAudioSource.pitch = 1.5f;
                spinAudioSource.volume = 1f;
                spinAudioSource.Play();
            }
        }
    }

    private void DetermineReward()
    {
        float finalAngle = transform.eulerAngles.z % 360;
        float segmentAngle = 360f / numberOfSegments;
        int segmentIndex = Mathf.FloorToInt((finalAngle + segmentAngle / 2f) / segmentAngle) % numberOfSegments;
        segmentIndex = (segmentIndex + numberOfSegments - 2) % numberOfSegments;

        Reward selectedReward = rewards[segmentIndex];

        if (selectedReward.rewardType == RewardType.Character)
        {
            int spinCount = PlayerPrefs.GetInt(SpinCountKey, 0);

            if (spinCount < MinSpinsBeforeCharacter)
            {
                selectedReward.rewardType = RewardType.Diamonds;
                selectedReward.amount = 30;
                PlayerPrefs.SetInt(SpinCountKey, spinCount + 1);
                PlayerPrefs.Save();

                StartCoroutine(WaitForPopup(() => {
                    NewAudioManager.Instance().PlayPopupSound();
                    Toast.Instance.ShowSpinMessage($"You got " + selectedReward.amount + " Diamonds");
                    AddDiamonds(selectedReward.amount);
                    AfterRewardShown();
                }));
            }
            else
            {
                PlayerPrefs.SetInt(SpinCountKey, 0);
                PlayerPrefs.Save();

                if (PlayerPrefs.GetInt(CharacterWonKey + segmentIndex, 0) == 0)
                {
                    PlayerPrefs.SetInt(CharacterWonKey + segmentIndex, 1);
                    PlayerPrefs.Save();

                    if (segmentIndex == 3)
                    {
                        StartCoroutine(WaitForPopup(() => {
                            Toast.Instance.ShowGirlPopup();
                            NewAudioManager.Instance().PlayPopupSound();
                            PlayerPrefs.SetInt($"HotgBought", 1);
                            PlayerPrefs.Save();
                            AfterRewardShown();
                        }));
                    }
                    else if (segmentIndex == 8)
                    {
                        StartCoroutine(WaitForPopup(() => {
                            Toast.Instance.ShowBoyPopup();
                            NewAudioManager.Instance().PlayPopupSound();
                            PlayerPrefs.SetInt($"HotbBought", 1);
                            PlayerPrefs.Save();
                            AfterRewardShown();
                        }));
                    }
                }
                else
                {
                    Debug.Log("======else bolck");
                    selectedReward.rewardType = RewardType.Diamonds;
                    selectedReward.amount = 30;
                    Debug.Log("======else bolck" + selectedReward.amount+" reward ");

                    StartCoroutine(WaitForPopup(() => {
                        NewAudioManager.Instance().PlayPopupSound();
                        Toast.Instance.ShowSpinMessage($"You already unlocked this character, so you received {selectedReward.amount} Diamonds instead.");
                        AddDiamonds(selectedReward.amount);
                        AfterRewardShown();
                    }));
                }
            }
        }
        else if (selectedReward.rewardType == RewardType.BadLuck)
        {
            if (segmentIndex == 2 || segmentIndex == 6)
            {
                StartCoroutine(WaitForPopup(() => {
                    NewAudioManager.Instance().BadLuckPopupSound();
                    Toast.Instance.showBetterLuckPopup();
                    AfterRewardShown();
                }));
            }
        }

        else if (selectedReward.rewardType == RewardType.Diamonds || segmentIndex == 8 || segmentIndex == 3)
        {

            StartCoroutine(WaitForPopup(() => {
                NewAudioManager.Instance().PlayPopupSound();
                Toast.Instance.ShowSpinMessage($"You got " + selectedReward.amount + " Diamonds");
                AddDiamonds(selectedReward.amount);
                AfterRewardShown();
            }));
        }
    }

    private void AddDiamonds(int amount)
    {
        if (PlayerPrefs.GetInt("GuestLogin") == 1)
        {
            diamondCount = PlayerPrefs.GetInt("UserGems");
            diamondCount += amount;
            UiManager.Instance.DiamondUpdate(diamondCount);
            PlayerPrefs.SetInt("UserGems", diamondCount);
            DataSaver.Instance.GuestData.diamonds = diamondCount;
            PlayerPrefs.Save();
        }
        else if (DataSaver.Instance.userId != null)
        {
            int diamondCount = DataSaver.Instance.UserData.diamonds;
            diamondCount += amount;
            DataSaver.Instance.UserData.diamonds = diamondCount;
            UiManager.Instance.DiamondUpdate(diamondCount);
        }
    }

    private IEnumerator WaitForPopup(Action callback)
    {
        yield return new WaitForSeconds(0.5f);
        callback?.Invoke();
    }

    private void AfterRewardShown()
    {
        SaveLastSpinTime();
        StartTimerUpdate();
    }

    public void Close()
    {
        spinPanel.gameObject.SetActive(false);
    }
}
