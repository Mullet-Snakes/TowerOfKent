using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rb = null;

    [Tooltip("Default: Ground")]
    public LayerMask groundMask;

    private bool isGrounded = false;

    [Tooltip("Default: 10")]
    [Range(1, 30)]
    public float m_groundSpeed = 10;

    [Tooltip("Default: 1.5")]
    [Range(1, 5)]
    public float m_dashMultiplier;

    [Tooltip("Default: 0.1")]
    [Range(0, 1)]
    public float m_inAirMovementSpeed = 0.1f;

    private bool rotating = false;

    private Vector3 m_gravity = new Vector3();

    private Vector3 gravityVelocity = new Vector3();

    private Camera m_camera = null;

    private bool allowLookScript = true;

    private Vector3 m_input = new Vector3();

    private Vector3 targetVelocity = new Vector3();

    public Vector3 spawnPosition = new Vector3();

    public Vector3 SpawnPosition { set { spawnPosition = value; } }


    [Tooltip("Default: 3")]
    [Range(1, 1000)]
    public float m_jumpSpeed = 3;

    [Tooltip("Default: 3")]
    [Range(0, 10)]
    public float m_rotationSpeed = 3;

    private CameraController cameraControllerScript = null;

    private float x = 0f;
    private float z = 0f;

    private bool dashing = false;

    [SerializeField]
    [Tooltip("Default: -9.8")]
    [Range(-30, 0)]
    private float gravityFactor = -9.8f;

    private float dotProduct = 0f;
    private Vector3 forward = new Vector3();
    private Vector3 right = new Vector3();
    private Quaternion targetRot = new Quaternion();

    [SerializeField]
    [Tooltip("Default: 60")]
    [Range(0, 360)]
    private float normalFOV = 60f;

    [SerializeField]
    [Tooltip("Default: 70")]
    [Range(0, 360)]
    private float dashingFOV = 70f;

    [SerializeField]
    [Tooltip("Default: 0.25")]
    [Range(0, 2)]
    private float timeToChangeFOV = 0.25f;

    private bool cameraMoving = false;

    public Vector3 Gravity
    {
        get
        {
            return m_gravity;
        }

        set
        {
            gravityVelocity.Set(0, 0, 0);

            m_gravity = value * gravityFactor;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Physics.gravity = Vector3.zero;

        m_rb = GetComponent<Rigidbody>();

        m_camera = Camera.main.GetComponent<Camera>();

        cameraControllerScript = m_camera.GetComponent<CameraController>();
    }

    // Start is called before the first frame update
    void Start()
    {

        m_gravity = GravityManager.worldGravity;

        m_camera.fieldOfView = normalFOV;
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.C))
        {
            OnDeath();
        }

        if (allowLookScript)
        {
            cameraControllerScript.CameraMove();
        }

        allowLookScript = true;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        m_input = (transform.right * x + transform.forward * z).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            m_rb.AddForce(-m_jumpSpeed * m_gravity.normalized, ForceMode.Impulse);
        }

        if (Input.GetButton("Shift") && isGrounded && m_input != Vector3.zero)
        {
            dashing = true;         
        }

        if(dashing && !cameraMoving)
        {
            StartCoroutine(ChangeCameraFOV(timeToChangeFOV, m_camera.fieldOfView, dashingFOV));
        }
        else if(!dashing && !cameraMoving)
        {
            StartCoroutine(ChangeCameraFOV(timeToChangeFOV, m_camera.fieldOfView, normalFOV));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.03f, groundMask);

        targetVelocity = m_rb.velocity;

        if (isGrounded && !rotating)
        {
            targetVelocity = Vector3.zero;

            if (m_input != Vector3.zero)
            {
                targetVelocity += dashing ? m_input * m_groundSpeed * m_dashMultiplier : m_input * m_groundSpeed;
            }

            if (hit.rigidbody != null)
            {
                targetVelocity += hit.rigidbody.velocity;
            }
        }

        if (!isGrounded && !rotating)
        {
            targetVelocity += ((m_input * m_groundSpeed) * m_inAirMovementSpeed) * Time.deltaTime;
        }

        if (!isGrounded)
        {
            gravityVelocity += m_gravity * Time.deltaTime;
        }

        if (isGrounded && !rotating)
        {
            gravityVelocity.Set(0, 0, 0);
        }

        targetVelocity += gravityVelocity * Time.deltaTime;

        m_rb.AddForce(targetVelocity - m_rb.velocity, ForceMode.VelocityChange);

        dotProduct = Vector3.Dot(Vector3.Normalize(Gravity), -transform.up);

        dashing = false;

        if (dotProduct < 0.995f)
        {
            cameraControllerScript.MovingPlayer = false;
            rotating = true;
            right = Vector3.Cross(-Gravity, m_camera.transform.forward);
            forward = Vector3.Cross(right, -Gravity);
            targetRot = Quaternion.LookRotation(forward, -Gravity);
            m_rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, m_rotationSpeed));

        }
        else if (rotating)
        {
            right = Vector3.Cross(-Gravity, m_camera.transform.forward);
            forward = Vector3.Cross(right, -Gravity);
            targetRot = Quaternion.LookRotation(forward, -Gravity);
            m_rb.MoveRotation(targetRot);
            rotating = false;
            allowLookScript = false;
            cameraControllerScript.MovingPlayer = true;
        }
    }

    private IEnumerator ChangeCameraFOV(float totalTime, float currentFOV, float desiredFOV)
    {
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            cameraMoving = true;
            m_camera.fieldOfView =  (Mathf.Lerp(currentFOV, desiredFOV, (elapsedTime / totalTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraMoving = false;
    }

    public void OnDeath()
    {
        transform.position = spawnPosition;
    }
}
