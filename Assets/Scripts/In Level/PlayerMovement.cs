using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Animations.Rigging;

public class PlayerMovement : MonoBehaviour
{
    private PlayerHealth playerHealth;

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

    public Vector3 movementMomentum = new Vector3(0,0,0); // TODO: Change to private
    public float friction = 0.1f;

    // Variable jumping
    public float maxJump = 0.25f;
    private float jumpDuration;

    // Swinging
    [SerializeField]  bool swinging = false;
    private Vector3 pivotPosition;
    public float targetSwingRadius = 2;

    private float rotateMomentum = 0;
    public float rotateDrag = 2;

    private bool exitSwingLock = false;

    private Animator animator;

    // Sound Effects
    private AudioSource audio;
    [SerializeField] private AudioClip landSound;

    //field for player rig
    [SerializeField] private Rig playerRig;
    [SerializeField] public GameObject armTarget;

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
        playerHealth = this.GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        audio = this.GetComponent<AudioSource>();
        standardConstraints = rBody.constraints;
        Physics.IgnoreLayerCollision(0, 7);
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
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }
        else
        {
            //interpolates the rotation to smoothly rotate the player
            Quaternion right = Quaternion.Euler(0, 90, 0);//Quaternion.LookRotation(new Vector3(0,90,0));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, right, speedOfRotation * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }
    }

    public void AddExternalMovementFactor(Vector3 external)
    {
        movementOutputVector += external;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!playerHealth.gameOver)
        {
            if (swinging)
            {
                float rotateForce = m_move.x/1.5f;
                float adj = (pivotPosition.y - transform.position.y);
                float opp = (pivotPosition.x - transform.position.x);
                armTarget.transform.position = pivotPosition;
                playerRig.weight = 1.0f;
                rotateMomentum += opp/2.5f + rotateForce; // Uses opposite to act as a sort of gravity. The further away from the center of the poll, the greater the force to go back to the center is
                rotateForce += rotateMomentum;
                this.transform.RotateAround(pivotPosition, new Vector3(0, 0, 1), rotateMomentum); // Swings player
                rotateMomentum = rotateMomentum / rotateDrag;
                float angle = Mathf.Atan(opp / adj) * 180 / (float)Math.PI; // SOHCAHTOA T^-1(O/A) converted to degrees from radians
                //Debug.Log("Player angle in relation to swing: " + angle);
                this.transform.eulerAngles = new Vector3(angle, 90, 0);
                Physics.SyncTransforms();
                //Debug.Log("New player rotation: " + transform.rotation.eulerAngles);
                Debug.Log("Position of swing" + pivotPosition);
            }
            else
            {
                playerRig.weight = 0.0f;
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
                movementMomentum = new Vector3(Mathf.Lerp(movementMomentum.x, movementOutputVector.x, friction),movementOutputVector.y,movementOutputVector.z);
                controller.Move(movementMomentum); // Moves the player with values calculated above and in Update()
            }


            //Debug.Log(movementVector.x + ", " + movementVector.y);
            Grounded = controller.isGrounded;
            if (!Grounded && !swinging)
                animator.SetBool("jump", true);
            else
                animator.SetBool("jump", false);


            FlipPlayer();
        }
        
    }

    private void Update()
    {
        if(!playerHealth.gameOver)
        {
            if (swinging)
            {
                if (m_move.y <= 0) // When player stops holding jump button
                {
                    Debug.Log("Swinging stopping in update");
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
    }

    private void EnterSwing(Vector3 pivotPos)
    {
        Debug.Log("Entered swing radius");
        rBody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
        controller.enabled = false;
        pivotPosition = pivotPos;
        float scalarDistance = (float)Math.Sqrt(Math.Pow(transform.position.x - pivotPos.x, 2) + Math.Pow(transform.position.y - pivotPos.y, 2));
        //Debug.Log("Distance from pivot: " + scalarDistance + " target swing radius: " + targetSwingRadius);
        Vector3 vec3Distance = transform.position - pivotPos;
        vec3Distance *= targetSwingRadius / scalarDistance;
        transform.position = pivotPos + vec3Distance;
        scalarDistance = (float)Math.Sqrt(Math.Pow(transform.position.x - pivotPos.x, 2) + Math.Pow(transform.position.y - pivotPos.y, 2));
        //Debug.Log("Fixed Distance from pivot: " + scalarDistance + " target swing radius: " + targetSwingRadius);
        swinging = true;
    }

    private void ExitSwing()
    {
        if (exitSwingLock) // Stops function being ran multiple times simultaneously
            return;
        exitSwingLock = true;
        rBody.constraints = standardConstraints;
        this.transform.rotation = Quaternion.Euler(0, 90, 0);
        controller.enabled = true;
        swinging = false;
        float adj = (pivotPosition.y - transform.position.y);
        float opp = (pivotPosition.x - transform.position.x);
        float angle = Mathf.Atan(opp / adj) * 180 / (float)Math.PI;
        
        if (rotateMomentum < 0) // Fixes issue where x vector is calculated in the wrong direction
        {
            movementOutputVector.x = -Math.Abs(Mathf.Cos(angle) * rotateMomentum); // Use SOHCAHTOA here
        }
        else
        {
            movementOutputVector.x = Math.Abs(Mathf.Cos(angle) * rotateMomentum);
        }
        
        movementOutputVector.y = Mathf.Sin(angle) * rotateMomentum;
        if (Math.Abs(movementOutputVector.y) > 0.08f)
            // Limits vertical momentum
            movementOutputVector.y = (float)Math.Pow(movementOutputVector.y,1/3)/2;
        Debug.Log("Jumped off swing: " + movementOutputVector.x + ", " + movementOutputVector.y + " momentum: " + rotateMomentum);
        exitSwingLock = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered collision");

        if (other.gameObject.tag == "Swing" && m_move.y > 0 && other.transform.position.y > this.transform.position.y) // Must get to swing and jump key held. Only if swing is above the player (Can't use contacts with trigger)
        {
            EnterSwing(other.gameObject.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Triggered collision");

        if (other.gameObject.tag == "Swing" && m_move.y > 0 && other.transform.position.y > this.transform.position.y) // Must get to swing and jump key held. Only if swing is above the player (Can't use contacts with trigger)
        {
            EnterSwing(other.gameObject.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject == collision.gameObject)
        {
            Debug.Log("Ignoring collision with self");
            Physics.IgnoreCollision(this.gameObject.GetComponent<BoxCollider>(), collision.collider);
        }
        Debug.Log(this.gameObject.name+ " collided with "+collision.gameObject.name);
        if (!Grounded && !swinging)
        {
            if (collision.gameObject.tag != "Swing")
            {
                jumpDuration = maxJump; // When the player hits something above them, extending the jump doesn't work.
                movementOutputVector.y = -gravity;
                Debug.Log("Head hit by non-swing. Stopping jump.");
            }
            
        }
        else
        {
            if (Grounded)
            {
                // Plays landing sound
                audio.clip = landSound;
                audio.Play();
                Debug.Log("Played landing sound");
            }
            /*Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
            Debug.Log("Collision force: " + collisionForce);
            if (collisionForce.y > 0.5f)
            {*/
            
            //animator.SetBool("jump", false);
            //}

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
