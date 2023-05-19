using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    private Animator animator;

    [SerializeField] private GameObject body;
    private Color originalBodyColour;

    public bool gameOver;

    public GameObject HUD;

    public int lives = 3;

    public float jumpOnHurt = 0.2f;
    public AudioClip monkeyHurt;
    private AudioSource audioSource;

    private bool healthLock = false; // When true, player cannot be hurt

    private void Awake()
    {
        
    }

    private void Start()
    {
        body = transform.Find("Body").gameObject;
        gameOver = false;
        animator = GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (gameOver)
        {
            animator.enabled = false;
        }
    }

    private void EndInvincibleTime()
    {
        healthLock = false;
        body.GetComponent<SkinnedMeshRenderer>().material.color = originalBodyColour;
    }

    void LoseLife()
    {
        if (healthLock==false)
        {
            healthLock = true;
            Invoke("EndInvincibleTime", 1);
            lives--;
            Debug.Log("Lives: " + lives);
            audioSource.PlayOneShot(monkeyHurt);

            if (lives <= 0)
            {
                gameOver = true;
                //Debug.Log("canvas active");
                //SceneManager.LoadSceneAsync(PlayerPrefs.GetString("Selected Level"));
                //GameObject.Find("EventSystem").GetComponent<SceneHandler>().LoadMainMenu();
            }
            else
            {
                Cursor.visible = false;
                Time.timeScale = 1.0f;
                HUD.GetComponent<HudController>().RemoveHeart();
                Debug.Log("Colour: "+body.GetComponent<SkinnedMeshRenderer>().material.color);
                SkinnedMeshRenderer renderer = body.GetComponent<SkinnedMeshRenderer>();
                originalBodyColour = renderer.material.color;
                renderer.material.color = new Color(originalBodyColour.r+0.2f,originalBodyColour.g,originalBodyColour.b,originalBodyColour.a/2);
                gameObject.GetComponent<PlayerMovement>().AddExternalMovementFactor(new Vector3(0,jumpOnHurt,0));
                /*foreach (Transform child in this.gameObject.GetComponentInChildren<Transform>().gameObject.GetComponentsInChildren<Transform>())
                {
                    child.gameObject.GetComponent<Renderer>().material.color = new Color(20,20,20);
                }*/
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
        if (other.collider.tag == "Enemy")
        {
            gameOver = true;
        }
        if(other.collider.tag == "JumpCollider")
        {
            gameOver = true;
        }
        if(other.collider.tag == "Lava")
        {
            gameOver = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Lava")
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
            LoseLife();
        }
    }

    void OnTriggerExit(Collider other)
    {

    }
}
