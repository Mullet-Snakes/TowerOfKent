using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoombaController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform body;
    public float wanderRange;
    private Vector3 targetDestination = Vector3.zero;
    private float distanceToTarget = 0.4f;
    private SpotPlayerScript spotPlayer = null;
    private GameObject player = null;
    private DoombaState m_state = DoombaState.DEFAULT;
    Vector3 delta;
    private enum DoombaState
    {
        DEFAULT,
        PATROLING,
        ATTACKING,

    };

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        body = transform.GetChild(0);
        spotPlayer = GetComponent<SpotPlayerScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_state = DoombaState.PATROLING;
        StartCoroutine("CheckIfFound");
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + delta);
    }

    public IEnumerator CheckIfFound()
    {
        YieldInstruction wfs = new WaitForSeconds(0.5f);

        while (true)
        {
            if (spotPlayer.CheckIfAttacking(player))
            {
                m_state = DoombaState.ATTACKING;
            }
            else
            {
                m_state = DoombaState.PATROLING;
            }

            yield return wfs;
        }
    }


    // Update is called once per frame
    void Update()
    {

        switch (m_state)
        {
            case DoombaState.DEFAULT:

                break;

            case DoombaState.PATROLING:

                agent.isStopped = false;

                if (targetDestination == Vector3.zero)
                {
                    SetDestination(GetRandomPoint());
                    targetDestination = agent.destination;
                }

                if ((agent.destination - transform.position).magnitude <= distanceToTarget)
                {
                    SetDestination(GetRandomPoint());
                    targetDestination = agent.destination;
                }


                if (agent.desiredVelocity != Vector3.zero)
                {
                    Vector3 patDir = Vector3.RotateTowards(body.forward, agent.desiredVelocity, 2 * Time.deltaTime, 0.0f);

                    body.rotation = Quaternion.LookRotation(patDir, transform.up);
                }

                break;

            case DoombaState.ATTACKING:

                agent.isStopped = true;

                delta = player.transform.position - transform.position;
                delta = transform.InverseTransformDirection(delta.x, 0, delta.z);
                delta = transform.TransformDirection(delta);
                delta = delta.normalized;

                Vector3 atkDir = Vector3.RotateTowards(body.forward, delta, 2 * Time.deltaTime, 0.0f);

                body.rotation = Quaternion.LookRotation(atkDir, transform.up);

                break;

            default:

                break;
        }
    }
}
