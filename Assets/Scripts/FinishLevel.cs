using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Finish door hit!");
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            Debug.Log("Back to main menu");
            SceneManager.LoadScene("Main Menu");
        }
    }
}
