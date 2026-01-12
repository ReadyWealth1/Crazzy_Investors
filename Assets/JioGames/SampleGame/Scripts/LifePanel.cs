using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.jiogames.wrapper;

namespace Jio.SampleGame
{
    public class LifePanel : MonoBehaviour
    {
        int lifeCount = 3;
        bool isRVPause = false;
        bool firstTimeRV = true;

        void OnEnable(){
            lifeCount = 2;
            isRVPause = false;
            firstTimeRV = true;

            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        void OnDisable(){
            lifeCount = 2;
            isRVPause = false;
            firstTimeRV = true;
        }

        public void LifeDecrease(){
            transform.GetChild(lifeCount).gameObject.SetActive(false);
             lifeCount -= 1;
                if (lifeCount < 0)
                {
                    if (!firstTimeRV)
                    {
                        GameOver();
                    }
                    else
                    {
                        firstTimeRV = false;

                        //If RV ad is available then open the RV popup otherwise open the game over screen
                        if(JioWrapperJS.Instance.IsRVReady)
                            UIManager.Instance.lifeoverPopup.SetActive(true);
                        else
                            GameOver();
                    }
                }
        }

        public void LifeIncrease(){
            lifeCount = 0;
            transform.GetChild(lifeCount).gameObject.SetActive(true);
        }

        public void GameOver()
        {
            //Show MidRoll AD.
            JioWrapperJS.Instance.postScore(PlayerPrefs.GetInt("HighScore"));
            JioWrapperJS.Instance.showInterstitial();

            UIManager.Instance.lifeoverPopup.SetActive(false);
            UIManager.Instance.gameoverScreen.SetActive(true);
            UIManager.Instance.gameplayScreen.SetActive(false);
        }

    }
}