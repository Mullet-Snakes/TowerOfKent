using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObject : MonoBehaviour
{
    public GameObject grabby;
    public GameObject player;
    private Rigidbody player_rb;
    private Collider player_collider;
    private GameObject carriedObject;
    private GameObject mainCamera;
    private Camera playerCamera;
    private Rigidbody prop_rb;
    private Vector3 noGrav = new Vector3(0, 0, 0);
    private Vector3 grabPosition;
    private float distance;
    private float objMass = 0;

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

    //private Vector3 spin;

    private bool isHolding = false;

    //public bool objStop;
    //public float stopSpeed;

    private void Awake()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        playerCamera = Camera.main;
        player_rb = player.GetComponent<Rigidbody>();
        player_collider = player.GetComponentInChildren<Collider>();
        //spin = new Vector3(0, 100, 0);
    }

    private void Update()
    {
        grabPosition = /*grabby.transform.position */mainCamera.transform.position + mainCamera.transform.forward * holdDistance;

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

        ////New attempt
        //else if (!objStop)
        //{
        //    if (obj.transform.position.x <= grabPosition.x + orbitRange || obj.transform.position.x >= grabPosition.x - orbitRange)
        //    {
        //        prop_rb.MovePosition(Vector3.Lerp(prop_rb.position, grabPosition, (objMove / 10) * Time.deltaTime));
        //    }
        //    if (obj.transform.position.y <= grabPosition.y + orbitRange || obj.transform.position.y >= grabPosition.y - orbitRange)
        //    {
        //        prop_rb.MovePosition(Vector3.Lerp(prop_rb.position, grabPosition, (objMove / 10) * Time.deltaTime));
        //    }
        //    if (obj.transform.position.z <= grabPosition.z + orbitRange || obj.transform.position.z >= grabPosition.z - orbitRange)
        //    {
        //        prop_rb.MovePosition(Vector3.Lerp(prop_rb.position, grabPosition, (objMove / 10) * Time.deltaTime));
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
                    hit.collider.GetComponent<GravityController>().CurrentGravity = noGrav;

                    // Disable collision with player
                    //Physics.IgnoreCollision(hit.collider, player_collider, true);
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
        // Reenable collision with player
        //Physics.IgnoreCollision(prop_rb.GetComponent<Collider>(), player_collider, false);

        prop_rb.GetComponent<GravityController>().CurrentGravity = player.GetComponent<PlayerController>().Gravity;
        distance = 0;

        isHolding = false;
        prop_rb = null;
        carriedObject = null;
    }
}
