using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractKeyManager : MonoBehaviour
{
    public delegate void ButtonPress(GameObject player);
    public static event ButtonPress OnButtonPress;
    public KeyCode interactKey = KeyCode.E;
    private GameObject playerPosition = null;
    public float sceneIndexToDestroy = 1;

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
        Cursor.visible = false;

        if (_instance != null && _instance != this)
        {
            DestroyThis();
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);

        playerPosition = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == sceneIndexToDestroy)
        {
            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
