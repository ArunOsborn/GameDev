using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    private int lives = 1;

    void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    void OnCollisionEnter(Collision other)
    {

        Debug.Log("HIT on collision enter. Tag is"+other.gameObject);

        if (other.collider.CompareTag("Player"))
        {

            //Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Lava")
        {
            Debug.Log("Lava collision");
            LoseLife();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            LoseLife();
            print("Hit obstacle");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            print("AGAIN");
            //player.transform.position = respawnPoint.transform.position;

            print("move the player");
            print(transform.position);
            transform.position = new Vector3(0f, 0f, 0f);
            print(transform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {

    }
}
