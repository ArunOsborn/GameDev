using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private Transform target;


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
