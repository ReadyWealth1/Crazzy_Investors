using UnityEngine;
using System.Collections;
public class CharacterAnimationController : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        // Start with idle animation
        animator.Play("Idle");
        StartCoroutine(AnimationSequence());
    }

    private IEnumerator AnimationSequence()
    {
        // Wait for 6 seconds
        yield return new WaitForSeconds(6);
        // Play stretching animation
        animator.Play("Stretching");

        // Wait for 5 seconds
        yield return new WaitForSeconds(5);
        // Play backflip animation
        animator.Play("Backflip");

        // Wait for the duration of the backflip animation (assuming it's 1 second here, adjust if needed)
        yield return new WaitForSeconds(1);
        // Return to idle animation
        animator.Play("Idle");
    }
}
