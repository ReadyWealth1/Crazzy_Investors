using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace Jio.SampleGame
{
    public class GamePlay : MonoBehaviour
    {
        public Transform ansList;
        public Transform que;
        // public Text timeText;
        public Text scoreText;
        // public Transform lifesTransform;

        public LifePanel lifePanel;

        public List<int> randNumbers = new List<int>();

        int seconds = 5;
        int startCharacter = 65;
        int correctAnsPos;

        bool isGameOver = false;
        int localScore;
        bool isGamePause = false;
        bool isGamePauseSelected = false;
        int pauseId = 0;

        int npCount = 0;
        int ansPos;

        private void OnEnable()
        {
            isGameOver = false;
            seconds = 60;
            isGamePause = false;
            isGamePauseSelected = false;
            pauseId = 0;

            localScore = 0;
            scoreText.text = "Score : " + localScore;

            // This initialize for gettting after the RV
            //JioWrapperJS.Instance.GratifyRewards.AddListener(GratifyRewards);

            Reset();

            npCount = 0;
        }

        private void Reset()
        {
            CancelInvoke();
            correctAnsPos = -1;
            startCharacter = UnityEngine.Random.Range(65, 88);
            for (int i = 0; i < que.childCount; i++)
            {
                que.GetChild(i).localScale = new Vector3(1.0f, 1.0f, 1.0f);
                que.GetChild(i).GetChild(0).GetComponent<Text>().text = "" + Convert.ToChar(startCharacter + i);
                que.GetChild(i).GetChild(0).GetComponent<Text>().color = Color.white;
            }
            ansPos = UnityEngine.Random.Range(0, que.childCount);
            que.GetChild(ansPos).GetChild(0).GetComponent<Text>().text = "?";
            que.GetChild(ansPos).GetChild(0).GetComponent<Text>().color = Color.yellow; //E6FF00

            que.GetChild(ansPos).GetComponent<Animation>().Play();

            randNumbers = GetRandomNumber(65, 90, ansList.childCount);
            for (int i = 0; i < ansList.childCount; i++)
            {
                ansList.GetChild(i).GetChild(0).GetComponent<Text>().text = "" + Convert.ToChar(randNumbers[i]);
                ansList.GetChild(i).gameObject.SetActive(true);
                if (startCharacter + ansPos == randNumbers[i])
                {
                    correctAnsPos = i;
                }
            }

            if (correctAnsPos == -1)
            {
                correctAnsPos = UnityEngine.Random.Range(0, ansList.childCount);
                randNumbers[correctAnsPos] = startCharacter + ansPos;
            }
            ansList.GetChild(correctAnsPos).GetChild(0).GetComponent<Text>().text = "" + Convert.ToChar(startCharacter + ansPos);

            // timeText.text = "" + seconds;
            // InvokeRepeating("Timer", 1, 1);
        }

        private void OnDisable()
        {
            que.GetChild(ansPos).GetComponent<Animation>().Stop();
            CancelInvoke();
            //JioWrapperJS.Instance?.GratifyRewards.RemoveListener(GratifyRewards);
        }

        void Timer()
        {
            // if (isRVPause)
            //     return;

            // timeText.text = "" + seconds;
            seconds -= 1;
            if (seconds < 0)
            {
                isGameOver = true;
                CancelInvoke();
                lifePanel.GameOver();
            }
        }

        public void OnClick_Back()
        {
            UIManager.Instance.gameplayScreen.SetActive(false);
            UIManager.Instance.mainScreen.SetActive(true);
        }

        public void OnClick_Ans(int num)
        {
            if (isGameOver)
                return;

            if (correctAnsPos == num)
            {
                AudioManager.Instance.Audio_RightAnswer();
                for (int i = 0; i < ansList.childCount; i++)
                {
                    ansList.GetChild(i).gameObject.SetActive(false);
                }
                que.GetChild(ansPos).GetChild(0).GetComponent<Text>().text = "" + Convert.ToChar(randNumbers[correctAnsPos]);
                que.GetChild(ansPos).GetComponent<Animation>().Stop();

                DOTween.Sequence().AppendInterval(2.0f).OnComplete(()=>{
                    Reset();
                });
                
                localScore += 1;
                scoreText.text = "Score : " + localScore;

                if (PlayerPrefs.GetInt("HighScore") < localScore)
                {
                    PlayerPrefs.SetInt("HighScore", localScore);
                }
            }
            else
            {
                AudioManager.Instance.Audio_WrongAnswer();

                ansList.GetChild(num).GetChild(1).gameObject.SetActive(true);
                int k = num;
                DOTween.Sequence().AppendInterval(0.5f).OnComplete(()=>{
                    ansList.GetChild(k).GetChild(1).gameObject.SetActive(false);
                });

                lifePanel.LifeDecrease();
            }
        }

        public List<int> GetRandomNumber(int from, int to, int numberOfElement)
        {
            var random = new System.Random();
            List<int> listNumbers = new List<int>();
            int number;
            for (int i = 0; i < numberOfElement; i++)
            {
                do
                {
                    number = random.Next(from, to);
                } while (listNumbers.Contains(number));
                listNumbers.Add(number);
            }
            return listNumbers;
        }

        /// Life Screen
        ///
        void GratifyRewards()
        {
            lifePanel.LifeIncrease();
            Debug.Log("GratifyRewards");
            UIManager.Instance.lifeoverPopup.SetActive(false);
            Reset();
        }

        public void OnClick_YES()
        {
            AudioManager.Instance.Audio_ButtonPress();
            //JioWrapperJS.Instance.showRewarded();
        }

        public void OnClick_NO()
        {
            AudioManager.Instance.Audio_ButtonPress();
            lifePanel.GameOver();
        }

        public void OnClick_Pause()
        {
            AudioManager.Instance.Audio_ButtonPress();
            UIManager.Instance.pausePopup.SetActive(true);
        }
    }
}