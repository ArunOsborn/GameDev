using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockLevels : MonoBehaviour
{
    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 4);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 4 > levelAt)
                levelButtons[i].interactable = false;
        }
    }
}
