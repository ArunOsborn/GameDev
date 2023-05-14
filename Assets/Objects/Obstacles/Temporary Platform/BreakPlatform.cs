using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    public float targetTimeForPlatformToBreak = 2.0f;
    bool beginTimer = false;

    void Update()
    {
        if (beginTimer)
        {
            targetTimeForPlatformToBreak -= Time.deltaTime;
            Color color = this.GetComponent<MeshRenderer>().material.color;
            color.a = targetTimeForPlatformToBreak/2;
            this.GetComponent<MeshRenderer>().material.color = color;
            if (targetTimeForPlatformToBreak <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PLATFORM COL: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision with player from temp platform");

            Debug.Log(collision.contacts[0].normal);

            //checks if the normal collision is on a specific face of the platform

            if(collision.contacts[0].normal[1] <= 1.0f)
            {
                beginTimer = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("checking");
    }
}
