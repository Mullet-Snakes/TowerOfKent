using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

    private Vector3 currentGravity = new Vector3();
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

    public Vector3 CurrentGravity
    {
        get
        {
            return currentGravity;
        }
        set
        {
            currentGravity = value;
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
        m_rb.AddForce(currentGravity * Time.deltaTime, ForceMode.VelocityChange);
    }

    void SetCurrentGravity(Vector3 grav, bool changingTargeted)
    {

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
