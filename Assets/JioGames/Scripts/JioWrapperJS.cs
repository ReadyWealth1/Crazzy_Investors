/*using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using System;
using TMPro;
using UnityEngine.UI;

using UnityEngine.UIElements;

namespace com.jiogames.wrapper
{
    public class JioWrapperJS : MonoBehaviour
    {
        internal static JioWrapperJS Instance { get; private set; }
      
        [DllImport("__Internal")]
        private static extern void PostScore(int score);
        [DllImport("__Internal")]
        private static extern void CacheInterstitial(string adKeyId, string package);
        [DllImport("__Internal")]
        private static extern void ShowInterstitial(string adKeyId, string package);
        [DllImport("__Internal")]
        private static extern void CacheRewarded(string adKeyId, string package);
        [DllImport("__Internal")]
        private static extern void ShowRewarded(string adKeyId, string package);
        [DllImport("__Internal")]
        private static extern void GetUserProfile();

        [DllImport("__Internal")]
        private static extern void LoadBanner();
        [DllImport("__Internal")]
        private static extern void SetTopBanner();
        [DllImport("__Internal")]
        private static extern void SetBottomBanner();
        [DllImport("__Internal")]
        private static extern void ShowBanner(string adKeyId, string package);
        [DllImport("__Internal")]
        private static extern void ShowNativeBanner(string adKeyId, string package);
        [DllImport("__Internal")]
        private static extern void HideBanner();
        

        [SerializeField] internal string interstitial_ZoneKey = "5fcpxsh8";
        [SerializeField] internal string rewardedVideo_ZoneKey = "yrf261np";
        [SerializeField] internal string packageName = "com.readywealth.investorsrunSP";

        [SerializeField] internal string banner_ZoneKey = "";
        [SerializeField] internal string banner_PackageName = "com.readywealth.investorsrun";

        internal bool IsAdReady {get; private set;}
        internal bool IsRVReady  {get; private set;}
        internal bool IsRewardUser  {get; private set;}
        
        UserProfileInfo profileInfo;
        internal Detail ProfileInfo { get { return profileInfo.detail; } }

        internal UnityEvent GratifyRewards = new();

        void Awake(){
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
                DontDestroyOnLoad(gameObject);
            }
          // cacheAd();
            Debug.Log("JioGamesJS: SDK initialize : 1.0.0");
        }

        #region "Methods"

        internal void loadBanner() {
            #if !UNITY_EDITOR
                LoadBanner();
            #endif
        }
        internal void setTopBanner() {
            #if !UNITY_EDITOR
                SetTopBanner();
            #endif
        }
        internal void setBottomBanner() {
            #if !UNITY_EDITOR
                SetBottomBanner();
            #endif
        }
        internal void showBanner() {
            #if !UNITY_EDITOR
                ShowBanner(banner_ZoneKey, banner_PackageName);
            #endif
        }
        internal void showNativeBanner() {
            #if !UNITY_EDITOR
                ShowNativeBanner(banner_ZoneKey, banner_PackageName);
            #endif
        }
        internal void hideBanner() {
            #if !UNITY_EDITOR
                HideBanner();
            #endif
        }

        internal void getUserProfile() {
            #if !UNITY_EDITOR
                GetUserProfile();
            #endif
        }

        internal void postScore(int score) {
            #if !UNITY_EDITOR
                PostScore(score);
            #else
                Debug.Log($"JioGamesJS: PostScore : {score}");
            #endif
        }

        internal void cacheInterstitial() {
            if (!IsAdReady) {
                #if !UNITY_EDITOR
                    CacheInterstitial(interstitial_ZoneKey, packageName);
                #else
                    onAdPrepared(interstitial_ZoneKey);
                #endif
            }
        }
        internal void cacheRewarded() {
            if (!IsRVReady) {
                #if !UNITY_EDITOR
                    CacheRewarded(rewardedVideo_ZoneKey, packageName);
                #else
                    onAdPrepared(rewardedVideo_ZoneKey);
                #endif
            }    
        }
        internal void showInterstitial() {
            if (IsAdReady) {
                #if !UNITY_EDITOR
                    ShowInterstitial(interstitial_ZoneKey, packageName);
                #else
                    onAdClosed(interstitial_ZoneKey+"|false|false");
                #endif
            }
        }
        internal void showRewarded() {
            if (IsRVReady) {
                #if !UNITY_EDITOR
                    IsRewardUser=false;
                    ShowRewarded(rewardedVideo_ZoneKey, packageName);
                #else
                    onAdClosed(rewardedVideo_ZoneKey+"|true|true");
                #endif
            }
        }

        // When call the both cacheAd at same time then use this method.
        internal void cacheAd(){
            StartCoroutine(CacheAd());
        }
        private IEnumerator CacheAd()
        {
            cacheInterstitial();
            yield return new WaitForSeconds(5);
            cacheRewarded();
        }
        #endregion

        #region "Callbacks"
        void onAdPrepared(string adSpotKey){
            if(string.Equals(adSpotKey, interstitial_ZoneKey)){
                IsAdReady = true;
                Debug.Log("JioGamesJS: onAdPrepared MidRoll " + IsAdReady);
            }
            else if(string.Equals(adSpotKey, rewardedVideo_ZoneKey)){
                IsRVReady = true;

               // adsButton.gameobject.SetActive(false);
                Debug.Log("JioGamesJS: onAdPrepared RewardedVideo " + IsRVReady);
            }
            else { }
        }
        void onAdClosed(string localData){
            Debug.Log("JioGamesJS: onAdClosed localData : " + localData);

            string[] resData = localData.Split('|');
            string adSpotKey = resData[0];
            bool pIsVideoCompleted = bool.Parse(resData[1]);
            bool pIsEligibleForReward = bool.Parse(resData[2]);

            if(string.Equals(adSpotKey, interstitial_ZoneKey)){
                IsAdReady = false;
                Debug.Log("JioGamesJS: onAdClosed MidRoll " + IsAdReady);
            }
            else if(string.Equals(adSpotKey, rewardedVideo_ZoneKey)){
                IsRVReady = false;
                Debug.Log("JioGamesJS: onAdClosed RewardedVideo " + IsRVReady);

            //Addisional code for give reward amount 
                if ( PlayerPrefs.GetInt("HomeRewardBtnClick") == 1)
                {

                    int diamondCount = PlayerPrefs.GetInt("UserGems");
                    
                    diamondCount += 30;
                   
                    UiManager.Instance.DiamondUpdate(diamondCount);
                    //DataSaver.Instance.GuestData.diamonds= diamondCount;
                    //  PlayerPrefs.SetInt("UserGems", diamondCount);
                    
                    PlayerPrefs.Save();

                    NewAudioManager.Instance().PlayPopupSound();
                    Toast.Instance.ShowSpinMessage("You got " + 30 + " Diamonds");
                    PlayerPrefs.SetInt("HomeRewardBtnClick", 0);
                }
                if (pIsVideoCompleted) {
                    GratifyReward();
                    IsRewardUser = pIsEligibleForReward;
                }
            }
            else { }
        }
        void onAdFailedToLoad(string localData){
            Debug.Log("JioGamesJS: onAdFailedToLoad localData : " + localData);

            string[] resData = localData.Split('|');
            string adSpotKey = resData[0];

            if(string.Equals(adSpotKey, interstitial_ZoneKey)){
                IsAdReady = false;
                Debug.Log("JioGamesJS: onAdFailedToLoad MidRoll " + IsAdReady);
            }
            else if(string.Equals(adSpotKey, rewardedVideo_ZoneKey)){
                IsRVReady = false;
                Debug.Log("JioGamesJS: onAdFailedToLoad RewardedVideo " + IsRVReady);
            }
            else { }
        }

        void onUserProfileResponse(string userInfo){
            Debug.Log("JioGamesJS: onUserProfileResponse Info : " + userInfo);

            profileInfo = JsonUtility.FromJson<UserProfileInfo>(userInfo);
            Debug.Log(ProfileInfo.gamer_id);
            Debug.Log(ProfileInfo.gamer_name);
            Debug.Log(ProfileInfo.gamer_avatar_url);
            Debug.Log(ProfileInfo.device_type);
            Debug.Log(ProfileInfo.dob);
        }
        #endregion

        void GratifyReward(){
            Debug.Log("JioGamesJS : GratifyReward : Event Called...");
            GratifyRewards?.Invoke();
        }

        #region "Forground and Background"

        void onClientPause(){
            Debug.Log("JioGamesJS : onClientPause");
        }
        void onClientResume(){
            Debug.Log("JioGamesJS : onClientResume");
        }
        void ResumeGameSound(){
            Debug.Log("JioGamesJS : ResumeGameSound");
            Jio.SampleGame.AudioManager.Instance.PauseResume(false);
        }
        void PauseGameSound(){
            Debug.Log("JioGamesJS : PauseGameSound");
            Jio.SampleGame.AudioManager.Instance.PauseResume(true);
        }


        #endregion
    }

    [Serializable]
    public class Detail
    {
        public string gamer_id;
        public string gamer_name;
        public string gamer_avatar_url;
        public string device_type;
        public string dob;
    }
    [Serializable]
    public class UserProfileInfo
    {
        public Detail detail;
    }
}*/