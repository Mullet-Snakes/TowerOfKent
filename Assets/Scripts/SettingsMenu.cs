using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //Global Audio Mixer
    public AudioMixer audioMixer;

    //Resolutions DropDown
    public Dropdown resolutionDropDown;

    //Array of Supported Resolutions
    Resolution[] resolutions;

    void Start()
    {
        
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        //For each available Resolution, adds to a list of resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }

        //Sets Dropdown Options to all available resolutions
        resolutionDropDown.AddOptions(options);
    }

    public void SetVolume(float volume)
    {
        Debug.Log("Volume Adjusted");
        //Updates Volume Mixer equal to slider
        audioMixer.SetFloat("volume", volume);
    }

    public void SetGraphics(int qualityIndex)
    {
        //Updates Graphics quality equal to option
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        //TOggle FullScreen
        Screen.fullScreen = isFullScreen;
    }
}
