using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayerPosition : MonoBehaviour
{
    [SerializeField] GameObject player;
    float x;
    float y;
    float z;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        //z = GameObject.FindGameObjectWithTag("Player").transform.position.z;
        y = player.transform.position.y + 1.4f;
        x = player.transform.position.x;
        z = player.transform.position.z;
        //Debug.Log(x + " " + y + " " + z);
       this.gameObject.transform.position = new Vector3(x, y, z);
    }
}
