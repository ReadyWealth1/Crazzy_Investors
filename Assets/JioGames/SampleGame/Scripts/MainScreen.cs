using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jio.SampleGame
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] internal Image imgSound;

        private void OnEnable() {
            AudioManager.Instance.Audio_BG(true);

            if(AudioManager.Instance.isAudioEnable){
                imgSound.sprite = AudioManager.Instance.spzSoundOn;
            }
            else{
                imgSound.sprite = AudioManager.Instance.spzSoundOff;
            }
        }
        public void OnClick_Play()
        {
            AudioManager.Instance.Audio_BG(false);
            AudioManager.Instance.Audio_ButtonPress();

            UIManager.Instance.mainScreen.SetActive(false);
            UIManager.Instance.gameplayScreen.SetActive(true);

           // JioWrapperJS.Instance.getUserProfile();

            /// <summary>
            ///     Call the MidRoll and RV ad.
            /// </summary>
            //JioWrapperJS.Instance.cacheAd();
            

        }
        public void OnClick_Info()
        {
            AudioManager.Instance.Audio_ButtonPress();
            UIManager.Instance.infoPopup.SetActive(true);
            //JioWrapperJS.Instance.setTopBanner();
        }

        public void OnClick_InfoBack()
        {
            AudioManager.Instance.Audio_ButtonPress();
            UIManager.Instance.infoPopup.SetActive(false);

            //JioWrapperJS.Instance.setBottomBanner();
        }

        public void OnClick_SoundOnOff()
        {
            AudioManager.Instance.Audio_ButtonPress();
            AudioManager.Instance.Audio_OnOff();

            if(AudioManager.Instance.isAudioEnable){
                imgSound.sprite = AudioManager.Instance.spzSoundOn;
            }
            else{
                imgSound.sprite = AudioManager.Instance.spzSoundOff;
            }
        }
    }
}
