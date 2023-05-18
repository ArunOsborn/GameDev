using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(health.lives <= 0 || health.gameOver)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
        }
    }
}
