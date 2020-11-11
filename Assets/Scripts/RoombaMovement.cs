﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoombaMovement : MonoBehaviour
{
    private Rigidbody m_rb = null;

    public float m_speed;

    public bool isGrounded = false;

    public bool rotating = false;

    public GameObject taar;
    public Vector3 tar = new Vector3();
    public Vector3 unit = new Vector3();

    public LayerMask floor;

    public NavMeshAgent m_agent = null;



    float t = 0;

    private void OnEnable()
    {
        GravityManager.GravityChange += TurnOffAgent;
    }

    private void OnDisable()
    {
        GravityManager.GravityChange -= TurnOffAgent;
    }


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void TurnOffAgent(Vector3 grav, bool changingTargeted)
    {
        m_agent.enabled = false;
    }

        // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if(t > 2)
        {
            GetTarget();
            t = 0;
        }
    }

    Vector3 GetTarget()
    {
        bool found = false;

        while(!found)
        {
            Vector3 finalPosition = new Vector3();
            Vector3 randomDirection = Random.insideUnitSphere * 20;
            randomDirection = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
            randomDirection += transform.position;          
            
            if(NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 2, 1))
            {
                finalPosition = hit.position;

                taar.transform.position = finalPosition;

                found = true;
            }

            return finalPosition;
        }

        return Vector3.zero;
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.51f);

        Vector3 right = new Vector3();
        Vector3 forward = new Vector3();
        Vector3 g = transform.GetComponent<GravityForce>().GetForce().normalized;
        Quaternion targetRot = new Quaternion();
        float m_rotationSpeed = 2f;
        float dotProduct = Vector3.Dot(Vector3.Normalize(g), -transform.up);


        if (dotProduct < 0.995f)
        {
            rotating = true;
            right = Vector3.Cross(-g, transform.forward);
            forward = Vector3.Cross(right, -g);
            targetRot = Quaternion.LookRotation(forward, -g);
            m_rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, m_rotationSpeed));

        }
        else if (rotating)
        {
            right = Vector3.Cross(-g, transform.forward);
            forward = Vector3.Cross(right, -g);
            targetRot = Quaternion.LookRotation(forward, -g);
            m_rb.MoveRotation(targetRot);
            rotating = false;
        }
        else if (isGrounded)
        {
            m_agent.enabled = true;
        }
    }
}
