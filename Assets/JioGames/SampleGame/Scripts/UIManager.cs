using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jio.SampleGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public GameObject mainScreen;
        public GameObject gameplayScreen;
        public GameObject gameoverScreen;
        public GameObject lifeoverPopup;
        public GameObject infoPopup;
        public GameObject pausePopup;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

        }

      /*  private void Start()
        {
            mainScreen.SetActive(true);

            JioWrapperJS.Instance?.loadBanner();
            StartCoroutine(BannerShow());
        }*/

        IEnumerator BannerShow(){
            yield return new WaitForSeconds(5);
            Debug.Log("Unity BannerShow");
            //JioWrapperJS.Instance?.showBanner();
        }
    }
}