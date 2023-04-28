using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField] private GameObject Heart1;
    [SerializeField] private GameObject Heart2;
    [SerializeField] private GameObject Heart3;

    public void RemoveHeart()
    {
        if (Heart3.activeSelf)
            Heart3.SetActive(false);
        else if (Heart2.activeSelf)
            Heart2.SetActive(false);
        else if (Heart1.activeSelf)
            Heart1.SetActive(false);
    }
}
