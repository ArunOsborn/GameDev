using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool Paused = false;
    // Start is called before the first frame update
    void Awake()
    {
        Paused = false;
    }

    void Start()
    {
        Resume();
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Paused == false)
        {
            ResumeCheck();

        }
        else
        {
            PauseCheck();
        }
    }

    public void PauseCheck()
    {
        if (Input.GetKeyDown("escape") == true)
        {
            Resume();
        }
    }

    public void ResumeCheck()
    {
        if (Input.GetKeyDown("escape") == true)
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Paused = true;
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }

    public void Resume()
    {

        Paused = false;
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }
}

