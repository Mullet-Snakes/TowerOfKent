using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public string keyName = "bluekey";

    [SerializeField]
    [Tooltip("Default: 2")]
    [Range(0,5)]
    private float distanceToInteract = 2f;


    private void OnEnable()
    {
        InteractKeyManager.OnButtonPress += CheckForInteract;
    }

    private void OnDisable()
    {
        InteractKeyManager.OnButtonPress -= CheckForInteract;
    }

    private void Awake()
    {

    }

    private void CheckForInteract(GameObject playerPos)
    {
        if (Vector3.Distance(transform.position, playerPos.transform.position) < distanceToInteract)
        {
            KeyChainScript.PickUpKey(keyName);

            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        KeyChainScript.AddKey(keyName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            KeyChainScript.PrintKeyChain();
        }
    }
}
