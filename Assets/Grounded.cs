using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    EnemyAIPathfinding enemyGroundedVariable;
    [SerializeField] GameObject enemy;
    int layerName;

    // Start is called before the first frame update
    void Start()
    {
        enemyGroundedVariable = enemy.GetComponent<EnemyAIPathfinding>();
        layerName = LayerMask.NameToLayer("Level");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == layerName)
        {
            enemyGroundedVariable.isGrounded = true;
            Debug.Log("Enemy grounded variable: " + enemyGroundedVariable.isGrounded);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        enemyGroundedVariable.isGrounded = false;
        Debug.Log("Enemy grounded variable: " + enemyGroundedVariable.isGrounded);
    }
}
