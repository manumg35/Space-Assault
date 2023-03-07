using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;

    [SerializeField] InputAction fire;

    [SerializeField] float offsetThrow = 30f;

    [SerializeField] float smoothInputSpeed = .1f;

    [SerializeField] float xRange = 8f;
    [SerializeField] float yRange = 5.5f;

    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float positionYawFactor = -2f;

    [SerializeField] float controllPitchFactor = -1.2f;
    [SerializeField] float controllRollFactor = -10f;

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
