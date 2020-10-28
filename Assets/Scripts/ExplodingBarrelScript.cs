using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrelScript : MonoBehaviour
{
    private Rigidbody m_rb = null;
    private Vector3 lastVel = new Vector3();

    [SerializeField]
    [Tooltip("Default: 20")]
    [Range(0,50)]
    public float explodeSpeed = 20f; 

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rb.velocity == Vector3.zero)
        {
            if (Vector3.Magnitude(lastVel - m_rb.velocity) > explodeSpeed)
            {
                print("exploding");
            }
        }

        lastVel = m_rb.velocity;
    }
}
