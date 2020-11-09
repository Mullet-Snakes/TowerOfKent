using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    public List<ForceScript> m_forceList;

    private Rigidbody m_rb = null;

    private Vector3 forceToApply = new Vector3();

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
