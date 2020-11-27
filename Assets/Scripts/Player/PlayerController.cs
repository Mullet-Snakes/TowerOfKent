using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private static PlayerController _instance;

    public static PlayerController Instance { get { return _instance; } }


    private Rigidbody m_rb = null;

    [Tooltip("Default: Ground")]
    public LayerMask groundMask;

    public LayerMask objectMask;

    private bool isGrounded = false;

    private bool isOnProp = false;

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

    public Animator m_animator = null;

    [Tooltip("Default: 0.05")]
    [Range(0, 1)]
    public float animationTime = .05f;

    private float playerSpeed = 0f;

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

    public GameObject door = null;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            print("to destroy");
            Destroy(gameObject);
        }
    }

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            
        }
        //DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(transform.root.gameObject);

        Physics.gravity = Vector3.zero;

        m_rb = GetComponent<Rigidbody>();

        m_camera = Camera.main.GetComponent<Camera>();

        cameraControllerScript = m_camera.GetComponent<CameraController>();

        m_animator = m_camera.transform.GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_gravity = GravityManager.worldGravity;

        m_camera.fieldOfView = normalFOV;
    }

    private void Update()
    {

        if (isOnProp && m_input == Vector3.zero)
        {
            playerSpeed = 0;
        }
        else
        {
            playerSpeed = Mathf.Lerp(playerSpeed, m_rb.velocity.magnitude, animationTime);
        }

        m_animator.SetFloat("Speed", playerSpeed);

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
        //isGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.03f, groundMask);

        isGrounded = Physics.SphereCast(transform.position, 0.5f, -transform.up, out RaycastHit hit, 0.53f, groundMask);

        targetVelocity = m_rb.velocity;

        if(isGrounded)
        {
            isOnProp = hit.collider.gameObject.IsInLayerMask(objectMask);
        }
            

        if (!isGrounded)
        {
            isOnProp = false;

            gravityVelocity += m_gravity * Time.deltaTime;

            if (!rotating)
            {
                targetVelocity += ((m_input * m_groundSpeed) * m_inAirMovementSpeed) * Time.deltaTime;
            }
        }

        if (isGrounded && !rotating)
        {
            targetVelocity = Vector3.zero;

            if (m_input != Vector3.zero)
            {
                targetVelocity += dashing ? m_input * m_groundSpeed * m_dashMultiplier : m_input * m_groundSpeed;
            }

            gravityVelocity.Set(0, 0, 0);

            if (hit.rigidbody != null)
            {
                targetVelocity += hit.rigidbody.velocity;
            }
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

    public void OnDeath(Vector3 respawnPos)
    {
        m_rb.velocity = Vector3.zero;
        transform.position = respawnPos;
        //transform.position = spawnPosition;
    }
}
