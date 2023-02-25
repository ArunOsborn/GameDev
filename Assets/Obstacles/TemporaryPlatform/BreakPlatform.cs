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
            if (targetTimeForPlatformToBreak <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            beginTimer = true;
            Debug.Log("Collision with player from temp platform");
        }
    }
}
