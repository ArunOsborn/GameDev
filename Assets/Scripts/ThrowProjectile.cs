using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectileObject;
    public Vector3 projectileSpeed = new Vector3(3, 0, 0);

    public PlayerControls playerControls;
    private InputAction throwControl;

    private bool m_fire;
    public float fireCooldownTime = 1;
    private float cooldown;

    private void OnEnable()
    {
        playerControls.Enable();
        throwControl = playerControls.Player.Fire;
        throwControl.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        throwControl.Disable();
    }

    private void Start()
    {
        playerControls = new PlayerControls();
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
        if (m_fire && cooldown<=0)
        {
            cooldown = fireCooldownTime;
            Debug.Log("Throw!");
            GameObject newProjectile = Object.Instantiate(projectileObject);
            newProjectile.transform.position = new Vector3(this.transform.position.x+3, this.transform.position.y, this.transform.position.z);
            newProjectile.GetComponent<Rigidbody>().velocity = projectileSpeed; // TODO: Change velocity dependant on the direction the monkey faces
        }
    }
}
