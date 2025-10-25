using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jio.SampleGame
{
    public class GameOver : MonoBehaviour
    {
        public void OnClick_Home()
        {
            AudioManager.Instance.Audio_ButtonPress();
            gameObject.SetActive(false);
            UIManager.Instance.mainScreen.SetActive(true);
        }

        public void OnClick_Restart()
        {
            AudioManager.Instance.Audio_ButtonPress();
            gameObject.SetActive(false);
            UIManager.Instance.gameplayScreen.SetActive(true);

            /// <summary>
            ///     Call the MidRoll and RV ad.
            /// </summary>
            // JioWrapperJS.Instance.cacheAd();
            //JioWrapperJS.Instance.cacheInterstitial();
        }
    }
}