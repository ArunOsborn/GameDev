using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    public float jumpSpeed = 0.2f;
    public float gravity = 1;

    CharacterController controller;
    Vector3 movementVector;

    public PlayerControls playerControls;
    private InputAction move;

    private void OnEnable()
    {
        playerControls.Enable();
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        move.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementVector.y += -gravity;

        controller.Move(movementVector);
    }

    private void Update()
    {
        Vector2 xy = move.ReadValue<Vector2>();
        movementVector.x = xy.x * speed;
        if (xy.y>0 && controller.isGrounded)
        {
            movementVector.y = jumpSpeed;
            controller.Move(movementVector);
            Debug.Log("Jump!");
        }
    }
}
