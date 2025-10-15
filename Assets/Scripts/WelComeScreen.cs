using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelComeScreen : MonoBehaviour//, LoginCallBack
{
  // public Button googleLogin;
    public Button guestLogin;

    public void Start()
    {
        //guestLogin.onClick.AddListener(GuestLogin);
       // googleLogin.onClick.AddListener(GoogleLogin);
    }
    public void GoogleLogin()
    {
       
       // GoogleController.instance.OnSignIn();

    }

    public void GuestLogin()
    {
        PlayerPrefs.SetInt("GuestLogin", 1);
        PlayerPrefs.SetInt("FirstTimeOpen", 1);
        Debug.Log("====FirstTimeOpen Call",this);
        //  Toast.Instance.ShowSpinMessage("you got" + 20 + " Diamonds");
        PlayerPrefs.SetInt("UserGems", 0);
      //  PlayerPrefs.SetInt("FirstTimeOpen", 1);
        DataSaver.Instance.GuestData.diamonds = 0;
        UiManager.Instance.UpdateNameText("Guest");
        UiManager.Instance.DiamondUpdate(0); 
       // UiManager.Instance.CloseWelComeScreen();
    }

    /*public void OnGoogleLoginComplete(string name, string userId)
    {
    
        DataSaver.Instance.userId = userId;
        UiManager.Instance.CloseWelComeScreen();


        DataSaver.Instance.UserData = new UserData(name, 0, 0);
        DataSaver.Instance.UserData.userName = name;

        DataSaver.Instance.LoadDataFn();
        UiManager.Instance.UpdateNameText(name);

      *//*  displayName.SetActive(true);

        loginBtn.gameObject.SetActive(false);

        logoutBtn.gameObject.SetActive(true);

        userNameText.text = name;*//*
        PlayerPrefs.SetInt("FirstTimeOpen", 1);
    }

    public void OnLogoutComplete()
    {
        PlayerPrefs.DeleteKey("GoogleUserName");
        PlayerPrefs.SetInt("GoogleLoggedIn", 0);
        PlayerPrefs.SetInt("GuestLogin", 1);
        PlayerPrefs.Save();
        UiManager.Instance.DiamondUpdate(PlayerPrefs.GetInt("UserGems"));
        UiManager.Instance.UpdateNameText("Guest");
        FindObjectOfType<HighScoreManager>().SetHighScore();
       // UpdateUserUI();
    }*/
}
