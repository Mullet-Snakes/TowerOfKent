using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoombaController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform body;
    public float wanderRange;
    private Vector3 targetDestination;
    private float distanceToTarget = 0.4f;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        body = transform.GetChild(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetDestination(GetRandomPoint());
    }

    Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        return randomDirection;
    }

    void SetDestination(Vector3 dest)
    {
        agent.destination = dest;
    }

    // Update is called once per frame
    void Update()
    {
        if ((agent.destination - transform.position).magnitude <= distanceToTarget)
        {
            SetDestination(GetRandomPoint());
            targetDestination = agent.destination;
        }


        if (agent.desiredVelocity != Vector3.zero)
        {
            Vector3 direction = Vector3.RotateTowards(body.forward, agent.desiredVelocity, 2 * Time.deltaTime, 0.0f);

            body.rotation = Quaternion.LookRotation(direction, transform.up);
        }

    }
}
