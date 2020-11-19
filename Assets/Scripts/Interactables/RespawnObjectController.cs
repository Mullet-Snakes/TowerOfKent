using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObjectController : MonoBehaviour
{
    private Vector3 spawnPosition = new Vector3();
    private Transform spawnObject = null;


    private void Awake()
    {
        spawnObject = transform.parent.GetChild(1);
        spawnPosition = spawnObject.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.GetComponentInParent<PlayerController>().SpawnPosition = spawnPosition;
        }
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
