using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollPlayer : MonoBehaviour
{
    PlayerHealth playerVariable;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerVariable = player.GetComponent<PlayerHealth>();
        setColliderState(false);
        setRigidBodyState(true);
    }
    private void FixedUpdate()
    {
        if (playerVariable.gameOver)
        {
            die();
        }
    }

    void die()
    {
        setRigidBodyState(false);
        setColliderState(true);
    }

    void setRigidBodyState(bool state)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rbs in bodies)
        {
            rbs.isKinematic = state;
        }
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = state;
        }
    }
}
