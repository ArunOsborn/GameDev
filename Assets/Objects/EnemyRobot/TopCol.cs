using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCol : MonoBehaviour
{
    EnemyAIPathfinding enemy;
    [SerializeField] GameObject enemyObject;
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        enemy = enemyObject.GetComponent<EnemyAIPathfinding>();
    }

    void FixedUpdate()
    {
        if(enemy.dead)
        {
            Destroy(collider);
        }
    }
}
