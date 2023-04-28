using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateFPS", 0.1f, 0.5f);
    }

    private void UpdateFPS()
    {
        GetComponent<TextMeshProUGUI>().text = "FPS: " + Mathf.Round(1 / Time.deltaTime);
    }
}
