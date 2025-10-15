using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] private GameObject splash;
    [SerializeField] private GameObject logo;
    [SerializeField] private float time;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI txtPercentage;

    private bool isSceneLoading;

    private void Start()
    {
        Application.targetFrameRate = 120;
        /*PlayerPrefs.SetInt("ShowChallenges", 1);
        PlayerPrefs.SetInt("GetHomeGetUserApiCall", 0);*/
        logo.SetActive(true);
        splash.SetActive(false);
        StartCoroutine(DelaySplash());
    }

    IEnumerator DelaySplash()
    {
        yield return new WaitForSeconds(1f);
        splash.SetActive(true);
        StartCoroutine(LoadSceneAsync("new onwrinner samplescene")); // Replace "HomeScene" with your target scene name
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        txtPercentage.text = "0%";
        loadingSlider.value = 0;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Update the slider and percentage
            float progress = Mathf.Clamp01(asyncOperation.progress );
            loadingSlider.value = progress;
            txtPercentage.text = Mathf.RoundToInt(progress * 100) + "%";

            // Check if loading is complete
            if (asyncOperation.progress >= 0.9f)
            {
                txtPercentage.text = "95%";
                loadingSlider.value = 0.95f;
                asyncOperation.allowSceneActivation = true; // Activate the scene
            }

            yield return null;
        }
    }
}
