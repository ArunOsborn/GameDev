using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMainMenu()
    {
        Debug.Log("Returning to main menu");
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevelSelect()
    {
        Debug.Log("Going to level select menu");
        SceneManager.LoadScene("Level Select");
    }

    public void QuitGame()
    {
        Application.Quit(); // TODO: Add "Are you sure?" message
    }
}
