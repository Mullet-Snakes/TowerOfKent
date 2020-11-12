using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrelScript : MonoBehaviour
{
    public GameObject spawner;
    private Rigidbody m_rb = null;
    private Vector3 lastVel = new Vector3();

    private bool isExplodable = true;
    public bool IsExplodable
    {
        get
        {
            return isExplodable;
        }
        set
        {
            isExplodable = value;
        }
    }

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
    private float triggerSpeed = 20f;

    [SerializeField]
    [Tooltip("Default: 5")]
    [Range(0, 30)]
    private float upExplosionModifier = 5f;

    public ParticleSystem m_particles = null;

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
        if (lastVel != Vector3.zero)
        {
            if (Vector3.Magnitude(lastVel - m_rb.velocity) > triggerSpeed && isExplodable)
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
                hitCollider.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, transform.position, explosionRadius, upExplosionModifier, 
                    ForceMode.VelocityChange);
            }               
        }

        Instantiate(m_particles, transform.position, transform.rotation);

        //spawner.GetComponent<BarrelSpawn>().Explode();
        Destroy(gameObject);
    }
}
