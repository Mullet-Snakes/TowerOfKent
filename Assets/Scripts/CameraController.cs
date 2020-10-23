using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    //hey

    [Range(0, 1000)]
    [Tooltip("Default: 300")]
    public float mouseSensitivity = 300f;

    private Transform player = null;

    private float xRotation = 0f;

    private float mouseX = 0f;
    private float mouseY = 0f;

    public bool MovingPlayer { get; set; } = true;

    private void Awake()
    {
        player = transform.parent;

    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void CameraMove()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (MovingPlayer)
        {
            player.Rotate(Vector3.up * mouseX);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
