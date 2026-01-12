using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
   public static Toast Instance { get; private set; }

    [SerializeField] private GameObject messagePanel;
    [SerializeField] private GameObject messagePanel1;
    [SerializeField] private GameObject messageShop;
    [SerializeField] private GameObject messagePowerUp;
    [SerializeField] private GameObject messageFreeReward;
    [SerializeField] private GameObject messageINR;
    [SerializeField] private GameObject msgchallengeReject;
    

    [SerializeField] private TextMeshProUGUI txtMessage;
    [SerializeField] private TextMeshProUGUI txtMessage1;
    [SerializeField] private TextMeshProUGUI txtMoveCount;
    [SerializeField] private TextMeshProUGUI txtPowerUp;
    [SerializeField] private TextMeshProUGUI txtFreeCoin;
    [SerializeField] private TextMeshProUGUI txtInrCoin;
    
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnClose1;
    [SerializeField] private Button btnCloseReject;
    [SerializeField] private Button btnCloseMessage;
    [SerializeField] private Button btnClosePowerUp;
    [SerializeField] private Button btnCloseFreeReward;
   
    private void Awake()
    {
        if (Instance) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
        btnClose.onClick.AddListener(Close);
        btnClose1.onClick.AddListener(OpenReward);
        btnCloseReject.onClick.AddListener(RejectClose);    
        btnCloseMessage.onClick.AddListener(ClosedMessageShop);
        btnClosePowerUp.onClick.AddListener(ClosedPowerUp);
        btnCloseFreeReward.onClick.AddListener(ClosedFreeReward);
    }
    public void RejectChallenge()
    {
        AudioManager.Instance().PanelOpen();
        msgchallengeReject.SetActive(true);
    }
    public void ShowMessage(string message)
    {
        AudioManager.Instance().PanelOpen();
        messagePanel.SetActive(true);
        BackButton.Instance().SetBackButtonCallback(Close);
        txtMessage.text = message;
    }
    public void ShowMessage1(string message)
    {
        AudioManager.Instance().PanelOpen();
        messagePanel1.SetActive(true);
        BackButton.Instance().SetBackButtonCallback(Close1);
        txtMessage1.text = message;
    }
    public void PowerUpShow(int powerUp)
    {
        AudioManager.Instance().PanelOpen();
        messagePowerUp.SetActive(true);
        BackButton.Instance().SetBackButtonCallback(ClosedPowerUp);
        txtPowerUp.text = powerUp.ToString();

    }
    public void MessageFreeReward(int CoinFree)
    {
        AudioManager.Instance().PanelOpen();
        messageFreeReward.SetActive(true);
        BackButton.Instance().SetBackButtonCallback(ClosedFreeReward);
        txtFreeCoin.text = CoinFree.ToString();
    }
    public void ClosedPowerUp()
    {
        AudioManager.Instance().PanleClose();
        messagePowerUp.SetActive(false);
    }
    public void ClosedFreeReward()
    {
        AudioManager.Instance().PanleClose();
        messageFreeReward.SetActive(false);
    }


    public void ShowMessage(string message,UnityAction action)
    {
        AudioManager.Instance().PanelOpen();
        messagePanel.SetActive(true);
        BackButton.Instance().SetBackButtonCallback(Close);
        txtMessage.text = message;
        btnClose.onClick.AddListener(action);
    }

    public void Close()
    {
        
        AudioManager.Instance().PanleClose();
        messagePanel.SetActive(false);
    }
    public void Close1()
    {
        AudioManager.Instance().PanleClose();
        messagePanel1.SetActive(false);
        
    }
    public void RejectClose()
    {
        AudioManager.Instance().PanleClose();
        msgchallengeReject.SetActive(false);
    }
    public void OpenReward()
    {
        AudioManager.Instance().PanleClose();
        messagePanel1.SetActive(false);
        MenuController.Instance().ShopClose();
        MenuController.Instance().OpenRewardPanel();

    }
    public void ClosedMessageShop()
    {
        AudioManager.Instance().PanleClose();
        messageShop.SetActive(false);
    }
    public void OpenShop(int movecount)
    {
        AudioManager.Instance().PanelOpen();
        messageShop.SetActive(true);
        BackButton.Instance().SetBackButtonCallback(ClosedMessageShop);
        txtMoveCount.text=movecount.ToString();

    }

    public void ShowINR(float count)
    {
        AudioManager.Instance().PanelOpen();
        messageINR.SetActive(true);
        txtInrCoin.text = count.ToString();
    }
   
}

