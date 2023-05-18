using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollScript : MonoBehaviour
{
    EnemyAIPathfinding enemyVariable;
    [SerializeField] GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyVariable = enemy.GetComponent<EnemyAIPathfinding>();
        setColliderState(false);
        setRigidBodyState(true);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if(enemyVariable.dead)
        {
            die();
        }
    }

    void die()
    {
        setRigidBodyState(false);
        setColliderState(true);
        Destroy(gameObject, 3f);
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
