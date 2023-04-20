using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Transactions;

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
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody rb;
    private Animator animator;
    private Transform currentPointB;
    public GameObject PatrolA;
    public GameObject PatrolB;
    [SerializeField] private LayerMask levelGround;
    public bool collided;

    // Start is called before the first frame update
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentPointB = PatrolB.transform;
        animator.SetBool("isRunning", true);

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

        private void flip()
    {
        Vector3 local = transform.localScale;
        local.x *= -1;
        transform.localScale = local;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
        else
        {
            Vector3 point = currentPointB.position - transform.position;
            if(currentPointB == PatrolB.transform)
            {
                rb.AddForce(this.transform.forward * 0.3f, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(-this.transform.forward * 0.3f, ForceMode.VelocityChange);
            }

            if (Vector2.Distance(transform.position, currentPointB.position) < 0.5f && currentPointB == PatrolB.transform)
            {
                flip();
                currentPointB = PatrolA.transform;
            }
            if(Vector2.Distance(transform.position, currentPointB.position) < 0.5f && currentPointB == PatrolA.transform)
            {
                flip();
                currentPointB = PatrolB.transform;
            }
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

        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + jumpCheckOffset);
        Debug.Log("GROUNDED VARIABLE = " + isGrounded);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        //direction calculation
        Vector2 direction = ((Vector3)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //jump
        if (jumpEnabled && isGrounded)
        {
            Debug.Log("Is Grounded");
            if(direction.y > jumpNodeHeightRequirement)
            {
                Debug.Log("enemy jump");
                rb.AddForce(Vector3.up * airSpeed, ForceMode.VelocityChange);
            }
        }
        
        //movement
        //if(collided)
        //{
        //    rb.AddForce(force);
        //}

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

        //direction graphics handling
        if (force.x >= 0.01f)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(90f, 0f, 0f));
        }
        else if (force.x <= -0.01f)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(-90f, 0f, 0f));
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
        if (collision.collider)
        {
            Debug.Log("true");
            collided = true;
        }
        else
        {
            collided = false;
        }
    }
}
