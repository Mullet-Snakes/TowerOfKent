using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoorConditionScript : DoorConditionScript
{

    public string keyName = null;

    override public bool CheckCondition()
    {
        bool condition = KeyChainScript.HasKey(keyName) ? true : false;

        return condition;
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
