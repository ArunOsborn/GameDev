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
        Cursor.visible = true;
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevelSelect()
    {
        Debug.Log("Going to level select menu");
        Cursor.visible = true;
        SceneManager.LoadScene("Level Select");
    }

    public void LoadOptions()
    {
        Debug.Log("Going to options menu");
        Cursor.visible = true;
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        Application.Quit(); // TODO: Add "Are you sure?" message
    }
}
