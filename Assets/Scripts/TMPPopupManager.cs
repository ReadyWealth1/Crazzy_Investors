using System.Collections;
using UnityEngine;
using TMPro;

public class TMPPopupManager : MonoBehaviour
{
    [Header("Popup Settings")]
    [Tooltip("The UI prefab for the popup. Must have a RectTransform and TextMeshProUGUI component.")]
    public GameObject popupPrefab;

    [Header("Animation Settings")]
    [Tooltip("How high the popup floats in pixels.")]
    public float floatHeight = 100f;

    [Tooltip("Total duration of the animation.")]
    public float duration = 1.0f;

    [Tooltip("Delay before the animation starts.")]
    public float startDelay = 0.1f;

    private Canvas _parentCanvas;

    void Awake()
    {
        // Cache the parent canvas
        _parentCanvas = GetComponentInParent<Canvas>();
        if (_parentCanvas == null)
        {
            Debug.LogError($"{name}: No parent Canvas found! Popups may not display correctly.", this);
        }
    }

    /// <summary>
    /// Shows a popup at a given World Position with custom text and color.
    /// </summary>
    public void ShowPopup(string text, Color color, Vector2 canvasPosition)
    {
        GameObject popupInstance = Instantiate(popupPrefab, _parentCanvas.transform);
        RectTransform popupRect = popupInstance.GetComponent<RectTransform>();

        // Directly set anchored position
        popupRect.anchoredPosition = canvasPosition;

        TextMeshProUGUI textMesh = popupInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = text;
            textMesh.color = color;
        }

        CanvasGroup canvasGroup = popupInstance.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = popupInstance.AddComponent<CanvasGroup>();

        StartCoroutine(FloatAndFade(popupRect, canvasGroup));
    }

    /// <summary>
    /// Converts a world position to the correct anchored position on the Canvas.
    /// </summary>
    private void PositionPopupOnCanvas(RectTransform popupRect, Vector3 worldPosition)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogWarning("No Main Camera found for positioning popup.");
            return;
        }

        // 1. Convert the world position to a screen point
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(worldPosition);

        // 2. Convert the screen point to a local position within the Canvas
        RectTransform canvasRect = _parentCanvas.GetComponent<RectTransform>();

        // Handle different Canvas Render Modes
        Camera uiCamera = _parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, screenPoint, uiCamera, out Vector2 localPoint);

        // 3. Set the anchored position
        popupRect.anchoredPosition = localPoint;
    }

    /// <summary>
    /// Handles the floating upward and fading out effect of the popup using RectTransform.
    /// </summary>
    private IEnumerator FloatAndFade(RectTransform popupRect, CanvasGroup canvasGroup)
    {
        // Wait for the initial delay
        yield return new WaitForSeconds(startDelay);

        Vector2 startPos = popupRect.anchoredPosition;
        Vector2 endPos = startPos + Vector2.up * floatHeight;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            // Calculate progress (0 to 1)
            float t = (Time.time - startTime) / duration;

            // Move upwards using anchoredPosition
            popupRect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            // Slightly scale up
            float scale = Mathf.Lerp(1.0f, 1.2f, t);
            popupRect.localScale = new Vector3(scale, scale, scale);

            // Fade out
            canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, t);

            yield return null;
        }

        // Destroy popup after animation
        if (popupRect != null)
            Destroy(popupRect.gameObject);
    }
}