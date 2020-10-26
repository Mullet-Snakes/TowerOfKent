using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerScript : MonoBehaviour
{
    public DoorConditionScript condition = null;
    public bool canOpen = false;

    public Transform left;
    public Transform right;

    public float timeToMove = 2f;
    public float distanceToMove = 5f;

    private DoorState doorState = DoorState.NONE;

    enum DoorState
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            OpenDoor();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            CloseDoor();
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            if(condition != null)
            {
                canOpen = condition.CheckCondition();

            }

            if (canOpen)
            {
                OpenDoor();
            }

        }
    }
}
