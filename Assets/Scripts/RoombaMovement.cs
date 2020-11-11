using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum RoombaState
{
    DEFAULT,
    PATROLING,
    ATTACKING,
    ROTATING

};

public class RoombaMovement : MonoBehaviour
{
    private Rigidbody m_rb = null;

    public float m_speed;

    public bool isGrounded = false;

    public bool rotating = false;

    public LayerMask floor;

    public NavMeshAgent m_agent = null;

    private GameObject player;

    

    public RoombaState m_state = RoombaState.DEFAULT;



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
        player = GameObject.FindGameObjectWithTag("Player");
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

        float dot = Vector3.Dot(transform.up, player.transform.up);

        if(!rotating)
        {
            if (dot > 0.8f)
            {
                if ((transform.position - player.transform.position).magnitude <= 10)
                {
                    m_state = RoombaState.ATTACKING;
                }
                else
                {
                    m_state = RoombaState.PATROLING;
                }
            }
            else
            {
                m_state = RoombaState.PATROLING;
            }
        }
        

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
            m_state = RoombaState.ROTATING;

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
            m_state = RoombaState.PATROLING;
            m_agent.enabled = true;
        }
    }
}
