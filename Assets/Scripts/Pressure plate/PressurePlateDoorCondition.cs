using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateDoorCondition : DoorConditionScript
{
    [SerializeField]
    [Tooltip("Drag pressure plates here")]
    private List<GameObject> pressurePlateList = null;

    override public bool CheckCondition()
    {
        foreach(GameObject plate in pressurePlateList)
        {
            PressurePlateScript temp = plate.GetComponent<PressurePlateScript>();

            if (temp != null)
            {
                if(!temp.IsColliding)
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
