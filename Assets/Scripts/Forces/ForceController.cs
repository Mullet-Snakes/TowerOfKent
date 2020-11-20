using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    public List<ForceScript> m_forceList;

    private Rigidbody m_rb = null;

    private Vector3 forceToApply = new Vector3();

    public bool freezable = false;

    public bool Freezable { get { return freezable; } }

    public bool frozen = false;

    public bool Frozen { get { return frozen; } }


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        forceToApply = Vector3.zero;

        foreach(ForceScript f in m_forceList)
        {
            forceToApply += f.GetForce();
        }

        m_rb.AddForce(forceToApply, ForceMode.VelocityChange);
    }

    public void FreezeConstraints(bool freezeContraints)
    {
        if (freezeContraints)
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
