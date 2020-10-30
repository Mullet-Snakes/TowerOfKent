using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Starts New Game
    public void New()
    {
        Debug.Log("Starting New Game");
        KeyChainScript.ClearKeyChain();
        SceneManager.LoadScene("Greybox TUT Level");
        
    }

    //Loads Save Game
    public void LoadGame()
    {
        Debug.Log("Loading Save Game");
    }

    //Exits Game
    public void Exit()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }

    //Loads Settings Menu
    public void SettingsMenu()
    {
        Debug.Log("Loading Settings Menu");
    }
}


