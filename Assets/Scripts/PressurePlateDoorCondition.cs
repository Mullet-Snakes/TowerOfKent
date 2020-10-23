using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateDoorCondition : DoorConditionScript
{
    public List<GameObject> pressurePlateList = null;

    override public bool CheckCondition()
    {
        foreach(GameObject plate in pressurePlateList)
        {
            if(plate.GetComponent<PressurePlateScript>() != null)
            {
                if(!plate.GetComponent<PressurePlateScript>().isColliding)
                {
                    return false;
                }
            }
        }

        return true;
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
