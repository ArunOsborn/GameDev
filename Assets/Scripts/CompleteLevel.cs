using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public GameObject completeLevel;
    public FinishLevel finishLevel;

    private void Awake()
    {
        completeLevel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(finishLevel.finish)
        {
            completeLevel.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
        }
    }
}
