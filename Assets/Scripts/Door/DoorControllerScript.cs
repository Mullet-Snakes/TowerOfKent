﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerScript : InteractableObjectScript
{

    [SerializeField]
    [Tooltip("Drag the conditions for the door here")]
    private List<DoorConditionScript> conditionList = null;
    
    private bool canOpen = false;

    [SerializeField]
    [Tooltip("Left door gameobject here")]
    private Transform left = null;

    [SerializeField]
    [Tooltip("Right door gameobject here")]
    private Transform right = null;

    [SerializeField]
    [Tooltip("Time to open")]
    private float timeToMove = 2f;

    [SerializeField]
    [Tooltip("Distance to open")]
    private float distanceToMove = 5f;

    private DoorState doorState = DoorState.NONE;

    private enum DoorState
    {
        NONE,
        MOVING,
        OPEN,
        CLOSED
    }

    // Start is called before the first frame update
    void Start()
    {
        doorState = DoorState.CLOSED;

        foreach(DoorConditionScript d in conditionList)
        {
            if (d is PressurePlateDoorCondition || d is ButtonConditionScript)
            {
                StartCoroutine("CheckAtLowerFPS");
                return;
            }
        }
    }

    protected override void CheckForInteract(GameObject player)
    {

        if(conditionList.Count != 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < distanceToInteract)
            {
                foreach(DoorConditionScript d in conditionList)
                {
                    if(!d.CheckCondition())
                    {
                        canOpen = false;
                        return;
                    }

                    canOpen = true;
                }
            }
        }

        if (canOpen)
        {
            OpenDoor();
        }
    }

    IEnumerator MoveDoor(Transform door, float dist, DoorState stateToMoveTo)
    {
        Rigidbody door_rb = door.GetComponent<Rigidbody>();
        Vector3 startingPos = door.position;
        Vector3 movement = door.right * dist;
        Vector3 endPos =  new Vector3(door.position.x + movement.x, door.position.y + movement.y, door.position.z + movement.z);

        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            doorState = DoorState.MOVING;
            door_rb.MovePosition(Vector3.Lerp(startingPos, endPos, (elapsedTime / timeToMove)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorState = stateToMoveTo;
    }

    public void OpenDoor()
    {
        if (doorState == DoorState.CLOSED && doorState != DoorState.MOVING)
        {
            StartCoroutine(MoveDoor(right, -distanceToMove, DoorState.OPEN));
            StartCoroutine(MoveDoor(left, distanceToMove, DoorState.OPEN));
        }
    }

    public void CloseDoor()
    {
        if(doorState == DoorState.OPEN && doorState != DoorState.MOVING)
        {
            StartCoroutine(MoveDoor(right, distanceToMove, DoorState.CLOSED));
            StartCoroutine(MoveDoor(left, -distanceToMove, DoorState.CLOSED));
        }
    }

    IEnumerator CheckAtLowerFPS()
    {
        float updateRate = 0.1f;
        YieldInstruction waitTime = new WaitForSeconds(updateRate);

        while (true)
        {

            if(conditionList.Count != 0)
            {
                foreach (DoorConditionScript d in conditionList)
                {
                    if (!d.CheckCondition())
                    {
                        canOpen = false;
                        break;
                    }

                    canOpen = true;
                }
            }

            if (canOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }

            yield return waitTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
