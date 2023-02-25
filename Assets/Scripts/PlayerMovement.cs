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

    private Rigidbody rBody;

    // Variable jumping
    public float maxJump = 0.25f;
    private float jumpDuration;

    // Swinging
    bool swinging = false;
    [SerializeField] private float rotationHeight;

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
        rBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!controller.isGrounded && !swinging)
        {
            movementVector.y += -gravity;
        }
        else
        {
            movementVector.y = 0;
        }
        //Debug.Log(movementVector.x + ", " + movementVector.y);
        controller.Move(movementVector);
    }

    private void Update()
    {
        Vector2 xy = move.ReadValue<Vector2>();
        if (!swinging)
        {
            movementVector.x = xy.x * speed;
        }
        else
        {
            if (xy.y > 0)
            {
                movementVector.x = speed;// Use SOHCAHTOA here
                movementVector.y = jumpSpeed;
            }
            this.transform.Rotate(new Vector3(0, 0, move.ReadValue<Vector2>().x));
        }
        if (xy.y>0)
        {
            if (controller.isGrounded)
            {
                // TODO: Check the player isn't hitting something above them to do the next part
                movementVector.y = jumpSpeed;
                //controller.Move(movementVector);
                Debug.Log("Jump!");
                while ((move.ReadValue<Vector2>().y > 0.9f) && jumpDuration < maxJump)
                {
                    jumpDuration += Time.deltaTime;
                    //Debug.Log(Time.deltaTime);
                    movementVector.y = jumpSpeed;
                    //Debug.Log("Still jumping with: " + jumpDuration +"s left and force of "+movementVector.y);
                }
                jumpDuration = 0;
                Debug.Log("Jump stopped");
            }            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Swing")
        {
            Debug.Log("Entered swing radius");
            //rBody.freezeRotation = false;
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
            swinging = false;
        }
    }
}
