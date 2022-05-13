using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
     // Start is called before the first frame update
    public float speed;
    private Rigidbody myRig;
    public Vector3 move;
    public Vector3 moveVel;

    bool on = false;

    private Camera mainCamera;

    
    // Start is called before the first frame update
    void Start()
    {
       myRig = GetComponent<Rigidbody>(); 
       mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
           on = false;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
           on = true;
        }


           if(on){
       move = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical")); 
       moveVel = move * speed;
    }
        

       Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
       Plane groundPlane = new Plane(Vector3.up,Vector3.zero);
       float rayLength;

       if(groundPlane.Raycast(cameraRay, out rayLength))
       {
          Vector3 pointToLook = cameraRay.GetPoint(rayLength);
        

          transform.LookAt(new Vector3(pointToLook.x,transform.position.y,pointToLook.z));

       }

    }

     void FixedUpdate() {
        myRig.velocity = moveVel;
    }
}
