using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWallScript : MonoBehaviour
{
    private GravityCastScript gravCastScript;
    public Shader m_shader = null;
    public Material m_material = null;

    private void Awake()
    {
        gravCastScript = Camera.main.GetComponent<GravityCastScript>();

        m_shader = GetComponent<Renderer>().material.shader;

        m_material = GetComponent<Renderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        gravCastScript.AddWall(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
