using System;
using System.Collections;
using System.Collections.Generic;
using com.jiogames.wrapper;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    [Header("Diamond Text")]
    [SerializeField] private TextMeshProUGUI diamondHomeText;
    [SerializeField] private TextMeshProUGUI diamondShopText;
    private TextMeshProUGUI userNameTxt;
    [SerializeField] private Button adsButton;
    [SerializeField] private Button adsButtonToast;

    [SerializeField] private GameObject welcomeScreen;
    private int diamondCount;


    //[Header("panels")]
    //[SerializeField] private GameObject settingPanel;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Debug.Log("Uimanager Call");
        Instance = this;
        DontDestroyOnLoad(gameObject);
       //  && PlayerPrefs.GetInt("GoogleLoggedIn") == 0)
       /* if ((PlayerPrefs.GetInt("GuestLogin") == 0))
        {
           
            OpenWelComeScreen();
        }
        else
        {
            CloseWelComeScreen();
           
        }*/

    }
    public void Update()
    {
        if(JioWrapperJS.Instance.IsRVReady == true)
        {
            adsButton.interactable = true;
            adsButtonToast.interactable = true;
           
        }
        else
        {
            adsButton.interactable = false;
            adsButtonToast.interactable = false;
        }
    }

    public void UpdateNameText(string name)
    {
        userNameTxt.text = name;
    }

    public void UpdateDimondTextOnly(int count)
    {
        diamondHomeText.text = count.ToString();
        diamondShopText.text = count.ToString();
    }

    public void DiamondUpdate(int count)
    {
        
        // count += PlayerPrefs.GetInt("UserGems");
      
        diamondCount = count;

        diamondHomeText.text = diamondCount.ToString();
        diamondShopText.text = diamondCount.ToString();
        PlayerPrefs.SetInt("UserGems", diamondCount);

        //  DataSaver.Instance.GuestData.diamonds = diamondCount;
        //*  userNameTxt.text = "Guest";*//*

        PlayerPrefs.Save();
        /* if (PlayerPrefs.GetInt("GuestLogin") == 1)
         {
             PlayerPrefs.SetInt("UserGems", diamondCount);
           //  DataSaver.Instance.GuestData.diamonds = diamondCount;
           *//*  userNameTxt.text = "Guest";*//*

             PlayerPrefs.Save();
         }
         else
         {
             Debug.Log("CRUCIAL!!!: " + DataSaver.Instance.userId);
             DataSaver.Instance.UserData.diamonds = diamondCount;

             userNameTxt.text = DataSaver.Instance.UserData.userName;
             //DataSaver.Instance.SaveDataFn();
         }*/

    }

    public void OpenWelComeScreen()
    {
        welcomeScreen.SetActive(true);
    }
    public void CloseWelComeScreen()
    {
        welcomeScreen.SetActive(false);

    }

}
