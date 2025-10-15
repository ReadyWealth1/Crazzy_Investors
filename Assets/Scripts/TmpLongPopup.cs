using System.Collections;
using UnityEngine;
using TMPro;

public class TmpLongPopup : MonoBehaviour
{
    [Header("Popup Settings")]
    public GameObject popupPrefab;

    [Header("Animation Settings")]
    public float floatHeight = 100f;
    public float duration = 5f;      // Long duration
    public float startDelay = 0.1f;

    private Canvas _parentCanvas;

    void Awake()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
        if (_parentCanvas == null)
        {
            Debug.LogError($"{name}: No parent Canvas found! Popups may not display correctly.", this);
        }
    }

    /// <summary>
    /// Shows a popup at a canvas position with text, color, and float/fade animation.
    /// </summary>
    public void ShowPopup(string text, Color color, Vector2 canvasPosition)
    {
        GameObject popupInstance = Instantiate(popupPrefab, _parentCanvas.transform);
        RectTransform popupRect = popupInstance.GetComponent<RectTransform>();

        // Set position on Canvas
        popupRect.anchoredPosition = canvasPosition;

        // Set text and color
        TextMeshProUGUI textMesh = popupInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = text;
            textMesh.color = color;
        }

        // Ensure CanvasGroup exists for fading
        CanvasGroup canvasGroup = popupInstance.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = popupInstance.AddComponent<CanvasGroup>();

        StartCoroutine(FloatAndFade(popupRect, canvasGroup));
    }

    private IEnumerator FloatAndFade(RectTransform popupRect, CanvasGroup canvasGroup)
    {
        // Wait before starting
        yield return new WaitForSeconds(startDelay);

        Vector2 startPos = popupRect.anchoredPosition;
        Vector2 endPos = startPos + Vector2.up * floatHeight;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;

            // Move upward
            popupRect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            // Scale up slightly
            float scale = Mathf.Lerp(1.0f, 1.2f, t);
            popupRect.localScale = new Vector3(scale, scale, scale);

            // Fade out
            canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, t);

            yield return null;
        }

        Destroy(popupRect.gameObject);
    }
}
