using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using com.jiogames.wrapper;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
   public static Toast Instance { get; private set; }

    [Header("Spin And Win")]
    [SerializeField] private GameObject messageSpinPanel;
   /* [SerializeField] private GameObject showGirlPopup;
    [SerializeField] private GameObject showBoyPopup;*/
    [SerializeField] private GameObject betterLuckNextTime;
    [SerializeField] private GameObject Quit_PopUp;
    [SerializeField] private GameObject adsPopup;
    //[SerializeField] private GameObject InternetPopup;

    [SerializeField] private TextMeshProUGUI spinTxtMessage;
    [SerializeField] private Button btnClose;
   /* [SerializeField] private Button girlCloseBtn;
    [SerializeField] private Button boyCloseBtn;*/
    [SerializeField] private Button betterLuckClose;
    [SerializeField] private Button adsBtn;
  
    



    private void Awake()
    {
    /*    if (Instance) {
            Destroy(gameObject);
            return;
        }*/
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

        // IronSourceAdsManager.instance.ShowBanner();
        btnClose.onClick.AddListener(CloseSpin);
       // girlCloseBtn.onClick.AddListener(CloseGirlPopup);
       // boyCloseBtn.onClick.AddListener(CloseBoyPopup);
        betterLuckClose.onClick.AddListener(ClosebetterLuck);
        adsBtn.onClick.AddListener(ShowAds);
    }

   

    public void OpenInternetPopup()
    {
       // InternetPopup.SetActive(true);
    }
    public void CloseInternetPopup()
    {

       // InternetPopup.SetActive(false );
    }
    public void ShowSpinMessage(string message)
    {
        //AudioManager.Instance().PanelOpen();
        messageSpinPanel.SetActive(true);
        
        spinTxtMessage.text = message;
       // BackButton.Instance().SetBackButtonCallback(CloseSpin);
    }
    public void ShowGirlPopup()
    {
        //showGirlPopup.SetActive(true);
    }
    public void ShowAds()
    {
        CloseAdsPopup();
        PlayerPrefs.SetInt("HomeRewardBtnClick", 1);
        JioWrapperJS.Instance.showRewarded();
        // IronSourceAdsManager.instance.ShowRewarded();
    }
  public void ShowBoyPopup()
    {
       // showBoyPopup.SetActive(true);
    }
    public void showBetterLuckPopup()
    {
        betterLuckNextTime.SetActive(true);
    }
    public void ShowAdsPopup()
    {
        adsPopup.SetActive(true);

    }
    public void CloseAdsPopup()
    {
        adsPopup.SetActive(false);
    }
    public void CloseSpin()
    {

        // AudioManager.Instance().PanleClose();
        messageSpinPanel.SetActive(false);
      
      //  showBoyPopup.SetActive(false);
        betterLuckNextTime.SetActive(false);
    }
    public void CloseGirlPopup()
    {
      // showGirlPopup.SetActive(false);
    }
    public void CloseBoyPopup()
    {
        //showBoyPopup.SetActive(false);
    }
    public void ClosebetterLuck()
    {
        betterLuckNextTime.SetActive(false );
    }
    public void Open_Quit_Panel()
    {
        Quit_PopUp.SetActive(true);
    }
    public void Close_Quit_Panel()
    {
        Quit_PopUp.SetActive(false);
    }
}

