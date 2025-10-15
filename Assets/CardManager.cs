using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards;
    private Vector3 offScreenPosition = new Vector3(-Screen.width, 0, 0);
    private float swipeDuration = 0.5f;
    private float rotationAngle = 15f; // Rotation angle when swiped
    private SwipeManager swipeManager;

    void Start()
    {
        swipeManager = FindObjectOfType<SwipeManager>();
        UpdateCardRotations();
    }

    void Update()
    {
        if (SwipeManager.swipeLeft)
        {
            SwipeLeft();
        }
    }

    void SwipeLeft()
    {
        GameObject topCard = cards[cards.Length - 1];

        // Animate top card off screen and rotate
        LeanTween.move(topCard, offScreenPosition, swipeDuration).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.rotateZ(topCard, rotationAngle, swipeDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(OnSwipeComplete);
    }

    void OnSwipeComplete()
    {
        // Move top card back to original position
        GameObject topCard = cards[cards.Length - 1];
        topCard.transform.position = Vector3.zero;
        topCard.transform.rotation = Quaternion.identity;

        // Move top card to bottom of hierarchy
        topCard.transform.SetAsFirstSibling();

        // Update cards array
        UpdateCardArray();

        // Update card rotations
        UpdateCardRotations();
    }

    void UpdateCardArray()
    {
        cards = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            cards[i] = transform.GetChild(i).gameObject;
        }
    }

    void UpdateCardRotations()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            float rotationZ = (cards.Length - 1 - i) * rotationAngle;
            cards[i].transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }
    }
}
