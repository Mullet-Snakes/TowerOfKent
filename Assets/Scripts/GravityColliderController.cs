using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityColliderController : MonoBehaviour
{

    private BoxCollider m_collider = null;
    public LayerMask layer;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("hello");
            GravityManager.AddToGravityList(m_collider.transform.position, m_collider.size, m_collider.transform.rotation, layer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GravityManager.ClearCurrentList();
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
