using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityColliderController : MonoBehaviour
{

    private BoxCollider m_collider = null;
    public LayerMask layer;
    public bool active = false;
    Vector3 centre;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(centre, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!active)
        {
            print("checking");
            if (other.gameObject.CompareTag("Player"))
            {
                active = true;
                print("hello");
                GravityManager.AddToGravityList(transform.position, transform.localScale, Quaternion.identity, layer);
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
        centre = transform.position;
    }
}
