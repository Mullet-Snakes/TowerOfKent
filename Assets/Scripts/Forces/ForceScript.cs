using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceScript : MonoBehaviour
{
    protected Vector3 force = new Vector3();

    public virtual Vector3 GetForce()
    {
        return force;
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
