using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f) * mouseSensitivity * Time.deltaTime;
        float mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f) * mouseSensitivity * Time.deltaTime;

        //if (Input.GetAxis("Mouse Y")>1) Debug.Log(Input.GetAxis("Mouse Y"));
        


        xRotation -= mouseY;
        
        

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler (xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
