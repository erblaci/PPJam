using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public Transform target;      // Player to follow
    public Transform fpsView;     // Player's head (for FPS mode)
    
    public float thirdPersonDistance = 5.0f;
    public float zoomSpeed = 2.0f;
    public float minDistance = 0.5f;   // When to switch to FPS
    public float maxDistance = 10.0f;
    public float rotationSpeed = 2.0f;

    private float mouseSensitivity = 5f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    
    private float currentX = 0f;
    private float currentY = 15f;
    public bool isFPS = false;  // Tracks if we're in first-person mode
    private Vector3 newdirection;
    
    public LayerMask obstacleLayers;
    
    private float adjustedDistance;   // Stores dynamically adjusted distance
    private float targetDistance;
   void Start()
    {
        adjustedDistance = thirdPersonDistance;
        targetDistance = thirdPersonDistance;
    }

    void Update()
    {
        if (thirdPersonDistance > minDistance)
        {
            newdirection = (target.position - transform.position).normalized;
        }

        

       

        // Zoom control
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetDistance -= scroll * zoomSpeed;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

        // Switch to FPS if zoomed in enough
       

        // Rotate camera (only in third-person mode)
        if (!isFPS && Input.GetMouseButton(1)) // Right-click to rotate
        {
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            currentY = Mathf.Clamp(currentY, 10f, 80f);
        }
        else if (isFPS)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 50f);
            yRotation += mouseX;

          
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(target.position, target.position - transform.position, Color.cyan);
    }

    void LateUpdate()
    {
      

       

        if (isFPS)
        {
            // First-person mode: Attach camera to player's head
            Cursor.lockState = CursorLockMode.Locked;
            transform.position = fpsView.position;
            transform.rotation = fpsView.rotation;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;

            // Third-person mode: Handle camera collision
            Vector3 direction = new Vector3(0, 0, -targetDistance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 desiredPosition = target.position + rotation * direction;

            if (Physics.Raycast(target.position, desiredPosition - target.position, out RaycastHit hit, targetDistance, obstacleLayers))
            {
                // Adjust distance to just before the obstacle
                adjustedDistance = Mathf.Clamp(hit.distance - 0.2f, minDistance, targetDistance);
            }
            else
            {
                // Gradually return to original zoom distance
                adjustedDistance = Mathf.Lerp(adjustedDistance, targetDistance, Time.deltaTime * 5f);
            }

            // Apply position and rotation
            
            transform.position = target.position + rotation * new Vector3(0, 0, -adjustedDistance);
            transform.LookAt(target.position);
        }
    }
}
