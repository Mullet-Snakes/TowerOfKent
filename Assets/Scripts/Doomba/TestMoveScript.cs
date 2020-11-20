using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMoveScript : MonoBehaviour
{
    NavMeshAgent agent;
    Transform body;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        body = transform.GetChild(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }

        }

        if(agent.desiredVelocity != Vector3.zero)
        {
            Vector3 direction = Vector3.RotateTowards(body.forward, agent.desiredVelocity, 5 * Time.deltaTime, 0.0f);

            body.rotation = Quaternion.LookRotation(direction, transform.up);
        }
   
    }
}
