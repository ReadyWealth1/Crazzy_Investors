using UnityEngine;

public class CharacterBridgeDetection : MonoBehaviour
{
    private CameraFollow cameraFollowScript;

    void Start()
    {
        // Assuming the camera has the CameraFollow script attached
        cameraFollowScript = Camera.main.GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bridge"))
        {
            //cameraFollowScript.SetFollowVertical(true);
            Debug.Log("Entered Bridge Area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bridge"))
        {
            //cameraFollowScript.SetFollowVertical(false);
            Debug.Log("Exited Bridge Area");
        }
    }
}
