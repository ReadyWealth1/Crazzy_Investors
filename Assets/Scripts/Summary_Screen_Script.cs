using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summary_Screen_Script : MonoBehaviour
{
    public GameObject[] SellBtns;
    public GameObject[] InvestedBackgrounds;
    public GameObject portObj;
    public GameObject GameOverPanel;

    void Start()
    {
        // Any necessary initialization can go here
    }

    public void ViewInDetail()
    {
        foreach (GameObject btn in SellBtns)
        {
            // Deactivate the Button component to disable button interactions
            Button buttonComponent = btn.GetComponent<Button>();
            Image btnimg = btn.GetComponent<Image>();
            if (buttonComponent != null)
            {
                buttonComponent.enabled = false;
                btnimg.enabled = false;
            }
        }

        // Change portObj's parent to GameOverPanel
        if (portObj != null && GameOverPanel != null)
        {
            portObj.transform.SetParent(GameOverPanel.transform, worldPositionStays: false);
        }

        // Set GameOverPanel's RectTransform to the values shown in the image
        RectTransform rectTransform = GameOverPanel.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector2(0, -13.2f);
            rectTransform.sizeDelta = new Vector2(100, 100); 
            rectTransform.localScale = new Vector3(0.2f, 0.0702f, 1f);
        }

        foreach (GameObject bgs in InvestedBackgrounds)
        {
            bgs.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
