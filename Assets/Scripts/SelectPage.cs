using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPage : MonoBehaviour
{
    [SerializeField] private GameObject[] pageList;

    public void ChangePage(string pageName)
    {
        foreach (GameObject page in pageList)
        {
            if (page.name == pageName)
            {
                Debug.Log("Found page with name: " + pageName);
                page.SetActive(true);
            }
            else
            {
                page.SetActive(false);
            }
        }
    }
}
