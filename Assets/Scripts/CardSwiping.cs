using UnityEngine;

public class CardSwiper : MonoBehaviour
{
    public float swipeDistance = 200f;
    public float swipeTime = 0.5f;
    private Transform cardContainer;
    public  bool isSwiping = false;
    public static bool IsItOpen;

    void Start()
    {
        cardContainer = transform; // Assuming this script is attached to the parent of the card objects
        UpdateCardRotations();
    }

    void Update()
    {
        if (!isSwiping && IsItOpen == true) // Fixed the assignment bug from your original
        {
            if (SwipeManager.swipeLeft)
            {
                Debug.Log("Left Swipe Detected");
                SwipeCard(-swipeDistance, true);
            }
            else if (SwipeManager.swipeRight)
            {
                Debug.Log("Right Swipe Detected");
                SwipeCard(swipeDistance, false);
            }
        }
    }

    // Public method for ToggleAnimation to check if swiping is happening
    public bool IsSwiping()
    {
        return isSwiping;
    }

    void SwipeCard(float distance, bool isLeftSwipe)
    {
        if (cardContainer.childCount == 0)
        {
            Debug.LogWarning("No cards found in the container.");
            return;
        }

        isSwiping = true;

        Transform topCard = isLeftSwipe ? cardContainer.GetChild(cardContainer.childCount - 1) : cardContainer.GetChild(0);
        Vector3 originalPos = topCard.position;
        Vector3 swipePos = new Vector3(topCard.position.x + (isLeftSwipe ? distance : -distance), topCard.position.y, topCard.position.z);

        // Cancel the ongoing tween if there is any
        LeanTween.cancel(topCard.gameObject);

        // Start the swipe tween
        LeanTween.moveX(topCard.gameObject, swipePos.x, swipeTime).setOnComplete(() =>
        {
            if (isLeftSwipe)
            {
                UpdateHierarchyAndMoveBackLeftSwipe(topCard, originalPos);
            }
            else
            {
                UpdateHierarchyAndMoveBackRightSwipe(topCard, originalPos);
            }
        });
    }

    void UpdateHierarchyAndMoveBackLeftSwipe(Transform topCard, Vector3 originalPos)
    {
        // Update the hierarchy for left swipe
        topCard.SetAsFirstSibling();
        Debug.Log("Card Swiped Left and Hierarchy Updated");

        // Update card rotations
        UpdateCardRotations();

        // Move the card back to its original position
        LeanTween.moveX(topCard.gameObject, originalPos.x, swipeTime).setOnComplete(() =>
        {
            isSwiping = false;
        });
    }

    void UpdateHierarchyAndMoveBackRightSwipe(Transform topCard, Vector3 originalPos)
    {
        // Update the hierarchy for right swipe
        topCard.SetAsLastSibling();
        Debug.Log("Card Swiped Right and Hierarchy Updated");

        // Update card rotations
        UpdateCardRotations();

        // Move the card back to its original position using the reversed animation
        LeanTween.moveX(topCard.gameObject, originalPos.x, swipeTime).setOnComplete(() =>
        {
            isSwiping = false;
        });
    }

    void UpdateCardRotations()
    {
        int childCount = cardContainer.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform card = cardContainer.GetChild(i);
            if (i < 5)
            {
                float rotationZ = -30 + i * 5;
                card.rotation = Quaternion.Euler(0, 0, rotationZ);
            }
            else
            {
                card.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}