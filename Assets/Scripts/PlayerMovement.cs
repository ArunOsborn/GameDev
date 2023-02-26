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
    Vector3 movementOutputVector;

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
        playerControls = new PlayerControls();
        controller = GetComponent<CharacterController>();
        rBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Grounded)
        {
            movementOutputVector.y = -0.02f; // Keeps player stuck to ground
        }
        else if (swinging)
        {
            this.transform.RotateAround(pivotPosition, Vector3.forward, movementInputVector.ReadValue<Vector2>().x);
            //this.transform.Rotate(new Vector3(0, 0, move.ReadValue<Vector2>().x));
        }
        else
        {
            if (movementInputVector.ReadValue<Vector2>().y > 0 && jumpDuration<maxJump) // Times jump button for higher/lower jumps
            {
                jumpDuration += Time.fixedDeltaTime;
            }
            else
                movementOutputVector.y += -gravity;
        }
        //Debug.Log(movementVector.x + ", " + movementVector.y);
        controller.Move(movementOutputVector); // Moves the player with values calculated above and in Update()
        Grounded = controller.isGrounded;
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
                movementOutputVector.x = speed*10; // Use SOHCAHTOA here
                movementOutputVector.y = jumpSpeed*10;
                swinging = false;
                Debug.Log("Jumped off swing: " + movementOutputVector.x + ", " + movementOutputVector.y);
            }            
        }
        if (xy.y>0)
        {
            if (Grounded)
            {
                // TODO: Check the player isn't hitting something above them to do the next part
                movementOutputVector.y = jumpSpeed;
                //controller.Move(movementVector);
                Debug.Log("Jump!");
                movementOutputVector.y = jumpSpeed;
                jumpDuration = 0;
                Debug.Log("Jump stopped. Movement is: "+movementOutputVector.x + ", " + movementOutputVector.y);
                Grounded = false;
            }            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Swing")
        {
            Debug.Log("Entered swing radius");
            //rBody.freezeRotation = false;
            pivotPosition = collision.gameObject.transform.position;
            controller.enabled = false;
            swinging = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Swing")
        {
            Debug.Log("Exiting swing radius");
            //this.GetComponent<Rigidbody>().freezeRotation = true;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            controller.enabled = true;
            swinging = false;
        }
    }
}
