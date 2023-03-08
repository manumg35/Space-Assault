using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Input Keys Settings")]
    [Tooltip("Movement Input Keys")] [SerializeField] InputAction movement;

    [Tooltip("Firing Input Keys")] [SerializeField] InputAction fire;

    [Header("General Setup Settings")]
    [Tooltip("How fast player moves around the screen")] [SerializeField] float offsetThrow = 30f;

    [Tooltip("Change Smoothness for inputs")] [SerializeField] float smoothInputSpeed = .1f;

    [Tooltip("Maximum range for movement on X axis")] [SerializeField] float xRange = 8f;
    [Tooltip("Maximum range for movement on Y axis")] [SerializeField] float yRange = 5.5f;

    [Header("Rotation depending position Settings")]
    [Tooltip("Fator for Pitch depending on position (rota en X)")] 
    [SerializeField] float positionPitchFactor = -1.5f;

    [Tooltip("Fator for Yaw depending on position (rota en Y)")] 
    [SerializeField] float positionYawFactor = -2f;

    [Header("Rotation depending Input Settings")]
    [Tooltip("Fator for Pitch depending on Input (rota en X)")] 
    [SerializeField] float controllPitchFactor = -1.2f;

    [Tooltip("Fator for Yaw depending on Input (rota en Y)")] 
    [SerializeField] float controllRollFactor = -10f;

    [Header("Particle System Settings")]
    [Tooltip("Array to Activate or Deactivate the shots ParticleSystem")] 
    [SerializeField] ParticleSystem[] lasers;


    float xThrow, yThrow;

    Vector2 currentInputVector;
    Vector2 smoothInputVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        smoothController();
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    private void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5f)
        {
            SetLasers(true);
        }
        else
        {
            SetLasers(false);
        }
        
    }



    void SetLasers(bool active)
    {
        foreach (ParticleSystem laser in lasers)
        {
            var emission = laser.emission;
            emission.enabled = active;
        }
    }

    private void smoothController()
    {
        Vector2 throw_ = movement.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, throw_, ref smoothInputVelocity, smoothInputSpeed);

        xThrow = currentInputVector.x;
        yThrow = currentInputVector.y;
    }

    private void ProcessRotation()
    {
        float pitchDuePosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controllPitchFactor;


        float pitch = (pitchDuePosition + pitchDueToControl) *-1;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controllRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {

        float xPos = transform.localPosition.x + offsetThrow * xThrow * Time.deltaTime;
        float yPos = transform.localPosition.y + offsetThrow * yThrow * Time.deltaTime;

        float clampedXPos = Mathf.Clamp(xPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(yPos, -yRange, yRange);

        transform.localPosition = new Vector3
            (clampedXPos,
            clampedYPos,
            transform.localPosition.z);


        
    }
}
