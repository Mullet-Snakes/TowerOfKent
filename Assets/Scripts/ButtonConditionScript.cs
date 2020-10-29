using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonConditionScript : DoorConditionScript
{
    public bool pressed = false;

    override public bool CheckCondition()
    {
        if(pressed)
        {
            return true;
        }

        return false;
        
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
