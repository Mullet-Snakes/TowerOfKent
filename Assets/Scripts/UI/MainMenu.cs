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
        //SceneManager.LoadScene("Greybox TUT Level");
        Utilities.LoadScene("Greybox TUT Level", false);
    }

    //Loads Save Game
    public void LoadGame()
    {
        string levelData = PlayerPrefs.GetString("Progress");
        Debug.Log("Loading Save Game");
        Debug.Log(levelData);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Utilities.LoadScene(levelData, false);
        Utilities.UnloadScene("MainMenu");
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


