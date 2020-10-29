using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoorButtonScript : InteractableObjectScript
{
    public bool isPressed = false;

    public bool IsPressed
    {
        get
        {
            return isPressed;
        }
    }

        

    protected override void CheckForInteract(GameObject playerPos)
    {
        bool temp = false;

        if (Vector3.Distance(transform.position, playerPos.transform.position) < distanceToInteract)
        {
            temp = true;
        }     

        isPressed = temp;
    }

    private void Awake()
    {

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
