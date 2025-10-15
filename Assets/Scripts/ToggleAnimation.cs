using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleAnimation : MonoBehaviour
{
    public Animator animator;
    public Button openButton;
    public Button closeButton;
    private bool isOpen = false;
    public GameObject Card_Holder;
    public Canvas canvas;
    public float delayTime = 0.6f;
    public int topSortingOrder = 10;
    private int originalSortingOrder;

    // Add a small delay before opening to let other panels finish closing
    public float openDelayTime = 0.1f;

    // Store references to running coroutines to stop them if needed
    private Coroutine closeCoroutine;
    private Coroutine openCoroutine;

    void Start()
    {
        if (openButton != null)
        {
            openButton.onClick.AddListener(() => StartCoroutine(OpenPanelWithDelay()));
        }
        else
        {
            Debug.LogError("Open button component is not assigned.");
        }

        if (closeButton != null)
        {
            // Change close button to use delayed closing
            closeButton.onClick.AddListener(() => StartCoroutine(ClosePanelWithDelay()));
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

    // New method with delay before opening
    IEnumerator OpenPanelWithDelay()
    {
        // Small delay to let other panels finish their close operations
        yield return new WaitForSeconds(openDelayTime);
        OpenPanel();
    }

    // New method that waits for card swiping to finish before closing
    IEnumerator ClosePanelWithDelay()
    {
        // Get reference to the CardSwiper component
        CardSwiper cardSwiper = Card_Holder.GetComponentInChildren<CardSwiper>();

        if (cardSwiper != null)
        {
            // Wait while any card is still swiping
            while (cardSwiper.IsSwiping())
            {
                yield return new WaitForSeconds(0.1f); // Check every 0.1 seconds
            }
        }

        // Now it's safe to close - no cards are swiping
        ClosePanel();
    }

    void OpenPanel()
    {
        if (animator == null) return;

        // Stop any running close coroutine to prevent conflicts
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
            closeCoroutine = null;
        }

        // If already open, don't do anything
        if (isOpen) return;

        animator.SetTrigger("Open");
        canvas.sortingOrder = topSortingOrder; // Set sorting order immediately
        Card_Holder.SetActive(true);

        // Start the open coroutine
        if (openCoroutine != null)
        {
            StopCoroutine(openCoroutine);
        }
        openCoroutine = StartCoroutine(EnableSwipingAfterDelay());

        isOpen = true;
    }

    void ClosePanel()
    {
        if (animator == null) return;

        // Stop any running open coroutine to prevent conflicts
        if (openCoroutine != null)
        {
            StopCoroutine(openCoroutine);
            openCoroutine = null;
        }

        // If already closed, don't do anything
        if (!isOpen) return;

        animator.SetTrigger("Close");
        CardSwiper.IsItOpen = false; // Disable swiping immediately

        // Start the close coroutine
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }
        closeCoroutine = StartCoroutine(DeactivateAfterDelay());

        isOpen = false;
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        // Only execute if we're still supposed to be closing (not interrupted)
        if (!isOpen)
        {
            Card_Holder.SetActive(false);
            canvas.sortingOrder = originalSortingOrder;
        }

        closeCoroutine = null; // Clear the reference
    }

    IEnumerator EnableSwipingAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        // Only enable swiping if we're still supposed to be open (not interrupted)
        if (isOpen)
        {
            CardSwiper.IsItOpen = true;
        }

        openCoroutine = null; // Clear the reference
    }
}