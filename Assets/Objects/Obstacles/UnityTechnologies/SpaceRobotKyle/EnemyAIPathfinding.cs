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
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
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
    CapsuleCollider c;
    RaycastHit hit;
    [SerializeField] private LayerMask levelGround;

    // Start is called before the first frame update
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();
        c = GetComponent<CapsuleCollider>();


        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
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

        //see if colliding with anything
        Vector3 pos = transform.position + Vector3.down * ((c.radius*0.9f) * 0.9f);

        //isGrounded = Physics.CheckSphere(pos, (c.radius * 0.9f), levelGround);

        //isGrounded = Physics.SphereCast(transform.position, (c.radius * 0.9f) * 0.9f, -Vector3.up, out hit, GetComponent<Collider>().bounds.extents.y + jumpCheckOffset);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + jumpCheckOffset);

        //direction calculation
        Vector2 direction = ((Vector3)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //jump
        if(jumpEnabled && isGrounded)
        {
            if(direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        //movement
        rb.AddForce(force);

        //next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        ////direction graphics handling
        //if (distance < nextWaypointDistance)
        //{
        //    currentWaypoint++;
        //}

        //if (force.x >= 0.01f)
        //{
        //    transform.localRotation = Quaternion.LookRotation(new Vector3(90f, 0f, 0f));
        //}
        //else if (force.x <= -0.01f)
        //{
        //    transform.localRotation = Quaternion.LookRotation(new Vector3(-90f, 0f, 0f));
        //}
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
}
