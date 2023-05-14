using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 positionOffset;
    public Vector3 angleOffset;
    public bool cameraPreset = true;

    public bool fixYLevel = false;
    public float yLevel;

    Vector3 exactFollow;

    private void Start()
    {
    }
    void FixedUpdate ()
    {
        exactFollow = target.position + positionOffset;
        if (fixYLevel)
            exactFollow.y = (float)yLevel;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, exactFollow, smoothSpeed);
        transform.position = smoothFollow;
        

        //transform.LookAt(target.position + angleOffset);
    }
}
