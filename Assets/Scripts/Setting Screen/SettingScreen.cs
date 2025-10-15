using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SettingScreen : MonoBehaviour, LoginCallBack
{
    [SerializeField] private Button btnVibration;
    [SerializeField] private GameObject vibrationOn;
    [SerializeField] private GameObject vibrationOff;
    /*[SerializeField] private Button loginBtn;
    [SerializeField] private Button logoutBtn;
*/
    /*[SerializeField] private GameObject displayName;
    [SerializeField] private TextMeshProUGUI userNameText;

*/

    public static SettingScreen instance;


   /* public void UpdateText(string text)
    {
        userNameText.text = text;
    }*/

    public static SettingScreen Instance()
    {
        return instance;
    }
    public void Awake()
    {
        if (instance == null)
        {
            // If no instance exists, set this as the instance and don't destroy it on load
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //UpdateText(GameData.userdetails.userName);
        //userName
    }


    public void OnEnable()
    {
        // GoogleController.instance.OnSignInSilently();
        if (PlayerPrefs.GetInt("GoogleLoggedIn") == 1)
        {
            //userNameText.text = PlayerPrefs.GetString("GoogleUserName");
            //txtLevelName.text = GameData.userdetails.userName;
            // Debug.Log("txtLevelName" + GameData.userdetails.userName);
        }
    }

    private void Start()
    {
        //if(DataSaver.Instance.userId != null)
        //{
        //    DataSaver.Instance.LoadDataFn();
        //}

        //IronSourceAdsManager.instance.ShowBanner();
       /* loginBtn.onClick.AddListener(GoogleLogin);
        logoutBtn.onClick.AddListener(OnLogOut);*/
        btnVibration.onClick.AddListener(ToggleVibration);

        if (!PlayerPrefs.HasKey("Vibration"))
        {
            PlayerPrefs.SetInt("Vibration", 1);
            PlayerPrefs.Save();
        }
      //  GoogleController.instance.SetLoginCallback(this);
        UpdateImage();
       // UpdateUserUI();
    }

    public void UpdateUserUI()
    {
        if (PlayerPrefs.GetInt("GoogleLoggedIn") == 1)
        {
            Debug.Log("login mathad call ++++++");
           /* displayName.SetActive(true);
            loginBtn.gameObject.SetActive(false);
            logoutBtn.gameObject.SetActive(true);*/

            if (PlayerPrefs.HasKey("GoogleUserName"))
            {
                Debug.Log("user name mathad call ++++++");
                string userName = PlayerPrefs.GetString("GoogleUserName");
                //userNameText.text = userName;
                Debug.Log("User Name Updated: " + userName);
            }
        }
        else
        {
         /*   displayName.SetActive(false);
            loginBtn.gameObject.SetActive(true);
            logoutBtn.gameObject.SetActive(false);*/
        }
    }

    public void GoogleLogin()
    {

        if (InternetConnectivity.Instance.HasInternet())
        {
            Debug.Log("Internet is available. Proceed with login.");
          //  GoogleController.instance.OnSignIn();
            Toast.Instance.CloseInternetPopup();


        }
        else
        {
            Debug.Log("No internet. Show popup.");
           // Toast.Instance.OpenInternetPopup();
            //internetPanel.SetActive(true);
        }
      

        // Calls OnSignIn from GoogleController
    }

    /*  public void OnGoogleLogout()
      {

          GoogleController.instance.OnSignOut();
          Debug.Log("OnGoogleLogOut ");
          // Clear stored username and update UI
          PlayerPrefs.DeleteKey("GoogleUserName");
          PlayerPrefs.SetInt("GoogleLoggedIn", 0);
          PlayerPrefs.Save();

          UpdateUserUI();
      }*/

    private void ToggleVibration()
    {
        int currentVibration = PlayerPrefs.GetInt("Vibration", 1);
        int newVibration = (currentVibration == 0) ? 1 : 0;

        PlayerPrefs.SetInt("Vibration", newVibration);
        PlayerPrefs.Save();
        UpdateImage();

    }

    private void UpdateImage()
    {
        bool isVibrationOn = PlayerPrefs.GetInt("Vibration") == 1;

        // Toggle vibration icons
        vibrationOn.SetActive(isVibrationOn);
        vibrationOff.SetActive(!isVibrationOn);

        btnVibration.image.color = isVibrationOn ? Color.green : Color.red;
    }

    public void OnGoogleLoginComplete(string name, string userId)
    {
        Debug.Log("OnGoogleLoginComplete");

        // Save login info
        PlayerPrefs.SetString("UserId", userId);
        PlayerPrefs.SetString("GoogleUserName", name);
        PlayerPrefs.SetInt("GoogleLoggedIn", 1);
        PlayerPrefs.SetInt("FirstTimeOpen", 1);
        PlayerPrefs.Save();

        DataSaver.Instance.userId = userId;

        // Load Firebase data
        DataSaver.Instance.LoadDataFn();

        //  Handle data after it is loaded from Firebase
        DataSaver.Instance.OnFirebaseDataLoaded += () =>
        {
            //  CHECK IF DATA EXISTS
            if (DataSaver.Instance.UserData == null || DataSaver.Instance.UserData.userName != name)
            {
                Debug.Log("Creating new user data...");
                DataSaver.Instance.UserData = new UserData(name, 0, 0); // default values
                DataSaver.Instance.SaveDataFn();
            }

            //  Update UI with Firebase or new data
           /* UiManager.Instance.UpdateNameText(DataSaver.Instance.UserData.userName);
            UiManager.Instance.DiamondUpdate(DataSaver.Instance.UserData.diamonds);*/
            /*userNameText.text = DataSaver.Instance.UserData.userName;*/

          /*  displayName.SetActive(true);
            loginBtn.gameObject.SetActive(false);
            logoutBtn.gameObject.SetActive(true);*/

            UpdateUserUI();
        };

       // UiManager.Instance.CloseWelComeScreen();
        InternetConnectivity.Instance.StartMonitoringInternet();
    }

    public void OnLogOut()
    {
        //DataSaver.Instance.SaveDataFn();
       // GoogleController.instance.OnSignOut();
    }

    public void OnLogoutComplete()
    {
        Debug.Log("OnGoogleLogOut ");
        // Clear stored username and update UI
        PlayerPrefs.DeleteKey("GoogleUserName");
        PlayerPrefs.SetInt("GoogleLoggedIn", 0);
        PlayerPrefs.SetInt("GuestLogin", 1);
        InternetConnectivity.Instance.OffMonitoringInternet();
        PlayerPrefs.Save();
        UiManager.Instance.DiamondUpdate(PlayerPrefs.GetInt("UserGems"));
        UiManager.Instance.UpdateNameText("Guest");

     //   FindObjectOfType<HighScoreManager>().SetHighScore();
        UpdateUserUI();
    }
}
