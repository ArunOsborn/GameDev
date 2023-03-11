using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectileObject;
    public Vector3 projectileSpeed = new Vector3(3, 0, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject newProjectile = Object.Instantiate(projectileObject);
            newProjectile.transform.position = new Vector3(this.transform.position.x+3, this.transform.position.y, this.transform.position.z);
            newProjectile.GetComponent<Rigidbody>().velocity = projectileSpeed; // TODO: Change velocity dependant on the direction the monkey faces
        }
    }
}
