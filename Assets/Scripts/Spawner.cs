using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemToSpawn = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Spawn(transform.position, transform.rotation);
        }
    }

    public void Spawn(Vector3 pos, Quaternion rot)
    {
        Instantiate(itemToSpawn, pos, rot);
    }
}
