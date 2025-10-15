using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    //public Character character;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ReplayGame1();
        }
    }
    public void ReplayGame()
    {
        //character.ResetCharacter();
        SceneManager.LoadScene("new onwrinner samplescene");
    }

    public void ReplayGame1()
    {
        //character.ResetCharacter();
        SceneManager.LoadScene("new onwrinner samplescene");
    }

}