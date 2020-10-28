﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerScript : MonoBehaviour
{
    public DoorConditionScript condition = null;
    public bool canOpen = false;

    [SerializeField]
    [Tooltip("Default: 2")]
    [Range(0, 5)]
    private float distanceToInteract = 2f;


    public Transform left;
    public Transform right;

    public float timeToMove = 2f;
    public float distanceToMove = 5f;

    private DoorState doorState = DoorState.NONE;

    private enum DoorState
    {
        NONE,
        MOVING,
        OPEN,
        CLOSED
    }

    private void OnEnable()
    {
        InteractKeyManager.OnButtonPress += CheckForInteract;
    }

    private void OnDisable()
    {
        InteractKeyManager.OnButtonPress -= CheckForInteract;
    }

    // Start is called before the first frame update
    void Start()
    {
        doorState = DoorState.CLOSED;
    }

    private void CheckForInteract(GameObject player)
    {
        if (condition != null)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < distanceToInteract)
            {
                canOpen = condition.CheckCondition();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
