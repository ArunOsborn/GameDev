using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Transactions;
using UnityEngine.SceneManagement;

public class EnemyAIPathfinding : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.2f;

    [Header("Physics")]
    public float speed = 200f;
    public float airSpeed = 10f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    public bool isGrounded;
    Seeker seeker;
    Rigidbody rb;
    private Animator animator;

    public bool collided;
    public bool jumpCollided;
    public bool jumping;

    [SerializeField] private float speedOfRotation;

    // Start is called before the first frame update
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (TargetInDistance())
        {
            animator.SetBool("isRunning", true);
            PathFollow();
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if(path == null)
        {
            return;
        }

        //reached end of path
        if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<CapsuleCollider>().bounds.extents.y);
        //Debug.Log("GROUNDED VARIABLE = " + isGrounded + "\n" + "2nd box trigger: " + jumpCollided);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        //direction calculation
        Vector2 direction = ((Vector3)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                Debug.Log("enemy jump");
                rb.AddForce(Vector3.up * airSpeed, ForceMode.VelocityChange);
            }
        }
        else
        {
            rb.AddForce(Vector3.down * 0.25f, ForceMode.VelocityChange);
        }

        //movement
        if (!jumpCollided)
        {
            rb.AddForce(force, ForceMode.VelocityChange);
        }

        //next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //direction graphics handling (turn the enemy to face the player)
        if (force.x >= 0.01f)
        {
            Quaternion left = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, left, speedOfRotation * Time.deltaTime);
        }
        else if (force.x <= -0.01f)
        {
            Quaternion right = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, right, speedOfRotation * Time.deltaTime);
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Banana")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if (collision.collider)
        {
            collided = true;
        }
        else
        {
            collided = false;
        }
    }
}
