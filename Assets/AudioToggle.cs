using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Example: Check if AudioManager instance exists and then toggle mute
        if (AudioManager.instance != null)
        {
            //AudioManager.instance.ToggleMute();
        }
        else
        {
            Debug.LogError("AudioManager instance not found. Make sure the AudioManager is in the scene.");
        }
    }
    public void TogAudio()
    {
        if (AudioManager.instance != null)
        {
           // AudioManager.instance.ToggleMute();
        }
    }
    // Update is called once per frame
   /* void Update()
    {
        // Example: Toggle mute on key press (e.g., M key)
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.ToggleMute();
            }
        }
    }*/
}
