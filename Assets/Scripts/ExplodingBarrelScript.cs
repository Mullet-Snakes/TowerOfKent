using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrelScript : MonoBehaviour
{
    private Rigidbody m_rb = null;
    private Vector3 lastVel = new Vector3();

    [SerializeField]
    [Tooltip("Default: 10")]
    [Range(0,30)]
    private float explosionRadius = 10f;

    [SerializeField]
    [Tooltip("Default: 50")]
    [Range(0,200)]
    private float explosiveForce = 50;

    [SerializeField]
    [Tooltip("Default: 20")]
    [Range(0,50)]
    private float explodeSpeed = 20f;

    [SerializeField]
    [Tooltip("Default: 10")]
    [Range(0, 30)]
    private float upwardsExplosionModifier = 10f;

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

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("LevelProp"))
            {
                hitCollider.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, transform.position, explosionRadius, upwardsExplosionModifier, 
                    ForceMode.VelocityChange);
            }               
        }
    }
}
