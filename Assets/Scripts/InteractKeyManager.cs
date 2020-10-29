using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKeyManager : MonoBehaviour
{
    public delegate void ButtonPress(GameObject player);
    public static event ButtonPress OnButtonPress;
    public KeyCode interactKey = KeyCode.E;
    private GameObject playerPosition = null;

    private static InteractKeyManager _instance = null;
    public static InteractKeyManager _Instance
    {
        get
        {
            return _instance;
        }    
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);

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
