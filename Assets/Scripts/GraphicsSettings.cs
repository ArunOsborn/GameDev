using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{
    public void SetGraphicsTier(int tier)
    {
        UnityEngine.Graphics.activeTier = UnityEngine.Rendering.GraphicsTier.Tier1;
        UnityEngine.QualitySettings.SetQualityLevel(tier);
        Debug.Log("Graphics set to " + (tier));
        PlayerPrefs.SetInt("Graphics Tier", (tier + 1));
    }
}
