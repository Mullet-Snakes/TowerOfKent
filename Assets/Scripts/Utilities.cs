using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities
{
    public static void LoadScene(string sceneName, bool additivly)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        { 
            if(additivly)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadSceneAsync(sceneName);
            }
            
        }
    
    }

    public static void UnloadScene(string sceneName)
    {
        Scene temp = SceneManager.GetSceneByName(sceneName);

        if(temp != null && temp.isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

    public static bool IsInLayerMask(this GameObject go, int layerMask)
    {
        return layerMask == (layerMask | 1 << go.transform.gameObject.layer);
    }

    public static void SaveGame(string sceneName)
    {
        PlayerPrefs.SetString("Progress", sceneName);
    }
}
