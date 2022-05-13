using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PossessionGame
{
    public class MoveObjectWithInput : MonoBehaviour
    {
        [Range(20f, 100f)] [SerializeField] private float moveSpeed= 20;    //the speed at which the player will move
        [Range(10f, 50f)] [SerializeField] private float strafeSpeed = 15f; //the speed at which the player will strafe in OverShoulder mode
        [Range(0.1f, 2.0f)] [SerializeField] private float smoothTime = 1;  //the time it will take for the player to accelerate to the movespeed
        Matrix4x4 velocityRotationMatrix = Matrix4x4.zero;                  // the matrix that will rotate the velocity in the direction the player is moving in OverShoulder mode
        CharacterController characterController;
        CameraBehaviour _mainCamera;
        private Vector3 velocity = Vector3.zero;    //the vector used to store the speed of the player

        // Start is called before the first frame update
        void Start()
        {
            characterController = gameObject.AddComponent<CharacterController>();
            _mainCamera = Camera.main.GetComponent<CameraBehaviour>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_mainCamera.CameraMode == CameraBehaviour.CameraModes.TopDown)
            {
                CalculateObjectSpeedTopdown();
                RotateObjectTowardsMouse();
            } else if(_mainCamera.CameraMode == CameraBehaviour.CameraModes.OverShoulder)
            {
                RotateObjectBecauseMouse();
                CalculateObjectSpeedOverShoulder();
            }

            if (Input.GetKeyDown("space"))
            {
                _mainCamera.ToggleCamera();
            }
        }

        private void LateUpdate()
        {
            characterController.Move(velocity * Time.deltaTime);
        }

        private void CalculateObjectSpeedTopdown()
        {
            //create a Vector3 that stores the current horizontal and vertical input values
            Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0 ,Input.GetAxisRaw("Vertical"));

            //Smoothly accelerate the object towards the current input direction
            Vector3.SmoothDamp(velocity, moveDirection * moveSpeed, ref velocity, smoothTime);
        }

        private void CalculateObjectSpeedOverShoulder()
        {
            Vector3 targetVelocity = Vector3.zero;
            targetVelocity.x = Input.GetAxisRaw("Horizontal") * strafeSpeed;
            targetVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;

            targetVelocity = transform.right * targetVelocity.x + transform.forward * targetVelocity.z;
            targetVelocity.y = 0;

            Vector3.SmoothDamp(velocity, targetVelocity, ref velocity, smoothTime);
        }

        private void RotateObjectTowardsMouse()
        {
            //get the vector from the object towards the mouse
            Vector3 ObjectTowardsMouse = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

            //calculate the angle of this vector
            float angleTowardsMouse = Mathf.Atan2(ObjectTowardsMouse.y, ObjectTowardsMouse.x) * Mathf.Rad2Deg;

            //set the rotation of this object to said angle, but negative
            //the angle is negative because the object would otherwise rotate the opposite way of the mouse
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -angleTowardsMouse, transform.rotation.eulerAngles.z);
        }

        private void RotateObjectBecauseMouse()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            transform.Rotate(new Vector3(0, mouseDelta.x, 0));

            _mainCamera.CameraFollowPoint.transform.Rotate(new Vector3(mouseDelta.y, 0, 0));

            Vector3 placeholder = _mainCamera.CameraFollowPoint.transform.eulerAngles;
            placeholder = new Vector3(ClampAngle(placeholder.x, -40, 40), placeholder.y, placeholder.z);
            _mainCamera.CameraFollowPoint.transform.eulerAngles = placeholder;
        }

        float ClampAngle(float angle, float from, float to)
     {
         // accepts e.g. -80, 80
         if (angle < 0f) angle = 360 + angle;
         if (angle > 180f) return Mathf.Max(angle, 360+from);
         return Mathf.Min(angle, to);
     }
    }
}
