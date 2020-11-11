using System.Collections;
using System.Collections.Generic;
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

    private List<Vector3> waypoints = new List<Vector3>();

    private GameObject player;

    float t = 0f;

    private void Awake()
    {
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
        if ((transform.position - player.transform.position).magnitude <= 10 && m_agent.isOnNavMesh)
        {
            return Vector3.zero - m_rb.velocity;
        }
        else if (m_agent.isOnNavMesh)
        {
            return direction - m_rb.velocity;
        }

        return Vector3.zero;
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

    // Update is called once per frame
    void Update()
    {
        m_agent.nextPosition = transform.position;

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

        t += Time.deltaTime;

        if(t > 3)
        {
            float dot = Vector3.Dot(transform.up, player.transform.up);

            if (dot > 0.8f)
            {
                NavMeshHit hit;

                if (NavMesh.SamplePosition(player.transform.position, out hit, 2, NavMesh.AllAreas))
                {
                    NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);

                    AddToList();
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
