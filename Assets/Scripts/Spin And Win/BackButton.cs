using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackButton : MonoBehaviour
{

    private static BackButton instance;

    public static BackButton Instance()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private UnityAction backButton;


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            OnBackButtonPressed();
        }
    }

    public void OnBackButtonPressed()
    {
        if(backButton != null)
        {
            backButton.Invoke();
        }
    }

    public void SetBackButtonCallback(UnityAction action)
    {
        backButton = action;
    }

}
