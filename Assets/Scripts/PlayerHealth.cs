using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    public GameObject HUD;

    public int lives = 3;

    private bool healthLock = false; // When true, player cannot be hurt

    private void Awake()
    {
        /*Debug.Log("This player's lives: " + lives);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            foreach (GameObject player in players)
            {
                if (player != this.gameObject)
                {
                    Destroy(player);
                    Debug.Log("Destryed uneeded player :)");
                }
            }
        }
        DontDestroyOnLoad(this.gameObject);*/
    }

    private void DoInvincibleTime()
    {
        healthLock = false;
    }

    void LoseLife()
    {
        if (healthLock==false)
        {
            healthLock = true;
            Invoke("DoInvincibleTime", 1);
            lives--;
            Debug.Log("Lives: " + lives);
            if (lives <= 0)
            {
                SceneManager.LoadSceneAsync(PlayerPrefs.GetString("Selected Level"));
                GameObject.Find("EventSystem").GetComponent<SceneHandler>().LoadMainMenu();
            }
            else
            {
                HUD.GetComponent<HudController>().RemoveHeart();
            }
        }
    }


    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("HIT on collision enter. Tag is"+other.gameObject);

        if (other.collider.CompareTag("Player"))
        {

            //Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Lava")
        {
            LoseLife();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            LoseLife();
            print("Hit trigger");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            print("AGAIN");
        }
    }

    void OnTriggerExit(Collider other)
    {

    }
}
