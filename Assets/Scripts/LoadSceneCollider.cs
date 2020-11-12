﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneCollider : MonoBehaviour
{
    private bool completed = false;
    public string levelName = "";
    public bool trueIfWantingToLoad = false;

    private void OnTriggerEnter(Collider other)
    {
        print("hittig");
        if(trueIfWantingToLoad)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!completed)
                {
                    print("load");
                    Utilities.LoadScene(levelName);
                }

                completed = true;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!completed)
                {
                    print("delete");
                    Utilities.UnloadScene(levelName);
                }

                completed = true;
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
