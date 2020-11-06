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

    public Vector3 tar = new Vector3();

    float t = 0;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded)
            t += Time.deltaTime;

        if(t > 3)
        {
            SetTargetPosition();
            t = 0;
        }

        if(Mathf.Abs(Vector3.SqrMagnitude(tar - transform.position)) < 0.1)
        {
            SetTargetPosition();
            t = 0;
        }
    }

    private void OnDrawGizmos()
    {

    }

    void SetTargetPosition()
    {
        float angle = Random.Range(0, 360);
        Vector3 inFront = transform.forward * 5;
        float radius = 5;
        float x = inFront.x + radius * Mathf.Cos(angle);
        float z = inFront.z + radius * Mathf.Sin(angle);
        tar = new Vector3(x, transform.position.y, z);
        target.transform.position = tar;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.38f);

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
        
        if(isGrounded && !rotating)
        {
            Vector3 relativePos = tar - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.time);
        }
    }
}
