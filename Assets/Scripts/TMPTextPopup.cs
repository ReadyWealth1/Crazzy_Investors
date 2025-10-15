/*using UnityEngine;
using TMPro;

public class TMPTextPopup : MonoBehaviour
{
    public TMP_Text popupText;
    public float duration = 2f;
    public Vector3 offset = new Vector3(0, 2, 0);

    private float timer;
    private TMPPopupManager popupManager;

    void Start()
    {
        if (popupText == null)
        {
            popupText = GetComponentInChildren<TMP_Text>();
        }
        timer = duration;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // Return the popup to the pool instead of destroying it
                popupManager.objectPooler.ReturnPooledObject(gameObject);
            }

            // Optional: Update the position to create a floating effect
            transform.position += new Vector3(0, Time.deltaTime, 0);
        }
    }

    public void SetText(string text, TMPPopupManager manager, Color color)
    {
        if (popupText != null)
        {
            popupText.text = text;
            popupText.color = color; // Set the text color
        }
        timer = duration; // Reset the timer when text is set
        popupManager = manager; // Set the popup manager
    }
}
*/