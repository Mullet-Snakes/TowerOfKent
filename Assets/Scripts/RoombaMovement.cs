using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMovement : MonoBehaviour
{
    private Rigidbody m_rb = null;

    public float m_speed;

    public bool isGrounded = false;

    public bool rotating = false;

    public GameObject target = null;

    public GameObject offset = null;
    public Vector3 tar = new Vector3();
    public Vector3 unit = new Vector3();

    float t = 0;


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
        t += Time.deltaTime;

        if(t > 2)
        {
            GetTarget();

             t = 0;
        }
    }

    Vector3 GetTarget()
    {
        unit = Random.onUnitSphere;
        unit = new Vector3(unit.x, 0, unit.z).normalized * 3;
        target.transform.position = transform.position + transform.TransformDirection(unit);

        return transform.position + transform.TransformDirection(unit);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.51f);

        Vector3 right = new Vector3();
        Vector3 forward = new Vector3();
        Vector3 g = transform.GetComponent<GravityController>().CurrentGravity;
        Quaternion targetRot = new Quaternion();
        float m_rotationSpeed = 2f;
        float dotProduct = Vector3.Dot(Vector3.Normalize(g), -transform.up);

        if (dotProduct < 0.995f)
        {
            rotating = true;
            right = Vector3.Cross(-g, transform.forward);
            forward = Vector3.Cross(right, -g);
            targetRot = Quaternion.LookRotation(forward, -g);
            m_rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, m_rotationSpeed));

        }
        else if (rotating)
        {
            right = Vector3.Cross(-g, transform.forward);
            forward = Vector3.Cross(right, -g);
            targetRot = Quaternion.LookRotation(forward, -g);
            m_rb.MoveRotation(targetRot);
            rotating = false;
        }
        else if(isGrounded && !rotating)
        {
            //find the vector pointing from our position to the target
            Vector3 _direction = (unit - transform.position).normalized;
            print(_direction);

            //create the rotation we need to be in to look at the target
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            m_rb.MoveRotation(Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 10));
        }
   
    }
}
