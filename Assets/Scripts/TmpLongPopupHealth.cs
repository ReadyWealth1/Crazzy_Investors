using System.Collections;
using UnityEngine;
using TMPro;

public class TmpLongPopupHealth : MonoBehaviour
{
    public GameObject popupPrefab;

    public void ShowPopup(string text, Color color, Vector3 position)
    {
        GameObject popupInstance = Instantiate(popupPrefab, transform);
        popupInstance.transform.position = position;
        TextMeshProUGUI textMesh = popupInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = text;
            textMesh.color = color;
        }

        CanvasGroup canvasGroup = popupInstance.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = popupInstance.AddComponent<CanvasGroup>();
        }

        StartCoroutine(FloatAndFade(popupInstance, canvasGroup));
    }

    private IEnumerator FloatAndFade(GameObject popupInstance, CanvasGroup canvasGroup)
    {
        // Increase this value to make the pop-up visible longer
        float duration = 5.0f; // Changed from 3.5f to 5.0f
        float elapsed = -0.5f;
        Vector3 startPosition = popupInstance.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * 100.0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            popupInstance.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            float scale = Mathf.Lerp(1.0f, 1.2f, t);
            popupInstance.transform.localScale = new Vector3(scale, scale, scale);
            canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(popupInstance);
    }
}