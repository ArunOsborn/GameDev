using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] private GameObject graphicsTierDropdown;


    void Start()
    {
        graphicsTierDropdown.GetComponent<TMPro.TMP_Dropdown>().value = PlayerPrefs.GetInt("Graphics Tier") - 1;
    }

    public void SetGraphicsTier(int tier)
    {
        UnityEngine.Graphics.activeTier = UnityEngine.Rendering.GraphicsTier.Tier1;
        UnityEngine.QualitySettings.SetQualityLevel(tier);
        Debug.Log("Graphics set to " + (tier));
        PlayerPrefs.SetInt("Graphics Tier", (tier + 1));
    }
}
