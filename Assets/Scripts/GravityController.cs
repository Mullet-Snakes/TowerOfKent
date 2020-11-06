using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

    private Vector3 currentGravity = new Vector3();

    private Vector3 gravVel = new Vector3();

    public Vector3 CurrentGravity
    {
        get
        {
            return currentGravity.normalized;
        }
    }
    private Rigidbody m_rb;
    private bool isTargeted = false;

    public bool IsTargeted
    {
        get
        {
            return isTargeted;
        }
        set
        {
            isTargeted = value;
        }
    }

    public Material highlighed;
    public Material normal;

    [SerializeField]
    [Tooltip("Default: -9.8")]
    [Range(-30, 0)]
    private float gravityFactor = -9.8f;

    private bool frozen = false;

    public bool Frozen
    {
        get
        {
            return frozen;
        }
    }

    public float GravityFactor
    {
        get
        {
            return gravityFactor;
        }
        set
        {
            gravityFactor = value;
        }
    }

    private void OnEnable()
    {
        GravityManager.GravityChange += SetCurrentGravity;
    }

    private void OnDisable()
    {
        GravityManager.GravityChange -= SetCurrentGravity;
    }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGravity = GravityManager.worldGravity;
    }

    private void Update()
    {
        if (isTargeted)
        {
            transform.GetComponent<Renderer>().material = highlighed;
        }
        else
        {
            transform.GetComponent<Renderer>().material = normal;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(transform.GetComponent<RoombaMovement>() != null)
        {
            Vector3 targetVelocity = m_rb.velocity;

            if(transform.GetComponent<RoombaMovement>().isGrounded && !transform.GetComponent<RoombaMovement>().rotating)
            {
                targetVelocity = Vector3.zero;

                targetVelocity += (transform.GetComponent<RoombaMovement>().target.transform.position - transform.position).normalized * transform.GetComponent<RoombaMovement>().m_speed;
                //targetVelocity += transform.forward * transform.GetComponent<RoombaMovement>().m_speed;
            }

            if(!transform.GetComponent<RoombaMovement>().isGrounded || transform.GetComponent<RoombaMovement>().rotating)
            {
                gravVel += currentGravity * Time.deltaTime;
            }

            if (transform.GetComponent<RoombaMovement>().isGrounded && !transform.GetComponent<RoombaMovement>().rotating)
            {
                gravVel.Set(0, 0, 0);
            }

            targetVelocity += gravVel * Time.deltaTime;

            m_rb.AddForce(targetVelocity - m_rb.velocity, ForceMode.VelocityChange);

        }

        else
        {
            m_rb.AddForce(currentGravity * Time.deltaTime, ForceMode.VelocityChange);
        }
        

    }

    public void FreezeConstraints(bool freezeContraints)
    {
        if(freezeContraints)
        {
            m_rb.constraints = RigidbodyConstraints.FreezeAll;
            frozen = true;
        }
        else
        {
            m_rb.constraints = RigidbodyConstraints.None;
            frozen = false;
        }
        
    }

    void SetCurrentGravity(Vector3 grav, bool changingTargeted)
    {
        gravVel = Vector3.zero;

        if(changingTargeted)
        {
            if(isTargeted)
            {
                currentGravity = grav * gravityFactor;
            }
        }
        else
        {
            if(!isTargeted)
            {
                currentGravity = grav * gravityFactor;
            }
        }
    }
}
