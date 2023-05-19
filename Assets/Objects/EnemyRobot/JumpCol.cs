using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpCol : MonoBehaviour
{
    EnemyAIPathfinding enemyJumpColVariable;
    [SerializeField] GameObject enemy;
    private Collider collider;
    int layerName;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        enemyJumpColVariable = enemy.GetComponent<EnemyAIPathfinding>();

        layerName = LayerMask.NameToLayer("Level");
    }

    void FixedUpdate()
    {
        if(enemyJumpColVariable.dead)
        {
            Destroy(collider);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == layerName)
        {
            enemyJumpColVariable.jumpCollided = true;
            //Debug.Log("Enemy Jump Variable: " + enemyJumpColVariable.jumpCollided);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == layerName)
        {
            enemyJumpColVariable.jumpCollided = false;
            //Debug.Log("Enemy Jump Variable: " + enemyJumpColVariable.jumpCollided);
        }
    }
}
