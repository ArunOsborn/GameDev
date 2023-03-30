using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayerPosition : MonoBehaviour
{
    float x;
    float y;
    float z;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        z = GameObject.FindGameObjectWithTag("Player").transform.position.z;
        y = GameObject.FindGameObjectWithTag("Player").transform.position.y;
        x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        Debug.Log(x + " " + y + " " + z);
        GameObject.FindGameObjectWithTag("CopyPlayerPos").transform.position = new Vector3(x, y, z);
    }
}
