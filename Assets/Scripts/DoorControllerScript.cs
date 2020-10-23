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

    private bool running = false;
    private bool open = false;
    private bool closed = true;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator MoveDoor(Transform door, float dist)
    {
        Rigidbody door_rb = door.GetComponent<Rigidbody>();
        Vector3 startingPos = door.position;
        Vector3 movement = door.right * dist;
        Vector3 endPos =  new Vector3(door.position.x + movement.x, door.position.y + movement.y, door.position.z + movement.z);

        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            running = true;
            door_rb.MovePosition(Vector3.Lerp(startingPos, endPos, (elapsedTime / timeToMove)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        running = false;
    }

    public void OpenDoor()
    {
        if(closed && !running)
        {
            StartCoroutine(MoveDoor(right, -distanceToMove));
            StartCoroutine(MoveDoor(left, distanceToMove));
            open = true;
            closed = false;
        }
    }

    public void CloseDoor()
    {
        if(open && !running)
        {
            StartCoroutine(MoveDoor(right, distanceToMove));
            StartCoroutine(MoveDoor(left, -distanceToMove));
            open = false;
            closed = true;
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
