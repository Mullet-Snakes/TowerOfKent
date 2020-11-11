using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class RoombaMoveForce : ForceScript
{
    private RoombaMovement controllerScript = null;

    private Rigidbody m_rb = null;

    private void Awake()
    {
        controllerScript = GetComponent<RoombaMovement>();

        m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        force = Vector3.zero;

        if (controllerScript.isGrounded && !controllerScript.rotating)
        {

            //force += (controllerScript.target.transform.position - transform.position).normalized * controllerScript.m_speed;

        }
    }

    public override Vector3 GetForce()
    {
        if(force != Vector3.zero)
        {
            return force -= m_rb.velocity;
        }
        else
        {
            return Vector3.zero;
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
