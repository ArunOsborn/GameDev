using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPlayerCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) // Stops player jumping on banana when they throw it.
    {
        Debug.Log("Tag: " + collision.gameObject.tag+" Name: "+collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), collision.collider);
            Debug.Log("Player-Banana collision ignored");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), collision.collider);
            Debug.Log("Player-Banana collision ignored");
        }
    }
}
