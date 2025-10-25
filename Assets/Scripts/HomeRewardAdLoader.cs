using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeRewardAdLoader : MonoBehaviour
{
    private bool hasRequestedReward = false;

    private void OnEnable()
    {
        StartCoroutine(InitAfterDelay());
    }

 /*   private void OnDisable()
    {
        if (JioWrapperJS.Instance != null)
        {
            JioWrapperJS.Instance.GratifyRewards.RemoveListener(OnRewardAdClosed);
        }
    }*/

    private IEnumerator InitAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Wait to ensure JioWrapperJS is ready

        /*if (JioWrapperJS.Instance != null)
        {
            //JioWrapperJS.Instance.GratifyRewards.AddListener(OnRewardAdClosed);
            LoadRewardedAdIfNeeded();
        }
        else
        {
            Debug.LogWarning("HomeRewardAdLoader: JioWrapperJS.Instance is null after delay.");
        }*/
    }

    private void LoadRewardedAdIfNeeded()
    {
      /*  if (!JioWrapperJS.Instance.IsRVReady && !hasRequestedReward)
        {
            hasRequestedReward = true;
            JioWrapperJS.Instance.cacheRewarded();
            Debug.Log("HomeRewardAdLoader: Rewarded ad caching requested.");
        }*/
    }

    private void OnRewardAdClosed()
    {
        // Reset and try loading again
        hasRequestedReward = false;
        LoadRewardedAdIfNeeded();
    }
}
