using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jio.SampleGame
{
    public class PausePopup : MonoBehaviour
    {
        [SerializeField] internal Image imgSound;

        private void OnEnable() {
            if(AudioManager.Instance.isAudioEnable){
                imgSound.sprite = AudioManager.Instance.spzSoundOn;
            }
            else{
                imgSound.sprite = AudioManager.Instance.spzSoundOff;
            }

            //JioWrapperJS.Instance.hideBanner();
        }

        private void OnDisable() {
            //JioWrapperJS.Instance.showBanner();    
        }

        public void OnClick_Resume(){
            AudioManager.Instance.Audio_ButtonPress();
            gameObject.SetActive(false);
        }
        public void OnClick_Restart(){
            AudioManager.Instance.Audio_ButtonPress();
            UIManager.Instance.gameplayScreen.SetActive(false);
            gameObject.SetActive(false);
            UIManager.Instance.gameplayScreen.SetActive(true);
        }
        public void OnClick_Home(){
            AudioManager.Instance.Audio_ButtonPress();
            gameObject.SetActive(false);
            UIManager.Instance.gameplayScreen.SetActive(false);
            UIManager.Instance.mainScreen.SetActive(true);

         //   JioWrapperJS.Instance.showInterstitial();
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