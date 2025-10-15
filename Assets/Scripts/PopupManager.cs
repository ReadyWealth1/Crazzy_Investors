/*using UnityEngine;

public class TMPPopupManager : MonoBehaviour
{
    public ObjectPooler objectPooler;

    public void CreateTextPopup(Vector3 worldPosition, string text, Vector3 customOffset, Color color)
    {
        // Convert world position to screen position
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // Convert screen position to canvas position
        RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, null, out canvasPosition);

        // Get a pooled object and set its position and text
        GameObject popup = objectPooler.GetPooledObject();
        popup.SetActive(true);
        popup.transform.SetParent(canvasRect, false);
        popup.GetComponent<RectTransform>().anchoredPosition = canvasPosition + (Vector2)customOffset;

        TMPTextPopup tmpTextPopup = popup.GetComponent<TMPTextPopup>();
        tmpTextPopup.SetText(text, this, color); // Pass the color parameter
    }
}
*/