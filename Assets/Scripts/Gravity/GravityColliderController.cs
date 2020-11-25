using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityColliderController : MonoBehaviour
{

    private BoxCollider m_collider = null;
    public LayerMask layer;
    private bool active = false;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!active)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                active = true;
                print("hello");
                GravityManager.AddToGravityList(m_collider.transform.position, m_collider.size, m_collider.transform.rotation, layer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(active)
        {
            print("goodbye");
            GravityManager.ClearCurrentList();
            active = false;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
    }
}
