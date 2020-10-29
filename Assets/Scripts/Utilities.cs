﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities
{
    public static void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        { 
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    
    }
}
