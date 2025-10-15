using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Check if PersistentManagerScene is already loaded
        if (!IsSceneLoaded("PersistentManagerScene"))
        {
            SceneManager.LoadScene("PersistentManagerScene", LoadSceneMode.Additive);
        }

        // Set the main scene as the active scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set the active scene to the main scene after loading PersistentManagerScene
        if (scene.name == "new onwrinner samplescene") // Replace with your main scene name
        {
            Debug.Log("CCheck");
            SceneManager.SetActiveScene(scene);
            if (PlayerPrefs.GetInt("GuestLogin") == 1)
            {
                UiManager.Instance.DiamondUpdate(PlayerPrefs.GetInt("UserGems"));
            }
            else
            {
                UiManager.Instance.DiamondUpdate(PlayerPrefs.GetInt("UserGems"));
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
