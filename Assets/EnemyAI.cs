using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;


    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody rb;

    [SerializeField] private bool Grounded;
    //[SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask levelGround;

    public float maxDistance;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector3)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(force.x >= 0.01f)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(90f, 0f, 0f));
        }
        else if (force.x <= -0.01f)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(-90f, 0f, 0f));
        }

        Grounded = GroundCheck();
    }

    bool GroundCheck()
    {
        CapsuleCollider c = GetComponent<CapsuleCollider>(); 
        float radius = c.radius * 0.9f;
        Vector3 pos = transform.position + Vector3.down * (radius * 0.9f);

        return Physics.CheckSphere(pos, radius, levelGround);
    }
}
