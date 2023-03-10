using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler
{
    public static void LoadLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public static void LoadMainMenu()
    {
        Debug.Log("Returning to main menu");
        Cursor.visible = true;
        SceneManager.LoadScene("Main Menu");
    }

    public static void LoadLevelSelect()
    {
        Debug.Log("Going to level select menu");
        Cursor.visible = true;
        SceneManager.LoadScene("Level Select");
    }

    public static void LoadOptions()
    {
        Debug.Log("Going to options menu");
        Cursor.visible = true;
        SceneManager.LoadScene("Options");
    }

    public static void QuitGame()
    {
        Application.Quit(); // TODO: Add "Are you sure?" message
    }
}
