using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{

    private bool saved = false;
    public string sceneToLoad = "";

    private void OnTriggerEnter(Collider other)
    {
        if(!saved)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Utilities.SaveGame(sceneToLoad);
                saved = true;
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

        if(Input.GetKeyDown(KeyCode.M))
        {
            print(PlayerPrefs.GetString("Progress"));
        }

    }
}
