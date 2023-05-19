using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBox : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("controls-tutorial")==1)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("controls-tutorial")!=1 && (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.E)))
        {
            this.gameObject.SetActive(false);
            PlayerPrefs.SetInt("controls-tutorial", 1);
        }
    }
}
