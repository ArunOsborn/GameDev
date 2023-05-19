using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectileObject;
    public float projectileSpeed = 3;

    public PlayerControls playerControls;
    private InputAction throwControl;

    private bool m_fire;
    public float fireCooldownTime = 1;
    private float cooldown = 0;
    private GameObject newProjectile;
    private Animator animator;


    private void Start()
    {
        cooldown = fireCooldownTime;
        animator = GetComponent<Animator>();
    }

    public void Fire(InputAction.CallbackContext context) // Gets called when fire button pressed
    {
        //Debug.Log(context);
        m_fire = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown>0)
            cooldown -= Time.deltaTime;
        if (m_fire && cooldown <= 0)
        {
            cooldown = fireCooldownTime;
            newProjectile = Object.Instantiate(projectileObject);
            newProjectile.layer = 8;
            Physics.IgnoreCollision(newProjectile.transform.GetComponent<Collider>(),GetComponent<Collider>());
            //Debug.Log("Rotation: " + this.transform.rotation.eulerAngles.y);

            if (this.transform.rotation.eulerAngles.y < 180)
            {
                //Debug.Log("Right throw");
                newProjectile.transform.position = new Vector3(this.transform.position.x + 1.2f, this.transform.position.y + 1, this.transform.position.z);
                newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(projectileSpeed, 3, 0);
            }
            else
            {
                //Debug.Log("Left throw");
                newProjectile.transform.position = new Vector3(this.transform.position.x - 1.2f, this.transform.position.y + 1, this.transform.position.z);
                newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(-projectileSpeed, 3, 0); ;
            }

            //Debug.Log("setting to true");
            //Debug.Log(animator.GetLayerWeight(animator.GetLayerIndex("throwing")));

            if (animator.GetLayerWeight(animator.GetLayerIndex("throwing")) <= 0.0f)
            {
                animator.SetLayerWeight(animator.GetLayerIndex("throwing"), 1.0f);
                animator.SetBool("m_fire", true);
            }
            else
            {
                animator.SetLayerWeight(animator.GetLayerIndex("throwing"), cooldown);
                animator.SetBool("m_fire", true);
            }
           

        }            
        else
        {
            animator.SetLayerWeight(animator.GetLayerIndex("throwing"), cooldown);


            //Debug.Log("setting to false");
            animator.SetBool("m_fire", false);
        }
    }

    private void OnCollisionEnter(Collision collision) // Stops player jumping on banana when they throw it.
    {
        Debug.Log("Tag: "+collision.gameObject.tag);
        if (collision.gameObject.tag=="Banana")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);
            Debug.Log("Banana collision ignored");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Banana")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);
            Debug.Log("Banana collision ignored");
        }
    }
}
