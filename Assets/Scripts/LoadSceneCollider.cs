using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneCollider : MonoBehaviour
{
    private bool completed = false;
    public List <string> levelName = new List<string>();
    public bool trueIfWantingToLoad = false;

    private void OnTriggerEnter(Collider other)
    {
        if(trueIfWantingToLoad)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!completed)
                {
                    print("load");

                    foreach(string levelName in levelName)
                    {
                        Utilities.LoadScene(levelName);
                    }
                    
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

                    foreach (string levelName in levelName)
                    {
                        Utilities.UnloadScene(levelName);
                    }
                    
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
