using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    //Filler Game Object to allow toggling of the menu.
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;

    //Cursor Lockstate Setup, locking to allow the unlocking of the cursor on pause.
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to pause or unpause the game, Hotkey will be changed to ESC for the main builds but while still in unity editor will stay as TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    //Pause Meny Function, Sets Time Scale, Visibility and Lockstate of Cursor.
    void Pause()
    {
        Debug.Log("Game Paused");
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
    }

    //Resume Game Function, Sets Time Scale, Visibility and Lockstate of Cursor.
    public void Resume()
    {
        Debug.Log("Game Resumed");
        SettingsMenuUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        //SettingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
    }

    //Will Load the Settings Menu
    public void SettingsMenu()
    {
        Debug.Log("Loading Settings Menu");
        SettingsMenuUI.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        Debug.Log("Close Settings Menu");
        SettingsMenuUI.SetActive(false);
    }

    //Exits to Main Menu
    public void ExitToMainMenu()
    {
        Debug.Log("Returning to Main Menu");
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        string savedLevel = PlayerPrefs.GetString("Progress");
        Utilities.LoadScene("MainMenu");
        Utilities.UnloadScene(savedLevel);

    }

    public void Save()
    {
        Debug.Log("Saving Game.");
        Utilities.SaveGame("Progress");
        Debug.Log("Game Saved.");
    }
}
