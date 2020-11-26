using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityColliderController : MonoBehaviour
{

    private BoxCollider m_collider = null;
    public LayerMask layer;
    public bool active = false;
    Vector3 centre;
    public float timer = 0f;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(timer >= 1)
        {
            if (!active)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    active = true;
                    print("hello");
                    GravityManager.ClearCurrentList();
                    GravityManager.AddToGravityList(transform.position, transform.localScale, Quaternion.identity, layer);
                    timer = 0;
                }
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
        if(timer < 1)
        {
            timer += Time.deltaTime;

        }
    }
}
