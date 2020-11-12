using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce : ForceScript
{

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

    public Vector3 Force
    {
        get
        {
            return force.normalized;
        }
        set
        {
            force = value;
        }
    }

    public override Vector3 GetForce()
    {
        return force * Time.deltaTime;
    }

    private void OnEnable()
    {
        GravityManager.GravityChange += SetCurrentGravity;
    }

    private void OnDisable()
    {
        GravityManager.GravityChange -= SetCurrentGravity;
    }



    void SetCurrentGravity(Vector3 grav, bool changingTargeted)
    {

        if (changingTargeted)
        {
            if (isTargeted)
            {
                force = grav * gravityFactor;
            }
        }
        else
        {
            if (!isTargeted)
            {
                force = grav * gravityFactor;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        force = GravityManager.worldGravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargeted)
        {
            transform.GetComponentInChildren<Renderer>().material = highlighed;
        }
        else
        {
            transform.GetComponentInChildren<Renderer>().material = normal;
        }
    }
}
