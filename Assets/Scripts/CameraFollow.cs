using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera follows
    public float heightOffset = 2f; // Desired height from the ground
    public static float distanceBehind = 5f; // Distance behind the target
    public static float followSpeed = 5f; // Speed at which the camera follows
    public LayerMask groundLayer; // LayerMask for the ground
    private Vector3 targetPosition;

    void Start()
    {
        // Immediately set the camera to the correct position behind the target at the start
        if (target != null)
        {
            // Determine the desired position behind the target
            targetPosition = target.position - target.forward * distanceBehind;

            // Maintain the current camera height initially
            targetPosition.y = transform.position.y;

            // Immediately set the camera position behind the target
            transform.position = targetPosition;

            // Ensure the camera is looking at the target
            transform.LookAt(target);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Determine the desired position behind the target based on the forward direction and distance
        targetPosition = target.position - target.forward * distanceBehind;

        // Maintain the current camera height initially
        targetPosition.y = transform.position.y;

        // Cast a ray downwards from the camera's future position
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Set the camera's height to maintain the desired height above the ground
            targetPosition.y = hit.point.y + heightOffset;
        }

        // Directly set the camera position behind the target
        transform.position = targetPosition;

        // Ensure the camera is looking at the target
       // transform.LookAt(target);
    }

    // Coroutine to gradually change the distanceBehind value
    public static IEnumerator LerpDistanceBehind(float targetDistance, float duration)
    {
        float startDistance = distanceBehind;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            distanceBehind = Mathf.Lerp(startDistance, targetDistance, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        distanceBehind = targetDistance; // Ensure the target value is set at the end
    }
}
