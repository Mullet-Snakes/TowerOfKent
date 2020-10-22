using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public string keyName = "bluekey";

    // Start is called before the first frame update
    void Start()
    {
        KeyChainScript.AddKey(keyName);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            KeyChainScript.PickUpKey(keyName);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            KeyChainScript.PrintKeyChain();
        }
    }
}
