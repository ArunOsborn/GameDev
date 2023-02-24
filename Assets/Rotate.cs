using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    private void FixedUpdate()
    {
        this.transform.Rotate(rotation);
    }
}
