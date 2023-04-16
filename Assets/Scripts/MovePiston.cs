using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiston : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AudioClip upSound;
    [SerializeField] private AudioClip downSound;

    private Vector3 startPosition;
    private bool up = true;

    private AudioSource audioSource;

    private void Start()
    {
        startPosition = transform.position;
        audioSource = this.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        
        if (up)
        {
            if (transform.position.y > startPosition.y - moveDistance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
            }
            else // Switch movement
            {
                up = false;
                audioSource.clip = upSound;
                audioSource.Play();
            }
        }
        else
        {
            if (transform.position.y < startPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
            }
            else // Switch movement
            {
                up = true;
                audioSource.clip = downSound;
                audioSource.Play();
            }
        }
    }
}
