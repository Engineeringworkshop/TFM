using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoysticCombo : DynamicComboController
{
    [SerializeField] private Transform joystickGameObject;
    [SerializeField] private float speedGain;

    [SerializeField] private DynamicComboManager dynamicComboManager;

    //
    private Vector3 defaultPosition;
    private Vector2 movement;

    private void Awake()
    {
        defaultPosition = joystickGameObject.position;
    }

    private void Update()
    {
        CalculateMovement();
    }

    // Controls
    public void Move(InputAction.CallbackContext value)
    {
        var moveVal = value.ReadValue<Vector2>();
        //Debug.Log("Gamepad Right Stick: " + value.ReadValue<Vector2>());

        movement = value.ReadValue<Vector2>();
    }

    public void ResetPosition()
    {
        joystickGameObject.position = defaultPosition;
    }

    private void CalculateMovement()
    {
        Vector2 currentMovement = new Vector3(joystickGameObject.position.x + speedGain * movement.x, joystickGameObject.position.y + speedGain * movement.y,
            joystickGameObject.position.z);

        Vector2 joystickPosition = new Vector2(joystickGameObject.position.x, joystickGameObject.position.y);
        Vector2 originPosition = new Vector2(dynamicComboManager.transform.position.x, dynamicComboManager.transform.position.y);

        Vector2 joystickVector = joystickPosition-originPosition;
        //Debug.DrawLine(originPosition, joystickPosition, UnityEngine.Color.red);
        
        Vector2 movementVector = currentMovement - originPosition;
        //Debug.DrawLine(originPosition, joystickPosition, UnityEngine.Color.green);

        // If movement vector is bigger than the reference distance, decrese it to reference radius
        if (movementVector.sqrMagnitude > Mathf.Pow(dynamicComboManager.ReferenceRadius, 2))
        {
            currentMovement = movementVector.normalized * dynamicComboManager.ReferenceRadius + originPosition;

            //Debug.Log("Currrent Joy: " + joystickGameObject.position + " New joy: " + currentMovement);
        }

        joystickGameObject.position = currentMovement;

        // Check if is close to the target and reset position if reaced
        if (dynamicComboManager.CheckDistance(joystickGameObject.transform.position))
        {
            ResetPosition();
        }
    }
}
