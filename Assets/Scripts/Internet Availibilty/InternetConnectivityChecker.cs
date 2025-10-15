using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InternetConnectivityChecker : MonoBehaviour
{
  
    [SerializeField] private GameObject internet_Panel;
    [SerializeField] private Button retry;
    
   


    void Start()
    {
        // Check internet connectivity when the game starts
        retry.onClick.AddListener(Check);
        CheckInternetConnectivity();
    }
    public void Check()
    {
        CheckInternetConnectivity();
    }
    public void Update()
    {
        CheckInternetConnectivity();
    }
    void CheckInternetConnectivity()
    {
        // Check the current internet reachability status
        NetworkReachability reachability = Application.internetReachability;

        // Check if the device has internet connectivity
        if (reachability == NetworkReachability.NotReachable)
        {
            // No internet connection
            
           
            DisablePlayButton();
            // Optionally, you can disable gameplay elements or show a specific UI message.
            // For example: DisablePlayButton();
        }
        else
        {
            // Internet connection is available
            
         
            EnablePlayButton();
            // Optionally, enable gameplay elements or proceed with the game.
            // For example: EnablePlayButton();
        }
    }

    // Optional: You can use this method to disable a play button or show a specific UI message
    void DisablePlayButton()
    {
        internet_Panel.SetActive(true);
        Time.timeScale = 0f;
       
        //internet_Txt.text = "Please Turn On Your Internet Connection";

        // Your implementation to disable the play button or show a specific UI message
    }

    // Optional: You can use this method to enable a play button or hide a specific UI message
    void EnablePlayButton()
    {
      
        internet_Panel.SetActive(false);
        Time.timeScale = 1.0f;
        
        
        // Your implementation to enable the play button or hide a specific UI message
    }
}
