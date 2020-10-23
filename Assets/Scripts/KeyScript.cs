using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public string keyName = "bluekey";
    private GameObject player = null;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        KeyChainScript.AddKey(keyName);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {         
            if(Vector3.Distance(transform.position, player.transform.position) < 5)
            {
                KeyChainScript.PickUpKey(keyName);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            KeyChainScript.PrintKeyChain();
        }
    }
}
