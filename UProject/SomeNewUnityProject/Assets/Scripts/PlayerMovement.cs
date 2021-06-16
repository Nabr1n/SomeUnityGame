using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private CharacterController myController;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Transform CameraOrigin;
    
    
    [SerializeField] private float MaxForwardSpeed, MaxRightSpeed;
    [SerializeField] private float Acceleration;
    
    [SerializeField] private float StepLength;
    [SerializeField] private AnimationCurve CameraStepMovement;
    [SerializeField] private float maxCameraStepOffset;
    
    private float forwardSpeed;
    private float rightSpeed;
    
    private Vector3 lastStepPlace;
    private float currentStepDistance;



    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;

    [SerializeField] private LayerMask GroundMask;
    
    
    

    Vector3 velocity;
    bool isGrounded;
    
    
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        lastStepPlace = transform.position;
    }

    private void MakeSpeed(float forwardAxis, float rightAxis){
        //  Debug.Log (forwardAxis + " " +rightAxis);
        if (forwardAxis > 0f || forwardAxis < 0f) {
            forwardSpeed += Acceleration * Time.deltaTime;
            forwardSpeed = Mathf.Clamp (forwardSpeed, 0f, MaxForwardSpeed);
        }
        else if (forwardAxis == 0f) {
            forwardSpeed = 0f;
        }

        if (rightAxis > 0f || rightAxis < 0f)
        {
            rightSpeed+=Acceleration * Time.deltaTime;
            rightSpeed = Mathf.Clamp (rightSpeed+Acceleration, 0f, MaxRightSpeed);

        }
        else if (rightAxis == 0f)
        {
             rightSpeed = 0f;
            
        }
       
    }


    private void CheckStep(Vector3 move){
        float StepAlpha;
        
        
        currentStepDistance += Vector3.Distance(move , new Vector3(0,0,0))*Time.deltaTime;
        StepAlpha = currentStepDistance/StepLength;

        CameraOrigin.localPosition = new Vector3(0, CameraStepMovement.Evaluate(StepAlpha)*maxCameraStepOffset, 0);

        if (currentStepDistance >= StepLength) Step();
    }

   


    private void Step(){
        currentStepDistance = 0;
        //Debug.Log("STEP!");
        StartCoroutine(myCamera.GetComponent<CameraShake>().Shake(0.1f, 0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;


        float forwardspeed = Input.GetAxis("Vertical");
        float rightspeed = Input.GetAxis("Horizontal");


        
        
        MakeSpeed(forwardspeed, rightspeed);
        Vector3 move = transform.right * rightspeed * rightSpeed + transform.forward*forwardspeed * forwardSpeed;
        CheckStep(move);
        

        myController.Move(move * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime; 

        
        myController.Move(velocity * Time.deltaTime);
    }
}
