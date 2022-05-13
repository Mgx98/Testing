using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   public float speed;
    private Rigidbody myRig;
    public Vector3 move;
    public Vector3 moveVel;


    private Camera mainCamera;

    public BowCon theBow;
    // Start is called before the first frame update
    void Start()
    {
       myRig = GetComponent<Rigidbody>(); 
       mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

 
       move = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical")); 
       moveVel = move * speed;
    
        

       Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
       Plane groundPlane = new Plane(Vector3.up,Vector3.zero);
       float rayLength;

       if(groundPlane.Raycast(cameraRay, out rayLength))
       {
          Vector3 pointToLook = cameraRay.GetPoint(rayLength);
          
          
  
          transform.LookAt(new Vector3(pointToLook.x,transform.position.y,pointToLook.z));
          
       }


       if(Input.GetMouseButtonDown(0)){
          theBow.isFiring = true;
       }
       if(Input.GetMouseButtonUp(0)){
          theBow.isFiring = false;
       }
    }

     void FixedUpdate() {
        myRig.velocity = moveVel;
    }
}
