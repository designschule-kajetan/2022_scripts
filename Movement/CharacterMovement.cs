using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    #region  examples
    
    private bool b = true;

    private int i = 10;

    private float f = 5.87256574f;

    private Vector2 v2 = new Vector2(4f, -9f);

    private string s = "dhfhuioeoknnbgcj mch";
    private char character = 'u';

    #endregion
    
    //simple counter
    private int updateCount = 0;

    private Rigidbody rb;
    private Vector2 inputVector;
    private float movingSpeed;
    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float sneakSpeed = 3f;
    [SerializeField] private float sprintSpeed = 3f;
    
    [SerializeField] private float jumpForce = 5f;
    
    //things for a ground check
    [SerializeField] private Transform transformRayStart;
    [SerializeField] private float rayLength = 0.5f;
    [SerializeField] private LayerMask layerGroundCheck;
    
    //things for a slopecheck
    [SerializeField] private float maxAngleSlope = 30f;
    
    //things for camera control
    [SerializeField] private Transform transformCameraFollow;
    [SerializeField] private float rotationSensivity = 1f;
    private float cameraPitch;
    private float cameraRoll;
    [SerializeField] private float maxCameraPitch = 80f;
    [SerializeField] private bool invertCameraPitch = false;
    
    [SerializeField] private Transform transformVisualCharacter;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        movingSpeed = defaultSpeed;
        
        Debug.Log("Start");
    }

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        if (invertCameraPitch)
        {
            cameraPitch = cameraPitch - mouseDelta.y * rotationSensivity;
        }
        else
        {
            cameraPitch = cameraPitch + mouseDelta.y * rotationSensivity;
        }
        
        cameraPitch = Mathf.Clamp(cameraPitch, -maxCameraPitch, maxCameraPitch);

        cameraRoll = cameraRoll + mouseDelta.x * rotationSensivity;

        transformCameraFollow.localEulerAngles = new Vector3(cameraPitch, cameraRoll, 0f);
    }

    private void FixedUpdate()
    {
        if (SlopeCheck())
        {
            Vector3 movementDirection =
                new Vector3(inputVector.x * movingSpeed, rb.velocity.y, inputVector.y * movingSpeed);

            movementDirection = Quaternion.AngleAxis(transformCameraFollow.localEulerAngles.y, Vector3.up) *
                                movementDirection;

            rb.velocity = movementDirection;

            if (movementDirection.x != 0 && movementDirection.z != 0)
            {
                Vector3 lookDirection = movementDirection;
                lookDirection.y = 0f;
                transformVisualCharacter.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
        
    }

    void OnJump()
    {
        Debug.Log("JUMP!");
        if (GroundCheck())
        {
            rb.velocity = new Vector3(0f, jumpForce, 0f);
        }
    }

    void OnSneak(InputValue inputValue)
    {
        Debug.Log("Sneak!: " + inputValue.Get<float>());
        float isSneak = inputValue.Get<float>();
        
        if (Mathf.RoundToInt(isSneak) == 1)
        {
            movingSpeed = sneakSpeed;
        }
        else
        {
            movingSpeed = defaultSpeed;
        }
    }
    
    void OnSprint(InputValue inputValue)
    {
        Debug.Log("Sneak!: " + inputValue.Get<float>());
        float isSprint = inputValue.Get<float>();
        
        if (Mathf.RoundToInt(isSprint) == 1)
        {
            movingSpeed = sprintSpeed;
        }
        else
        {
            movingSpeed = defaultSpeed;
        }
    }

    void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
    }

    bool GroundCheck()
    {
        return Physics.Raycast(transformRayStart.position, Vector3.down, rayLength, layerGroundCheck);
    }

    bool SlopeCheck()
    {
        RaycastHit hit;

        Physics.Raycast(transformRayStart.position, Vector3.down, out hit, rayLength, layerGroundCheck);

        if (hit.collider != null)
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle > maxAngleSlope)
            {
                return false;
            }
        }

        return true;
    }
}
