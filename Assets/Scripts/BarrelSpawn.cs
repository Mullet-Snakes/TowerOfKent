using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawn : MonoBehaviour
{
    public GameObject barrelPrefab;
    public Vector3 spawnPos = new Vector3();

    private void Start()
    {
        spawnPos = transform.position;
        print(spawnPos);
        NewBarrel(transform.position, transform.rotation);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            NewBarrel(transform.position, transform.rotation);
        }
    }

    public void NewBarrel(Vector3 pos, Quaternion rot)
    {
        print("explode : " + spawnPos);

        Instantiate(barrelPrefab, pos, rot);

    }
}
