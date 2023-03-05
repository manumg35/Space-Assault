using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;

    [SerializeField] InputAction fire;

    [SerializeField] float offsetThrow = 30f;

    [SerializeField] float xRange = 8f;
    [SerializeField] float yRange = 5.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        float xThrow = movement.ReadValue<Vector2>().x;
        float yThrow = movement.ReadValue<Vector2>().y;


        float xPos= transform.localPosition.x + offsetThrow * xThrow * Time.deltaTime;
        float yPos = transform.localPosition.y + offsetThrow * yThrow * Time.deltaTime;

        float clampedXPos = Mathf.Clamp(xPos,-xRange,xRange);
        float clampedYPos = Mathf.Clamp(yPos, -yRange, yRange); 

        transform.localPosition = new Vector3
            (clampedXPos,
            clampedYPos,
            transform.localPosition.z);


    }
}
