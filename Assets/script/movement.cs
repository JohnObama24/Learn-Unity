using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Transform playerCamera;
    public float moveSpeed = 5f;
    public float lookSensitivity = 100f;
    public float groundDistance = 0.4f;
    public CharacterController characterController;
    // private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor in the game window
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;


        transform.Rotate(Vector3.up * mouseX);

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }


}
