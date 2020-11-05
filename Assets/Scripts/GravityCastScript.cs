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

    [SerializeField]
    [Tooltip("Default: 0.1")]
    [Range(0.05f, 1)]
    private float capsuleCastRadius = 0.1f;

    private Vector3 pos1 = new Vector3();
    private Vector3 pos2 = new Vector3();

    public void AddWall(GameObject wall)
    {
        levelWalls.Add(wall);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.forward * 50, Color.red);

        if (!PauseMenu.GameIsPaused)
        {

            if (Input.GetMouseButtonDown(0))
            {
                pos1 = transform.position;
                pos2 = transform.position + transform.forward;

                if (Physics.CapsuleCast(pos1, pos2, capsuleCastRadius, transform.forward, out RaycastHit hit))
                {

                    hit.transform.GetComponent<Renderer>().material = highlightedWallMaterial;

                    foreach (GameObject go in levelWalls)
                    {
                        if (go != hit.transform.gameObject)
                        {
                            if (go.GetComponent<Renderer>() != null)
                            {
                                go.GetComponent<Renderer>().material = go.GetComponent<DebugWallScript>().m_material;
                                go.GetComponent<Renderer>().material.shader = go.GetComponent<DebugWallScript>().m_shader;
                            }
                           
                        }
                    }

                    if (hit.transform.CompareTag("GravityWall"))
                    {
                        playerControllerScript.Gravity = hit.normal;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                pos1 = transform.position;
                pos2 = transform.position + transform.forward;

                if (Physics.CapsuleCast(pos1, pos2, capsuleCastRadius, transform.forward, out RaycastHit hit))
                {
                    if (hit.transform.CompareTag("GravityWall"))
                    {
                        GravityManager.ChangeGrav(hit.normal, false);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                pos1 = transform.position;
                pos2 = transform.position + transform.forward;

                if (Physics.CapsuleCast(pos1, pos2, capsuleCastRadius, transform.forward, out RaycastHit hit))
                {
                    if (hit.transform.GetComponent<GravityController>() != null)
                    {
                        if (targeted != null)
                        {
                            targeted.IsTargeted = false;

                            if (targeted.gameObject == hit.transform.gameObject)
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

            if (Input.GetKeyDown(KeyCode.F))
            {
                pos1 = transform.position;
                pos2 = transform.position + transform.forward;

                if (Physics.CapsuleCast(pos1, pos2, capsuleCastRadius, transform.forward, out RaycastHit hit))
                {
                    if (hit.transform.GetComponent<GravityController>() != null)
                    {
                        if(!hit.transform.GetComponent<GravityController>().Frozen)
                        {
                            hit.transform.GetComponent<GravityController>().FreezeConstraints(true);
                        }
                        else
                        {
                            hit.transform.GetComponent<GravityController>().FreezeConstraints(false);
                        }                     
                    }
                }
            }
        }
    }
}
