using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public PlayerHealth health;
    public GameObject player;
    private float timeToEnd = .5f;
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
            Debug.Log(timeToEnd);
            timeToEnd -= Time.deltaTime;
            if (timeToEnd <= 0)
            {
                Time.timeScale = 0.0f;
                Cursor.visible = true;
            }
            gameOver.SetActive(true);
            //player.SetActive(false);
        }
    }
}
