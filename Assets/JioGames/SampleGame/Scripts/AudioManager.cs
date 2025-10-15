using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jiogames.wrapper;

namespace Jio.SampleGame
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        AudioSource audio;
        [SerializeField] internal AudioSource bgAudio;

        [Header("Audio Files"), Space]
        [SerializeField] internal AudioClip clipBG;
        [SerializeField] internal AudioClip clipButtonPress;
        [SerializeField] internal AudioClip clipRightAnswer;
        [SerializeField] internal AudioClip clipWrongAnswer;
        [SerializeField] internal AudioClip clipGameOver;

        [Header("Sprite Files"), Space]
        [SerializeField] internal Sprite spzSoundOff;
        [SerializeField] internal Sprite spzSoundOn;


        internal bool isAudioEnable {get; private set;}

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            audio = GetComponent<AudioSource>();
            bgAudio.clip = clipBG;
            isAudioEnable = true;
        }

        internal void Audio_BG(bool isPlay){
            if(isPlay)
                bgAudio.Play();
            else
                bgAudio.Stop();
        }

        internal void Audio_ButtonPress(){
            audio.clip = clipButtonPress;
            audio.Play();
        }
        internal void Audio_RightAnswer(){
            audio.clip = clipRightAnswer;
            audio.Play();
        }
        internal void Audio_WrongAnswer(){
            audio.clip = clipWrongAnswer;
            audio.Play();
        }
        internal void Audio_GameOver(){
            audio.clip = clipGameOver;
            audio.Play();
        }

        internal void Audio_OnOff(){
            isAudioEnable = !isAudioEnable;
            Debug.Log(isAudioEnable);

            AudioListener.pause = !isAudioEnable; 
            audio.mute = !isAudioEnable;
            bgAudio.mute = !isAudioEnable;
        }

        internal void PauseResume(bool isPause){
            AudioListener.pause = isPause; 
            audio.mute = isPause;
            bgAudio.mute = isPause;
        }

        void OnApplicationFocus(bool hasFocus) {
            Debug.Log("OnApplicationFocus : " + hasFocus);
            if (!hasFocus) {
                AudioListener.pause = true;
            } else {
                AudioListener.pause = false;
            }
        }
    }
}