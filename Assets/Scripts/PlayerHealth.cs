using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    public GameObject HUD;

    private int lives = 3;

    void LoseLife()
    {
        lives--;
        Debug.Log("Lives: "+lives);
        if (lives <= 0)
        {
            SceneHandler.LoadMainMenu();
        }
        else
        {
            HUD.GetComponent<HudController>().RemoveHeart();
            this.transform.position = respawnPoint.position;
            Physics.SyncTransforms();
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
