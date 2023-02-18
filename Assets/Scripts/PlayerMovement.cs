using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    public float jumpSpeed = 0.2f;
    public float gravity = 1;

    CharacterController controller;
    Vector3 movementVector;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hMovement = Input.GetAxis("Horizontal");
        movementVector.x = hMovement * speed;

        movementVector.y += -gravity;

        controller.Move(movementVector);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            movementVector.y = jumpSpeed;
            controller.Move(movementVector);
            Debug.Log("Jump!");
        }
    }
}
