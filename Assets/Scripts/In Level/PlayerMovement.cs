using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

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
    private RigidbodyConstraints standardConstraints;

    [SerializeField] private bool Grounded;
    private Vector2 m_move;

    // Variable jumping
    public float maxJump = 0.25f;
    private float jumpDuration;

    // Swinging
    [SerializeField]  bool swinging = false;
    private Vector3 pivotPosition;

    private Animator animator;

    // Sound Effects
    private AudioSource audio;
    [SerializeField] private AudioClip landSound;

    //throw.cs
    private ThrowProjectile projectileThrow;

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
        projectileThrow = GetComponent<ThrowProjectile>();
    }

    private void Start()
    {
        audio = this.GetComponent<AudioSource>();
        standardConstraints = rBody.constraints;

    }

    public void SetMovement(InputAction.CallbackContext context)
    {
        m_move = context.ReadValue<Vector2>();
    }

    public void FlipPlayer()
    {
        if (ifDirectionChanged)
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (swinging)
        {
            float rotateForce = m_move.x; // TODO: move this update to update
            if (rotateForce == 0)
            {
                //rotateForce = 20/this.transform.rotation.eulerAngles.x;
            }
            //this.transform.RotateAround(pivotPosition, new Vector3(0,0,1), rotateForce);
            float adj = (pivotPosition.y - transform.position.y);
            float opp = (pivotPosition.x - transform.position.x);
            this.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Atan(opp/adj)*180/(float)Math.PI, 90, 0));
            Physics.SyncTransforms();
            //this.transform.Rotate(new Vector3(0, 0, m_move.x));
        }
        else
        {
            if (Grounded)
            {
                movementOutputVector.y = -0.02f; // Keeps player stuck to ground
            }
            else
            {
                if (m_move.y > 0 && jumpDuration < maxJump) // Times jump button for higher/lower jumps
                {
                    jumpDuration += Time.fixedDeltaTime;
                }
                else
                    movementOutputVector.y += -gravity;
            }
            controller.Move(movementOutputVector); // Moves the player with values calculated above and in Update()

            Debug.Log(projectileThrow.m_fire);

            if(projectileThrow.m_fire)
            {
                Debug.Log("setting to true");
                Debug.Log(animator.GetLayerWeight(animator.GetLayerIndex("throwing")));

                if (animator.GetLayerWeight(animator.GetLayerIndex("throwing")) <= 0.0f)
                {
                    animator.SetLayerWeight(animator.GetLayerIndex("throwing"), 1.0f);
                    animator.SetBool("m_fire", true);
                }
                else
                {
                    animator.SetLayerWeight(animator.GetLayerIndex("throwing"), projectileThrow.cooldown);
                    animator.SetBool("m_fire", true);
                }
            }
            else
            {
                animator.SetLayerWeight(animator.GetLayerIndex("throwing"), projectileThrow.cooldown);


                Debug.Log("setting to false");
                animator.SetBool("m_fire", false);
            }
        }


        //Debug.Log(movementVector.x + ", " + movementVector.y);
        Grounded = controller.isGrounded;
        if (!Grounded && !swinging)
            animator.SetBool("jump", true);
        else
            animator.SetBool("jump", false);


        FlipPlayer();
    }

    private void Update()
    {
        if (swinging)
        {
            if (m_move.y <= 0) // When player stops holding jump button
            {
                Debug.Log("Swinging stopping in update");
                movementOutputVector.x = Mathf.Cos(transform.rotation.eulerAngles.z) * speed; // Use SOHCAHTOA here
                movementOutputVector.y = Mathf.Sin(transform.rotation.eulerAngles.z) * speed;
                swinging = false;
                Debug.Log("Jumped off swing: " + movementOutputVector.x + ", " + movementOutputVector.y);
                ExitSwing();
            }
        }
        else
        {
            movementOutputVector.x = m_move.x * speed;
        }
        if (m_move.y>0)
        {
            //Debug.Log("Attempted jump. Grounded=" + Grounded);
            if (Grounded)
            {
                // TODO: Check the player isn't hitting something above them to do the next part
                movementOutputVector.y = jumpSpeed;
                jumpDuration = 0;
                //Debug.Log("Jump stopped. Movement is: "+movementOutputVector.x + ", " + movementOutputVector.y);
                //animator.SetBool("jump", true);
                Grounded = false;
            }            

        }
        float test = Mathf.Clamp01(movementOutputVector.magnitude);

        if (movementOutputVector.x > 0)
        {
            ifDirectionChanged = false;
            animator.SetBool("running", true);
            animator.SetFloat("Blend", 1.0f, 0.05f, Time.deltaTime);
            //Debug.Log("move right");
        }
        else if (movementOutputVector.x < 0)
        {
            ifDirectionChanged = true;
            animator.SetBool("running", true);
            animator.SetFloat("Blend", 1.0f, 0.05f, Time.deltaTime);
            //Debug.Log("move left");
        }
        else
        {
            animator.SetBool("running", false);
            animator.SetFloat("Blend", test, 0.05f, Time.deltaTime);
        }

    }

    private void ExitSwing()
    {
        rBody.constraints = standardConstraints;
        this.transform.rotation = Quaternion.Euler(0, 90, 0);
        controller.enabled = true;
        swinging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered collision");

        if (other.gameObject.tag == "Swing" && m_move.y > 0 && other.transform.position.y > this.transform.position.y) // Must get to swing and jump key held. Only if swing is above the player (Can't use contacts with trigger)
        {
            Debug.Log("Entered swing radius");
            rBody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
            controller.enabled = false;
            //this.transform.LookAt(other.gameObject.transform);
            //this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, 90, 0);
            pivotPosition = other.gameObject.transform.position;
            swinging = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (!Grounded && !swinging)
        {
            jumpDuration = maxJump; // When the player hits something above them, extending the jump doesn't work.
            movementOutputVector.y = -gravity;
            Debug.Log("Head hit by non-swing. Stopping jump.");
        }
        else
        {
            // Plays landing sound
            audio.clip = landSound;
            audio.Play();
            Debug.Log("Played landing sound");
            //animator.SetBool("jump", false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Swing")
        {
            Debug.Log("Exiting swing radius");
            //ExitSwing();
        }
    }
}
