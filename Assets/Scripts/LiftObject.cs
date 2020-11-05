using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObject : MonoBehaviour
{
    #region failed code
    //public Transform destination;
    //private Camera playerCamera;

    //void Awake()
    //{
    //    playerCamera = Camera.main;
    //}

    //private void Updat()
    //{
    //    if (Input.GetKeyDown(KeyCode.I) && !grabbed)
    //    {
    //        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
    //        RaycastHit hit;

    //        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, Mathf.Infinity))
    //        {
    //            if (hit.collider.CompareTag("LevelProp"))
    //            {
    //                grabbed = true;
    //                Rigidbody prop_rb = this.GetComponent<Rigidbody>();

    //                while (grabbed == true)
    //                {
    //                    //GetComponent<BoxCollider>().enabled = false;
    //                    hit.collider.GetComponent<GravityController>().GravityFactor = 0f;
    //                    this.transform.position = destination.position;

    //                    //Transform propTransform = prop_rb.transform;
    //                    //float elapsedTime = 0f;
    //                    //prop_rb.MovePosition(Vector3.Lerp(propTransform.position, destination.position, (elapsedTime / propMove)));
    //                    //elapsedTime += Time.deltaTime;

    //                    prop_rb.transform.parent = destination;

    //                    if (Input.GetKeyDown(KeyCode.O) && grabbed == true)
    //                    {
    //                        grabbed = false;
    //                        this.transform.parent = null;
    //                        this.GetComponent<GravityController>().GravityFactor = -9.8f;
    //                        //GetComponent<BoxCollider>().enabled = true;
    //                    }
    //                }
    //            }
    //        }
    //    }

    //}
    #endregion

    //public bool canHold = true;
    public GameObject player;
    private GameObject carriedObject;
    private GameObject mainCamera;
    private Camera playerCamera;
    private Rigidbody prop_rb;
    //private Vector3 objectPos;
    private Vector3 noGrav = new Vector3(0, 0, 0);

    [SerializeField]
    [Tooltip("The movement of the object you're holding (default 20)")]
    [Range(15, 30)]
    public float objMove = 0.5f;

    //[SerializeField]
    //[Tooltip("Default: 600")]
    //[Range(1, 1000)]
    //public float throwForce;

    //private float distance;

    [SerializeField]
    [Tooltip("The yoink range")]
    public float grabDistance;

    [SerializeField]
    [Tooltip("The distance between an object you're holding and you")]
    [Range(0, 5)]
    public float holdDistance;

    private bool isHolding = false;
    //private float elapsedTime = 0f;

    #region Bad Code
    //// Update is called once per frame
    //void pdate()
    //{
    //    distance = Vector3.Distance(levelProp.transform.position, tempParent.transform.position);
    //    grabDistance = player.GetComponent<PlayerController>().playerGrab;

    //    if (distance >= grabDistance)
    //    {
    //        elapsedTime = 0;
    //        isHolding = false;
    //    }

    //    //Check if isholding
    //    if (isHolding == true)
    //    {
    //        prop_rb = levelProp.GetComponent<Rigidbody>();
    //        Vector3 propPosition = prop_rb.position;

    //        prop_rb.velocity = Vector3.zero;
    //        prop_rb.angularVelocity = Vector3.zero;
    //        //levelProp.transform.position = grabby.transform.position;
    //        //levelProp.transform.rotation = grabby.transform.rotation;
    //        levelProp.transform.SetParent(tempParent.transform);
    //        //prop_rb.MovePosition(Vector3.Lerp(propPosition, tempParent.transform.position, (elapsedTime / moveTime)));
    //        elapsedTime += Time.deltaTime;

    //        if (Input.GetMouseButtonDown(1))
    //        {
    //            elapsedTime = 0;
    //            levelProp.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
    //            isHolding = false;
    //        }
    //    }
    //    else
    //    {
    //        elapsedTime = 0;
    //        objectPos = levelProp.transform.position;
    //        levelProp.transform.SetParent(null);
    //        this.GetComponent<GravityController>().CurrentGravity = player.GetComponent<PlayerController>().Gravity;
    //        levelProp.transform.position = objectPos;
    //    }
    //}

    //void OnMouseDown()
    //{
    //    if (distance <= grabDistance)
    //    {
    //        isHolding = true;
    //        this.GetComponent<GravityController>().CurrentGravity = noGrav;
    //        levelProp.GetComponent<Rigidbody>().detectCollisions = true;
    //    }
    //}
    //void OnMouseUp()
    //{
    //    isHolding = false;
    //    elapsedTime = 0;
    //}
    #endregion

    private void Awake()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        playerCamera = Camera.main;
    }

    private void Update()
    {
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
        //moveTime may be wrong
        obj.transform.position = Vector3.Lerp(obj.transform.position, mainCamera.transform.position + mainCamera.transform.forward * holdDistance, Time.deltaTime * objMove);
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

                if (hit.collider.CompareTag("LevelProp"))
                {
                    isHolding = true;
                    carriedObject = prop_rb.gameObject;
                    hit.collider.GetComponent<GravityController>().CurrentGravity = noGrav;
                    //prop_rb.isKinematic = true;
                }
            }
        }
    }

    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        isHolding = false;
        prop_rb.GetComponent<GravityController>().CurrentGravity = player.GetComponent<PlayerController>().Gravity;
        //prop_rb.isKinematic = false;
        prop_rb = null;
        carriedObject = null;
    }
}
