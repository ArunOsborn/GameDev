using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameByFrame : MonoBehaviour
{
    public bool frameByFrame;
    private bool unpause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frameByFrame)
        {
            Time.timeScale = 0.0f;
            if (Input.GetKeyDown(KeyCode.Return) && unpause==false)
            {
                Debug.Log("return pressed");
                Time.timeScale = 1.0f;
                unpause = true;
            }
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            Time.timeScale = 0.1f;
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            Time.timeScale = 1.0f;
        }
    }

    private void LateUpdate()
    {
        if (unpause)
        {
            Time.timeScale = 0.0f;
            unpause = false;
        }
    }
}
