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

    private RoombaState lastState = RoombaState.DEFAULT;

    public bool completedPath = true;

    public float wanderRange = 30f;

    public float attackTargetCooldown = 3f;
    public float patrolTargetCooldown = 1f;

    public GameObject taar;



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

        if (waypoints.Count == 0)
        {
            return Vector3.zero;
        }

        else if(controller.m_state == RoombaState.ATTACKING)
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
        

        //print("nothing");
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
            waypoints.Add(new Vector3(path.corners[i].x, transform.position.y, path.corners[i].z));
        }
    }

    private IEnumerator PatrolState()
    {

        YieldInstruction lowfps = new WaitForSeconds(0.1f);
        YieldInstruction patrolCooldown = new WaitForSeconds(patrolTargetCooldown);

        while (true)
        {
            if(completedPath && controller.IsGrounded)
            {
                Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
                randomDirection = new Vector3(randomDirection.x, 0, randomDirection.z);
                randomDirection = transform.TransformDirection(randomDirection);
                randomDirection += transform.position;

                if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 0.5f, NavMesh.AllAreas))
                {

                    NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);

                    taar.transform.position = hit.position;

                    AddToList();

                    completedPath = false;

                    yield return patrolCooldown;
                }
            }

            yield return lowfps;

        }
    }

    private IEnumerator AttackState()
    {
        YieldInstruction attackCooldown = new WaitForSeconds(attackTargetCooldown);

        while(true)
        {
            NavMeshHit hit;

            if (NavMesh.SamplePosition(player.transform.position, out hit, 2f, NavMesh.AllAreas))
            {
                NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);

                AddToList();
            }

            yield return attackCooldown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_agent.nextPosition = transform.position;

        if(controller.m_state != lastState)
        {
            if(controller.m_state == RoombaState.PATROLING)
            {
                StopCoroutine("AttackState");
                lastState = controller.m_state;
                StartCoroutine("PatrolState");
            }
            else if(controller.m_state == RoombaState.ATTACKING)
            {
                StopCoroutine("PatrolState");
                lastState = controller.m_state;
                StartCoroutine("AttackState");
                completedPath = true;
            }
            else if(controller.m_state == RoombaState.ROTATING)
            {
                waypoints.Clear();
                lastState = controller.m_state;
                StopCoroutine("PatrolState");
                StopCoroutine("AttackState");
                completedPath = true;
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
                    completedPath = true;
                    direction = Vector3.zero;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            //create a ray cast and set it to the mouses cursor position in game
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);

                NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path);

                AddToList();

            }
        }
    }
}
