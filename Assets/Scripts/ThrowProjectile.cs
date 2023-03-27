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

    private void Start()
    {
        cooldown = fireCooldownTime;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log(context);
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
            GameObject newProjectile = Object.Instantiate(projectileObject);
            Debug.Log("Rotation: " + this.transform.rotation.eulerAngles.y);

            if (this.transform.rotation.eulerAngles.y < 180)
            {
                Debug.Log("Right throw");
                newProjectile.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z);
                newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(projectileSpeed, 3, 0); // TODO: Change velocity dependant on the direction the monkey faces
            }
            else
            {
                Debug.Log("Left throw");
                newProjectile.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y + 1, this.transform.position.z);
                newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(-projectileSpeed, 3, 0); ; // TODO: Change velocity dependant on the direction the monkey faces
            }
        }
    }
}
