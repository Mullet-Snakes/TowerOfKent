using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawn : MonoBehaviour
{
    public GameObject barrelPrefab;

    private void Start()
    {
        //barrel = barrelPrefab;
        Instantiate(barrelPrefab, this.transform.position, this.transform.rotation);
    }

    public void Explode()
    {
        //barrel = barrelPrefab;
        Instantiate(barrelPrefab, this.transform.position, this.transform.rotation);
    }
}
