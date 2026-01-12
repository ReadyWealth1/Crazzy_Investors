using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleAnimation : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public Button openButton; // Reference to the Open Button component
    public Button closeButton; // Reference to the Close Button component

    private bool isOpen = false; // To track the current state
    public GameObject Card_Holder;
    public Canvas canvas; // Reference to the canvas containing this UI
    public float delayTime = 0.6f; // Delay time in seconds
    public int topSortingOrder = 10; // Sorting order value to set when this canvas is on top
    private int originalSortingOrder;

    void Start()
    {
        if (openButton != null)
        {
            openButton.onClick.AddListener(OpenPanel); // Add listener to the open button
        }
        else
        {
            Debug.LogError("Open button component is not assigned.");
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePanel); // Add listener to the close button
        }
        else
        {
            Debug.LogError("Close button component is not assigned.");
        }

        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
        }

        if (canvas == null)
        {
            Debug.LogError("Canvas component is not assigned.");
        }
        else
        {
            originalSortingOrder = canvas.sortingOrder;
        }
    }

    void OpenPanel()
    {
        if (!isOpen && animator != null)
        {
            
            animator.SetTrigger("Open"); // Set the Open trigger
            canvas.sortingOrder = topSortingOrder; // Bring this canvas to the top
            Card_Holder.SetActive(true);
            StartCoroutine(EnableSwipingAfterDelay());
            isOpen = true; // Set the state to open
        }
    }

    void ClosePanel()
    {
        if (isOpen && animator != null)
        {
          
            animator.SetTrigger("Close"); // Set the Close trigger
            StartCoroutine(DeactivateAfterDelay());
            CardSwiper.IsItOpen = false; // Disable swiping
            isOpen = false; // Set the state to closed
        }
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); // Wait for the delay time
        Card_Holder.SetActive(false);
        // Restore original sorting order
        canvas.sortingOrder = originalSortingOrder;
    }

    IEnumerator EnableSwipingAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); // Wait for the delay time to finish the open animation
        CardSwiper.IsItOpen = true; // Enable swiping after the open animation
    }
}
