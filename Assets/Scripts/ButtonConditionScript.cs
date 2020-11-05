using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonConditionScript : DoorConditionScript
{
    [SerializeField]
    [Tooltip("Drag button/s here")]
    private List<GameObject> buttonList = null;

    override public bool CheckCondition()
    {
        foreach (GameObject button in buttonList)
        {
            InteractDoorButtonScript temp = button.GetComponent<InteractDoorButtonScript>();

            if (temp != null)
            {
                if (!temp.IsPressed)
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
