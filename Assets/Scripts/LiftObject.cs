using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObject : MonoBehaviour
{
    private bool isHolding = false;
    private GameObject carriedObject;
    private GameObject mainCamera;
    private Camera playerCamera;
    private Rigidbody prop_rb;

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
                    prop_rb.isKinematic = true;
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
        prop_rb.isKinematic = false;
        prop_rb = null;
        carriedObject = null;
    }
}
