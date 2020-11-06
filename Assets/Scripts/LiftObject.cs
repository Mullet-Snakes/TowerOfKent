using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObject : MonoBehaviour
{
    public GameObject grabby;
    public GameObject player;
    private GameObject carriedObject;
    private GameObject mainCamera;
    private Camera playerCamera;
    private Rigidbody prop_rb;
    private Vector3 noGrav = new Vector3(0, 0, 0);
    private Vector3 grabPosition;
    private float distance;

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

    private bool isHolding = false;

    //public bool objStop;
    //public float stopSpeed;

    private void Awake()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        playerCamera = Camera.main;
    }

    private void Update()
    {
        grabPosition = mainCamera.transform.position + mainCamera.transform.forward * holdDistance;

        if (isHolding)
        {
            CheckDrop();
        }
        else
        {
            Pickup();
        }
    }

    private void FixedUpdate()
    {
        if(isHolding)
        {
            Carry(carriedObject);
        }
    }

    void Carry(GameObject obj)
    {
        distance = Vector3.Distance(obj.transform.position, player.transform.position);

        prop_rb = obj.GetComponent<Rigidbody>();
        prop_rb.AddForce((new Vector3(grabPosition.x, grabby.transform.position.y, grabPosition.z) - obj.transform.position) * objMove, ForceMode.VelocityChange);

        #region Slowing on arrival

        //Sets the velecity to 0 (Normal)
        //if (objStop)
        //{
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
        //}

        //Decellerates the velocity (spins oddly)
        //else if(!objStop)
        //{
        //    if (obj.transform.position.x <= grabPosition.x + orbitRange || obj.transform.position.x >= grabPosition.x - orbitRange)
        //    {
        //        prop_rb.velocity = new Vector3(prop_rb.velocity.x - (stopSpeed * prop_rb.velocity.x), prop_rb.velocity.y, prop_rb.velocity.z);
        //        prop_rb.angularVelocity = new Vector3(prop_rb.velocity.x - (stopSpeed * prop_rb.velocity.x), prop_rb.velocity.y, prop_rb.velocity.z);
        //    }

        //    if (obj.transform.position.y <= grabPosition.y + orbitRange || obj.transform.position.y >= grabPosition.y - orbitRange)
        //    {
        //        prop_rb.velocity = new Vector3(prop_rb.velocity.x, prop_rb.velocity.y - (stopSpeed * prop_rb.velocity.y), prop_rb.velocity.z);
        //        prop_rb.angularVelocity = new Vector3(prop_rb.velocity.x, prop_rb.velocity.y - (stopSpeed * prop_rb.velocity.y), prop_rb.velocity.z);
        //    }

        //    if (obj.transform.position.z <= grabPosition.z + orbitRange || obj.transform.position.z >= grabPosition.z - orbitRange)
        //    {
        //        prop_rb.velocity = new Vector3(prop_rb.velocity.x, prop_rb.velocity.y, prop_rb.velocity.z - (stopSpeed * prop_rb.velocity.z));
        //        prop_rb.angularVelocity = new Vector3(prop_rb.velocity.x, prop_rb.velocity.y, prop_rb.velocity.z - (stopSpeed * prop_rb.velocity.z));
        //    }
        //}
        #endregion
    }

    void Pickup()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, grabDistance))
            {
                prop_rb = hit.collider.GetComponent<Rigidbody>();
                distance = Vector3.Distance(hit.transform.position, player.transform.position);

                if (hit.collider.CompareTag("LevelProp"))
                {
                    isHolding = true;
                    carriedObject = prop_rb.gameObject;
                    //carriedObject.transform.parent = grabby.transform;
                    hit.collider.GetComponent<GravityController>().CurrentGravity = noGrav;
                }
            }
        }
    }

    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.I) || distance > grabDistance)
        {
            DropObject();
        }
    }

    void DropObject()
    {
        prop_rb.GetComponent<GravityController>().CurrentGravity = player.GetComponent<PlayerController>().Gravity;
        distance = 0;
        isHolding = false;
        //carriedObject.transform.parent = null;
        prop_rb = null;
        carriedObject = null;
    }
}
