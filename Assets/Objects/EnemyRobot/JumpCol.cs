using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCol : MonoBehaviour
{
    EnemyAIPathfinding enemyJumpColVariable;
    [SerializeField] GameObject enemy;
    int layerName;

    // Start is called before the first frame update
    void Start()
    {
        enemyJumpColVariable = enemy.GetComponent<EnemyAIPathfinding>();

        layerName = LayerMask.NameToLayer("Level");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == layerName)
        {
            enemyJumpColVariable.jumpCollided = true;
            Debug.Log("Enemy Jump Variable: " + enemyJumpColVariable.jumpCollided);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == layerName)
        {
            enemyJumpColVariable.jumpCollided = false;
            Debug.Log("Enemy Jump Variable: " + enemyJumpColVariable.jumpCollided);
        }
    }
}
