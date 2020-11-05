using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMovement : MonoBehaviour
{
    private Rigidbody m_rb = null;

    public float m_speed;

    public bool isGrounded = false;

    public bool rotating = false;

    public Vector3 moveDirection = new Vector3();

    public float movementJitter = 5f;
    public float radius = 2f;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        moveDirection
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetMoveDirection()
    {
        Vector3 forward = transform.forward * movementJitter;
        float randDeg = Random.Range(0, 360);
        float x = forward.x + radius * Mathf.Cos(randDeg);
        float y = forward.y + radius * Mathf.Sin(randDeg);

    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.01f);

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

    }
}
