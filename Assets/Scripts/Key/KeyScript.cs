using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : InteractableObjectScript
{
    public string keyName;

    public KeyScript(string name)
    {
        keyName = name;
    }

    private void Awake()
    {

    }

    protected override void CheckForInteract(GameObject playerPos)
    {
        if (Vector3.Distance(transform.position, playerPos.transform.position) < distanceToInteract)
        {
            KeyChainScript.PickUpKey(keyName);

            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        KeyChainScript.AddKey(keyName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            KeyChainScript.PrintKeyChain();
        }
    }
}
