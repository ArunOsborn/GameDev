using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ephemeral : MonoBehaviour
{
    [SerializeField] private float lifetime = 1;
    [SerializeField] private float fadetime = 0.2f;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime<=fadetime)
        {
            Color color = this.GetComponent<MeshRenderer>().material.color;
            color.a = lifetime/fadetime;
            this.GetComponent<MeshRenderer>().material.color = color;
            if (lifetime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
