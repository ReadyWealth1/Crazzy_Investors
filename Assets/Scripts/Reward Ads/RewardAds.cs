using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardAds : MonoBehaviour
{
    [SerializeField] private Button rewardAdsBtn;

    private void Start()
    {
       
        rewardAdsBtn.onClick.AddListener(ShowReward);
    }
    private void ShowReward()
    {
       PlayerPrefs.SetInt("HomeRewardBtnClick", 1);
        // IronSourceAdsManager.instance.ShowRewarded();
       // JioWrapperJS.Instance.showRewarded();
       
    }
}
