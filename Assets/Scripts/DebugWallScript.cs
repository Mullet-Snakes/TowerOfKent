using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWallScript : MonoBehaviour
{
    private GravityCastScript gravCastScript;

    private void Awake()
    {
        gravCastScript = Camera.main.GetComponent<GravityCastScript>();
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
