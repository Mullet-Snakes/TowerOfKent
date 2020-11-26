using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField] private Material offMaterial = null;
    [SerializeField] private Material onMaterial = null;
    [SerializeField] private GameObject renderObject = null;
    private MeshRenderer m_renderer = null;

    private bool isColliding = false;

    public bool IsColliding
    {
        get
        {
            return isColliding;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        m_renderer.material = onMaterial;

    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
        m_renderer.material = offMaterial;
    }

    private void Awake()
    {
        m_renderer = renderObject.GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
