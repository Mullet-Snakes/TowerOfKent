using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gravity;

public class LiftObject : MonoBehaviour
{
    public GameObject grabby;
    public GameObject player;
    private Rigidbody player_rb;
    private GameObject mainCamera;
    private Camera playerCamera;
    private Rigidbody prop_rb;
    private Vector3 noGrav = new Vector3(0, 0, 0);
    private Vector3 objGrav = new Vector3(0, 0, 0);
    private Vector3 grabPosition;
    private float distance;

    private GameObject carriedObject;
    public GameObject CarriedObject
    {
        get
        {
            return carriedObject;
        }
    }

    [SerializeField]
    [Tooltip("A layer the object-finding raycast ignores. Should be the same Layer as the players collider")]
    public LayerMask ignoreMe;

    [SerializeField]
    [Tooltip("The movement of the object you're holding (Default 10)")]
    [Range(5, 15)]
    public float objMove = 10;

    [SerializeField]
    [Tooltip("The range where held objects slow down (Default 4)")]
    [Range(1, 5)]
    public float orbitRange = 4;

    [SerializeField]
    [Tooltip("The yoink range (Default 10)")]
    public float grabDistance;

    [SerializeField]
    [Tooltip("The distance between an object you're holding and you (Default 3)")]
    [Range(0, 5)]
    public float holdDistance;

    [SerializeField]
    [Tooltip("The power of your throw (Default 5)")]
    [Range(0, 10)]
    public float throwForce;

    //private Vector3 spin;

    [SerializeField]
    private bool isHolding = false;
    public bool IsHolding
    {
        get
        {
            return isHolding;
        }
    }

    //public bool objStop;
    //public float stopSpeed;

    private void Awake()
    {
        
        mainCamera = GameObject.FindWithTag("MainCamera");
        playerCamera = Camera.main;
        player_rb = player.GetComponent<Rigidbody>();
        //spin = new Vector3(0, 100, 0);
    }

    private void OnEnable()
    {
        InteractKeyManager.OnButtonPress += Pickup;
    }
    private void OnDisable()
    {
        InteractKeyManager.OnButtonPress -= Pickup;
    }


    private void Update()
    {
        grabPosition = mainCamera.transform.position + mainCamera.transform.forward * holdDistance;

        if (isHolding)
        {
            //CheckDrop();
        }
        else
        {
            //Pickup(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Check that we can "see" the grabPosition & nothing is blocking it
        RaycastHit hitInfo;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, holdDistance, LayerMask.GetMask("Ground")))
        {
            grabPosition = hitInfo.point;
        }

        if (isHolding)
        {
            Carry(carriedObject);
        }
    }

    void Carry(GameObject obj)
    {
        distance = Vector3.Distance(obj.transform.position, player.transform.position);

        #region Carmine's code
        //prop_rb = obj.GetComponent<Rigidbody>();
        //prop_rb.MovePosition(Vector3.Lerp(prop_rb.position, grabPosition, objMove * Time.deltaTime));
        //prop_rb.rotation = grabby.transform.rotation;
        #endregion


        prop_rb = obj.GetComponent<Rigidbody>();
        prop_rb.AddForce((new Vector3(grabPosition.x, grabby.transform.position.y, grabPosition.z) - obj.transform.position) * objMove, ForceMode.VelocityChange);
        //prop_rb.rotation = grabby.transform.rotation;

        #region Slowing on arrival

        //Sets the velecity to 0 (Normal)
        if (obj.transform.position.x <= grabPosition.x + orbitRange || obj.transform.position.x >= grabPosition.x - orbitRange)
        {
            prop_rb.velocity = new Vector3(0, prop_rb.velocity.y, prop_rb.velocity.z);
            prop_rb.angularVelocity = new Vector3(0, prop_rb.velocity.y, prop_rb.velocity.z);

        }

        if (obj.transform.position.y <= grabPosition.y + orbitRange || obj.transform.position.y >= grabPosition.y - orbitRange)
        {
            prop_rb.velocity = new Vector3(prop_rb.velocity.x, 0, prop_rb.velocity.z);
            prop_rb.angularVelocity = new Vector3(prop_rb.velocity.x, 0, prop_rb.velocity.z);
        }

        if (obj.transform.position.z <= grabPosition.z + orbitRange || obj.transform.position.z >= grabPosition.z - orbitRange)
        {
            prop_rb.velocity = new Vector3(prop_rb.velocity.x, prop_rb.velocity.y, 0);
            prop_rb.angularVelocity = new Vector3(prop_rb.velocity.x, prop_rb.velocity.y, 0);
        }
        #endregion
    }

    void Pickup(GameObject go)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(x, y));
        RaycastHit hit;
        //print("Casting");

        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, grabDistance, ~ignoreMe))
        {
            prop_rb = hit.collider.GetComponent<Rigidbody>();
            distance = Vector3.Distance(hit.transform.position, player.transform.position);
            //print("Colliding");

            if (hit.collider.CompareTag("LevelProp"))
            {
                //print("Grabbing");
                isHolding = true;
                carriedObject = prop_rb.gameObject;
                objGrav = hit.collider.GetComponent<GravityForce>().Force;
                hit.collider.GetComponent<GravityForce>().Force = noGrav;

                if (carriedObject.GetComponent<ExplodingBarrelScript>())
                {
                    if (hit.collider.GetComponent<ExplodingBarrelScript>().IsExplodable == true)
                    {
                        hit.collider.GetComponent<ExplodingBarrelScript>().IsExplodable = false;
                    }
                }
            }
            //else
            //{
            //    print(hit.collider.tag);
            //}
        }
    }

    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.E) || distance > grabDistance)
        {
            DropObject();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowObject();
        }
    }

    public void DropObject()
    {
        prop_rb.GetComponent<GravityForce>().Force = objGrav * 9.8f;
        distance = 0;

        if (carriedObject.GetComponent<ExplodingBarrelScript>())
        {
            if (carriedObject.GetComponent<ExplodingBarrelScript>().IsExplodable == false)
            {
                carriedObject.GetComponent<ExplodingBarrelScript>().IsExplodable = true;
            }
        }

        isHolding = false;
        prop_rb = null;
        carriedObject = null;
    }

    void ThrowObject()
    {
        prop_rb.GetComponent<GravityForce>().Force = objGrav * 9.8f;
        distance = 0;
        prop_rb.AddForce((grabby.transform.position - player.transform.position) * throwForce, ForceMode.VelocityChange);

        if (carriedObject.GetComponent<ExplodingBarrelScript>())
        {
            if (carriedObject.GetComponent<ExplodingBarrelScript>().IsExplodable == false)
            {
                carriedObject.GetComponent<ExplodingBarrelScript>().IsExplodable = true;
            }
        }

        isHolding = false;
        prop_rb = null;
        carriedObject = null;
    }
}
