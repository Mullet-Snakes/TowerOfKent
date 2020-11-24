using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlayerScript : MonoBehaviour
{
    public LayerMask layerMask;

    [Range(0,100)]
    [SerializeField]
    private float distanceToTrigger = 10f;

    [Range(0, 1)]
    [SerializeField]
    private float dotToTrigger = 0.8f;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool CheckIfAttacking(GameObject target)
    {
        Vector3 dist = target.transform.position - transform.position;

        if (dist.magnitude < distanceToTrigger)
        {
            if (Physics.Raycast(transform.position, dist, out RaycastHit hit, dist.magnitude, ~layerMask))
            {
                if(hit.transform.CompareTag("GravityWall"))
                {
                    return false;
                }
            }

            float dot = Vector3.Dot(target.transform.up, transform.up);

            if (dot > dotToTrigger)
            {
                return true;
            }
        }

        return false;
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
