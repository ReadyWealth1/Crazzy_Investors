using UnityEngine;

public class PhysicalHitHandler : MonoBehaviour
{
    private Animator m_Animator;
    private Character characterScript;
    private Collider physicalObjectCollider; // Store the physical object's collider

    // Lane switching duration and simulation of lane change
    public float laneSwitchDuration = 0.5f;
    //  private bool isExitingObject = false; // To track if the player is exiting the object

    void Start()
    {
        // Get the Animator and Character components
        m_Animator = GetComponent<Animator>();
        characterScript = GetComponent<Character>();

        if (m_Animator == null || characterScript == null)
        {
            Debug.LogError("Missing components!");
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PhysicalToRight") && !Character.isFlying)
        {

            characterScript.OnTriggerEnterPhysicalRight(other);
            characterScript.HandlePhysicalHit(other);

        }
        else if (other.CompareTag("PhysicalToLeft") && !Character.isFlying)
        {
            characterScript.OnTriggerEnterPhysicalLeft(other);
            characterScript.HandlePhysicalHit(other);
        }
        else if (other.CompareTag("PBRE") && !Character.isFlying)
        {
            characterScript.OnTriggerStay(other);
            characterScript.HandlePhysicalHit(other);
        }
        else if (other.CompareTag("PBLE") && !Character.isFlying)
        {
            characterScript.OnTriggerStay(other);
            characterScript.HandlePhysicalHit(other);
        }
    }

    /*  void OnTriggerExit(Collider other)
      {
          if (other.CompareTag("PhysicaHitl") && isExitingObject)
          {
              // Re-enable the collider once the player has exited the object
              physicalObjectCollider.enabled = true;
              isExitingObject = false; // Reset the flag after exiting
              Debug.Log("Exited object, collider re-enabled.");
          }
      }*/

    private void PlayAnimation(string animationName)
    {
        // Play the specified animation on the Animator
        m_Animator.Play(animationName);
        Debug.Log("Playing animation: " + animationName);
    }
}
