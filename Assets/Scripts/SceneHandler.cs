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

    public void LoadLevel(int levelNumber)
    {
        PlayerPrefs.SetString("Selected Level", "Level "+levelNumber);
        Debug.Log("Set level to "+PlayerPrefs.GetString("Selected Level"));
        ResumeLevel();
    }

    public void RestartLevel()
    {
        ResumeLevel();
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void ResumeLevel()
    {
        if (PlayerPrefs.GetString("Selected Level") == "") // Defaults to level 1
        {
            PlayerPrefs.SetString("Selected Level", "Level 1");
            Debug.Log("Set level to default of level 1");
        }
        Debug.Log("Loading" + PlayerPrefs.GetString("Selected Level"));
        SceneManager.LoadSceneAsync("Preload");
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