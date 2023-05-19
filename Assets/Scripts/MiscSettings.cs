using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscSettings : MonoBehaviour
{
    public void ResetControlsTutorial()
    {
        PlayerPrefs.SetInt("controls-tutorial", 0);
    }
}
