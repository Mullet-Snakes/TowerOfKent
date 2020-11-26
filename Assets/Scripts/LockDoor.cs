using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public GameObject door = null;
    public string keyToDelete = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KeyChainScript.RemoveKey(keyToDelete);
            door.GetComponent<DoorControllerScript>().CloseDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
