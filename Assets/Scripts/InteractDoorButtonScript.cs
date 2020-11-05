using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoorButtonScript : InteractableObjectScript
{
    public bool isPressed = false;

    [SerializeField]
    [Tooltip("Default 0.5")]
    [Range(0,2)]
    private float timeToSwitch = 0.5f;

    public bool IsPressed
    {
        get
        {
            return isPressed;
        }
    }

        

    protected override void CheckForInteract(GameObject playerPos)
    {

        if (Vector3.Distance(transform.position, playerPos.transform.position) < distanceToInteract)
        {
            isPressed = isPressed ? false : true;
        }

        Invoke("TurnOffButton", timeToSwitch);

    }

    private void TurnOffButton()
    {
        isPressed = false;
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
