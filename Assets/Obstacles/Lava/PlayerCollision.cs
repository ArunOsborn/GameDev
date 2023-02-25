using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    bool col = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(col)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }

    void onCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Lava")
        {
            col = true;
            Debug.Log("Lava collision");
        }
    }

}
