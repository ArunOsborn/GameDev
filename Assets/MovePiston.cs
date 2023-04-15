using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiston : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;

    private Vector3 startPosition;
    private bool up = true;

    private void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        
        if (up)
        {
            if (transform.position.y > startPosition.y - moveDistance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
            }
            else
                up = false;
        }
        else
        {
            if (transform.position.y < startPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
            }
            else
                up = true;
        }
    }
}
