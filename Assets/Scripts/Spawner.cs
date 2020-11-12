using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemToSpawn = null;
    private GameObject currentObject = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckObject");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObject == null)
        {
            Spawn(transform.position, transform.rotation);
        }
    }

    public void Spawn(Vector3 pos, Quaternion rot)
    {
        GameObject go = Instantiate(itemToSpawn, pos, rot);
        go.SetActive(true);
        currentObject = go;

    }
}
