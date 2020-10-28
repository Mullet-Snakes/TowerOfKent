using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrelScript : MonoBehaviour
{
    private Rigidbody m_rb = null;
    private Vector3 lastVel = new Vector3();

    public Material mat = null;
    public float radius = 10f;

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
                ExplodeBarrel();
            }
        }

        lastVel = m_rb.velocity;
    }

    private void ExplodeBarrel()
    {
        LayerMask mask = LayerMask.GetMask("Wall");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer != 8)
            {
                print(hitCollider.gameObject.name);
                hitCollider.GetComponent<Renderer>().material = mat;
            }
                
        }
    }
}
