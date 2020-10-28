using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKeyManager : MonoBehaviour
{
    public delegate void ButtonPress(GameObject player);
    public static event ButtonPress OnButtonPress;
    public KeyCode interactKey = KeyCode.E;
    private GameObject playerPosition = null;

    private void Awake()
    {
        playerPosition = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactKey))
        {
            OnButtonPress?.Invoke(playerPosition);
        }
    }
}
