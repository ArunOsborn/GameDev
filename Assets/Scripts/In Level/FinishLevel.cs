using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public bool finish;

    private void Awake()
    {
        finish = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Finish door hit!");
        if (other.gameObject.tag == "Player")
        {
            finish = true;
            Debug.Log("Level select screen");
        }
    }
}
