using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Gravity;

public enum RoombaState
{
    DEFAULT,
    PATROLING,
    ATTACKING,
    ROTATING

};

public class RoombaController : MonoBehaviour
{
    private Rigidbody m_rb = null;

    public float m_speed;

    private bool isGrounded = false;

    public bool IsGrounded { get { return isGrounded; } }

    private bool rotating = false;

    public bool Rotating { get { return rotating; } }

    public LayerMask floor;

    private NavMeshAgent m_agent = null;

    private GameObject player;

    private float distToPlayer = 0f;

    public float DistToPlayer { get { return distToPlayer; } }

    public RoombaState m_state = RoombaState.DEFAULT;

    public float distanceToAttack = 20f;

    private Vector3 target;

    public Vector3 Target { set{ target = value; } }

    private Transform body = null;


    private float delay = 0f;

    public Vector3 gra;

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
        body = transform.GetChild(0);
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
        m_rb.AddForce(transform.up * 2, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position + transform.up * player.transform.GetComponentInChildren<CapsuleCollider>().height / 2, player.transform.position);
    }



    // Update is called once per frame
    void Update()
    {
        

        if (!rotating)
        {
            float dot = Vector3.Dot(transform.up, player.transform.up);

            if (dot > 0.9f)
            {
                distToPlayer = (transform.position - player.transform.position).magnitude;

                if (distToPlayer <= distanceToAttack)
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
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.51f);

        Vector3 right;
        Vector3 forward;
        Vector3 g = transform.GetComponent<GravityForce>().GetForce().normalized;
        Quaternion targetRot;
        float m_rotationSpeed = 4f;
        float dotProduct = Vector3.Dot(Vector3.Normalize(g), -transform.up);

        gra = g;

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
            if(m_agent.enabled == false)
            {
                delay += Time.deltaTime;
            }
            

            if(delay > 1f & m_agent.enabled == false)
            {
                m_agent.enabled = true;
                delay = 0f;
            }

            Vector3 direction = Vector3.RotateTowards(body.forward, target, m_speed * Time.deltaTime, 0.0f);

            body.rotation = Quaternion.LookRotation(direction, -g);

        }


    }
}
