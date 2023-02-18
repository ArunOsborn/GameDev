using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothspeed = 0.125f;
    public Vector3 positionOffset;
    public Vector3 angleOffset;
    public bool cameraPreset = true;

    private void Start()
    {

    }
    void FixedUpdate ()
    {
        Vector3 exactfollow = target.position + positionOffset;
        Vector3 smoothfollow = Vector3.Lerp(transform.position, exactfollow, smoothspeed);
        transform.position = smoothfollow;
        

        //transform.LookAt(target.position + angleOffset);
    }
}
