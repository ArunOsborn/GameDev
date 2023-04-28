using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepObjectInLevel : MonoBehaviour
{
    string firstSceneName = null;

    private void Awake()
    {
        Debug.Log(gameObject.name + " is awake");
        DontDestroyOnLoad(this.gameObject);
        Debug.Log(firstSceneName);
        if (firstSceneName==null)
        {
            Debug.Log("First scene not set for " + this.gameObject.name);
            firstSceneName = SceneManager.GetActiveScene().name;
        }
        else
        {
            Debug.Log("Object "+gameObject.name+"already loaded");
            if (!SceneManager.GetActiveScene().name.Contains("Level")) // Keeps object when entering a level
            {
                Debug.Log("Not level. Destroying " + this.gameObject.name);
                Destroy(this.gameObject);
            }
        }
    }
}
