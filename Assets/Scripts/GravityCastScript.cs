using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCastScript : MonoBehaviour
{
    private PlayerController playerControllerScript = null;

    [Tooltip("List of walls")]
    public List<GameObject> levelWalls;


    [Tooltip("Highlighted wall material")]
    public Material highlightedWallMaterial = null;

    [Tooltip("Default wall material")]
    public Material defaultWallMaterial = null;

    private GravityController targeted = null;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.forward * 50, Color.red);


        if (Input.GetKeyDown(KeyCode.E))
        {

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
            {

                hit.transform.GetComponent<Renderer>().material = highlightedWallMaterial;

                foreach (GameObject go in levelWalls)
                {
                    if (go != hit.transform.gameObject)
                    {
                        go.GetComponent<Renderer>().material = defaultWallMaterial;
                    }
                }

                playerControllerScript.Gravity = hit.normal;

            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("GravityWall"))
                {
                    GravityManager.ChangeGrav(hit.normal, false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.GetComponent<GravityController>() != null)
                {
                    if(targeted != null)
                    {
                        targeted.IsTargeted = false;

                        if(targeted.gameObject == hit.transform.gameObject)
                        {
                            targeted = null;
                            return;
                        }                     
                    }

                    targeted = hit.transform.GetComponent<GravityController>();
                    targeted.IsTargeted = true;
                }
                if (hit.transform.CompareTag("GravityWall"))
                {
                    if (targeted != null)
                    {
                        GravityManager.ChangeGrav(hit.normal, true);
                    }
                }
            }
        }
    }
}
