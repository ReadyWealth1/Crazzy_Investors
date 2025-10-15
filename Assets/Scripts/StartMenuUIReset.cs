using UnityEngine;

public class StartMenuUIReset : MonoBehaviour
{
    private Animator animator;

    // Cache the Animator reference in Awake
    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No Animator component found on this GameObject.");
        }
    }

    // OnEnable is called when the GameObject becomes active
    void OnEnable()
    {
        if (animator != null)
        {
            animator.Rebind();      // Resets the animator to its default state
            animator.Update(0f);    // Forces an immediate update to apply the changes
        }
    }
}
