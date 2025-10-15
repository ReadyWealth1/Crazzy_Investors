using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Timer : MonoBehaviour
{
   

    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;

    public int Duration;

    private int remainingDuration=30;

    

    private void Start()
    {
        Begin(Duration);
    }

    private void Begin(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            
            
                uiText.text = $"{remainingDuration}"; // Display time in seconds
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        // Restart the timer from the specified duration
        Begin(Duration);
    }
}
