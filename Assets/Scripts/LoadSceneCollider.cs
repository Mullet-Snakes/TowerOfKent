using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneCollider : MonoBehaviour
{
    public bool loaded = false;
    public string levelName = "";

    private void OnTriggerEnter(Collider other)
    {
        print("hittig");
        if (other.gameObject.CompareTag("Player"))
        {
            if (!loaded)
            {
                print("load");
                Utilities.LoadScene(levelName);
            }

            loaded = true;
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
