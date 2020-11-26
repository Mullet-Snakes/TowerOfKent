using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnCollide : MonoBehaviour
{

    [SerializeField] private GameObject plug = null;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            plug.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        plug.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
