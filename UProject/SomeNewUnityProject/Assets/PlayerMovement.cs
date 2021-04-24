using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    [SerializeField] private CharacterController myController;
    [SerializeField] private Rigidbody myRigid;
    [SerializeField] private float speed;
    [SerializeField] private float CameraSensitivity;
    
    
    
    private void Move(float axis, Vector3 WorldDir){
        if (axis >= 0.1f || axis <= -0.1f){
           var position = transform.position +  WorldDir*axis*speed*Time.deltaTime;
            myController.SimpleMove(WorldDir*axis*speed*Time.deltaTime);
            
        }
        
    }
    
    private void CameraRotation(){
        float Pitch = Input.GetAxis("Mouse X");
        float Yaw = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * Pitch);
        //transform.Rotate(new Vector3 (0, Pitch*Time.deltaTime*CameraSensitivity, 0), Space.Self);
        //transform.Rotate(Vector3.up, Pitch*Time.deltaTime*CameraSensitivity);
        //Debug.Log(Pitch + " " + Yaw);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardspeed = Input.GetAxis("Vertical");
        float rightspeed = Input.GetAxis("Horizontal");



        Move (forwardspeed, transform.forward);
        Move (rightspeed, transform.right);
        
        CameraRotation();
    }
}
