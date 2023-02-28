using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;


    void OnCollisionEnter(Collision other)
    {

        Debug.Log("HIT on collision enter. Tag is"+other.gameObject);

        if (other.collider.CompareTag("Player"))
        {

            //Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Lava")
        {
            transform.position = new Vector3(0f, 0f, 0f);
            Physics.SyncTransforms();
            Debug.Log("Lava collision");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            print("HIT");
            //player.transform.position = respawnPoint.transform.position;

            print("move player");

            print(transform.position);
            transform.position = new Vector3(0f, 0f, 0f);
            Physics.SyncTransforms();
            print(transform.position);
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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
