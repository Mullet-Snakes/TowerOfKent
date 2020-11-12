using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class RoombaAIForce : ForceScript
{
    private NavMeshAgent m_agent = null;
    private Rigidbody m_rb = null;
    private NavMeshPath path = null;
    private int posIndex = 0;
    private Vector3 direction = new Vector3();

    public float distanceToWaypoint = 0.2f;

    public List<Vector3> waypoints = new List<Vector3>();

    private GameObject player;
    private RoombaMovement controller;

    public RoombaState lastState = RoombaState.DEFAULT;

    public bool completedPath = true;

    public float wanderRange = 30f;

    public float attackTargetCooldown = 3f;
    public float patrolTargetCooldown = 1f;

    public GameObject taar;

    public float cooldown = 0f;



    private void Awake()
    {
        controller = GetComponent<RoombaMovement>();
        player = GameObject.FindWithTag("Player");
        m_rb = GetComponent<Rigidbody>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        path = m_agent.path;
        m_agent.updatePosition = false;
        m_agent.updateRotation = false;
        direction = Vector3.zero;
    }

    public override Vector3 GetForce()
    {

        if(controller.m_state == RoombaState.ATTACKING)
        {
            if(m_agent.isOnNavMesh)
            {
                if(controller.distToPlayer > 5)
                {
                    return direction - m_rb.velocity;
                }
                else
                {
                    return Vector3.zero - m_rb.velocity;
                }
                    
            }
        }

        else if(controller.m_state == RoombaState.PATROLING)
        {
            if (m_agent.isOnNavMesh)
            {
                return direction - m_rb.velocity;
            }
        }

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + direction * 10);

        if (waypoints.Count != 0)
        {
            foreach(Vector3 v in waypoints)
            {
                Gizmos.DrawSphere(v, 1);
            }
        }
    }


    void AddToList()
    {
        waypoints.Clear();

        posIndex = 0;

        for (int i = 0; i < path.corners.Length; i++)
        {

            waypoints.Add(path.corners[i]);
        }
    }

    bool IsAtLastPoint()
    {
        if(waypoints.Count > 0)
        {
            float temp = (transform.position - waypoints[waypoints.Count - 1]).magnitude;

            return temp < 0.5f;
        }

        return false;
        
    }

    void SetWaypoints(Vector3 target, float sizeOfCast)
    {
        if (NavMesh.SamplePosition(target, out NavMeshHit hit, sizeOfCast, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);

            taar.transform.position = hit.position;

            AddToList();
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        return randomDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.m_state == RoombaState.PATROLING)
        {
            if (IsAtLastPoint() || waypoints.Count == 0)
            {
                SetWaypoints(GetRandomPoint(), 0.5f);
            }
        }
        else if(controller.m_state == RoombaState.ATTACKING)
        {
            cooldown += Time.deltaTime;

            if(cooldown > 1 || waypoints.Count == 0)
            {
                SetWaypoints(player.transform.position, 2f);
                cooldown = 0;
            }
        }

        

        m_agent.nextPosition = transform.position;

        if(controller.m_state != lastState)
        {
            if(controller.m_state == RoombaState.PATROLING)
            {
                print("should get patrol");
                SetWaypoints(GetRandomPoint(), 0.5f);
                lastState = controller.m_state;
 
            }
            else if(controller.m_state == RoombaState.ATTACKING)
            {
                print("should get attack");
                SetWaypoints(player.transform.position, 2f); 
                lastState = controller.m_state;
            }
            else if(controller.m_state == RoombaState.ROTATING)
            {
                waypoints.Clear();
                lastState = controller.m_state;
            }
        }

    

        if (waypoints.Count != 0)
        {
            Vector3 dist = waypoints[posIndex] - transform.position;

            if (dist.magnitude < distanceToWaypoint)
            {

                if (posIndex < waypoints.Count - 1)
                {
                    posIndex++;

                    direction = (waypoints[posIndex] - transform.position).normalized * 10;
                }
                else
                {
                    direction = Vector3.zero;
                }
            }
        }
    }
}
