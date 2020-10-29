using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairScript : MonoBehaviour
{

    public GameObject CrossHairUI;
    
    // Update is called once per frame
    void Update()
    {
        
        
        //Disables / Enables the crosshair depending if the game is paused or not.
        if (PauseMenu.GameIsPaused)
        {
            CrossHairUI.SetActive(false);
        }

        else
        {
            CrossHairUI.SetActive(true);
        }
    }
}
