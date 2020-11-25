using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyMaterialScript : MonoBehaviour
{
    private Renderer m_mesh = null;

    public float tilingRatio;
 


    private void Awake()
    {
        m_mesh = GetComponent<Renderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 tilingAmount = new Vector2(transform.localScale.x, transform.localScale.z);
        m_mesh.material.mainTextureScale = tilingAmount / tilingRatio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
