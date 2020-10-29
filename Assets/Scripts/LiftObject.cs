﻿using System.Collections;
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

    float throwForce = 600;
    Vector3 objectPos;
    float distance;

    public bool canHold = true;
    public bool isHolding = false;
    public GameObject levelProp;
    public GameObject tempParent;
    public GameObject throwPoint;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(levelProp.transform.position, tempParent.transform.position);

        if (distance >= 1f)
        {
            isHolding = false;
        }

        //Check if isholding
        if (isHolding == true)
        {
            levelProp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            levelProp.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            levelProp.transform.SetParent(tempParent.transform);

            if (Input.GetMouseButtonDown(1))
            {
                levelProp.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }
        }
        else
        {
            objectPos = levelProp.transform.position;
            levelProp.transform.SetParent(null);
            this.GetComponent<GravityController>().GravityFactor = -9.8f;
            levelProp.transform.position = objectPos;
        }
    }

    void OnMouseDown()
    {
        if (distance <= 1f)
        {
            isHolding = true;
            this.GetComponent<GravityController>().GravityFactor = 0;
            levelProp.GetComponent<Rigidbody>().detectCollisions = true;
        }
    }
    void OnMouseUp()
    {
        isHolding = false;
    }
}