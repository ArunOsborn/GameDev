using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionLava : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Lava")
        {
            transform.position = new Vector3(0f, 0f, 0f);
            Physics.SyncTransforms();
            Debug.Log("Lava collision");
        }
    }

}
