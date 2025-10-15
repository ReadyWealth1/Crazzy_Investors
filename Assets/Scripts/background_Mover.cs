using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_Mover : MonoBehaviour
{
    // Start is called before the first frame update
    private float x;
    private float y;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, Character.FwdSpeed * Time.deltaTime);
    }
}
/*using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera follows
    public float heightOffset = 5f; // Desired height from the ground
    public static float distanceBehind = 5.62f; // Distance behind the target
    public static float followSpeed = 5f; // Speed at which the camera follows
    public LayerMask groundLayer; // LayerMask for the ground
    private Vector3 targetPosition;

    void Start()
    {
        // Immediately set the camera to the correct position behind the target at the start
        if (target != null)
        {
            // Determine the desired position behind the target
            targetPosition = target.position + target.forward * distanceBehind;

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

        // Determine the desired position behind the target based on the inverted forward direction and distance
        targetPosition = target.position + target.forward * distanceBehind;

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
        transform.LookAt(target);
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
}*/