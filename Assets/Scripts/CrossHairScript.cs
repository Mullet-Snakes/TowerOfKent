using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairScript : MonoBehaviour
{

    public GameObject CrossHairUI;
    
    // Update is called once per frame
    void Update()
    {
        while (PauseMenu.GameIsPaused)
        {
            CrossHairUI.SetActive(false);
        }
    }
}
