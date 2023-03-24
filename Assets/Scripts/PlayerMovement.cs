using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    public float jumpSpeed = 0.2f;
    public float gravity = 1;

    CharacterController controller;
    Vector3 movementOutputVector;
    [SerializeField] private float speedOfRotation;
    bool ifDirectionChanged = false;

    public PlayerControls playerControls;
    private InputAction movementInputVector;

    private Rigidbody rBody;

    [SerializeField] private bool Grounded;

    // Variable jumping
    public float maxJump = 0.25f;
    private float jumpDuration;

    // Swinging
    bool swinging = false;
    private Vector3 pivotPosition;

    private Animator animator;


    private void OnEnable()
    {
        playerControls.Enable();
        movementInputVector = playerControls.Player.Move;
        movementInputVector.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        movementInputVector.Disable();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();
        controller = GetComponent<CharacterController>();
        rBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (swinging)
        {
            float rotateForce = movementInputVector.ReadValue<Vector2>().x; // TODO: move this update to update
            if (rotateForce == 0)
            {
                rotateForce = this.transform.rotation.eulerAngles.x/20;
            }
            this.transform.RotateAround(pivotPosition, Vector3.forward, rotateForce);
            //this.transform.Rotate(new Vector3(0, 0, move.ReadValue<Vector2>().x));
        }
        else
        {
            if (Grounded)
            {
                movementOutputVector.y = -0.02f; // Keeps player stuck to ground
            }
            else
            {
                if (movementInputVector.ReadValue<Vector2>().y > 0 && jumpDuration < maxJump) // Times jump button for higher/lower jumps
                {
                    jumpDuration += Time.fixedDeltaTime;
                }
                else
                    movementOutputVector.y += -gravity;
            }
            controller.Move(movementOutputVector); // Moves the player with values calculated above and in Update()

        }

        
        //Debug.Log(movementVector.x + ", " + movementVector.y);
        Grounded = controller.isGrounded;

        rotatePlayer();
    }

    public void rotatePlayer()
    {
        if(ifDirectionChanged)
        {
            //interpolates the rotation to smoothly rotate the player
            Quaternion left = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, left, speedOfRotation * Time.deltaTime);
        }
        else
        {
            //interpolates the rotation to smoothly rotate the player
            Quaternion right = Quaternion.Euler(0, 90, 0);//Quaternion.LookRotation(new Vector3(0,90,0));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, right, speedOfRotation * Time.deltaTime);
        }
    }

    private void Update()
    {
        Vector2 xy = movementInputVector.ReadValue<Vector2>();
        if (!swinging)
        {
            movementOutputVector.x = xy.x * speed;
        }
        else
        {
            if (xy.y > 0) // When player jumps of swing
            {
                movementOutputVector.x = speed; // Use SOHCAHTOA here
                movementOutputVector.y = jumpSpeed;
                swinging = false;
                Debug.Log("Jumped off swing: " + movementOutputVector.x + ", " + movementOutputVector.y);
                ExitSwing();
            }            
        }
        if (xy.y>0)
        {
            if (Grounded)
            {
                // TODO: Check the player isn't hitting something above them to do the next part
                movementOutputVector.y = jumpSpeed;
                jumpDuration = 0;
                //Debug.Log("Jump stopped. Movement is: "+movementOutputVector.x + ", " + movementOutputVector.y);
                Grounded = false;
            }            

        }
        if (movementOutputVector.x > 0)
        {
            ifDirectionChanged = false;
            animator.SetBool("running", true);
            Debug.Log("move right");
        }
        else if (movementOutputVector.x < 0)
        {
            ifDirectionChanged = true;
            animator.SetBool("running", true);
            Debug.Log("move left");
        }
        else
        {
            animator.SetBool("running", false);
        }

    }

    private void ExitSwing()
    {
        this.transform.rotation = Quaternion.Euler(0, 90, 0);
        swinging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Swing")
        {
            Debug.Log("Entered swing radius");

            if (other.transform.position.y > this.transform.position.y) // Only if swing is above the player (Can't use contacts with trigger)
            {
                this.transform.LookAt(other.gameObject.transform);
                this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, 90, 0);
                pivotPosition = other.gameObject.transform.position;
                swinging = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Grounded)
        {
            jumpDuration = maxJump; // When the player hits something above them, extending the jump doesn't work.
            movementOutputVector.y = -gravity;
            Debug.Log("Head hit by non-swing. Stopping jump.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Swing")
        {
            Debug.Log("Exiting swing radius");
            ExitSwing();
        }
    }
}
