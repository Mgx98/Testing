using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PossessionGame
{
    public class CameraBehaviour : MonoBehaviour
    {
        public enum CameraModes
        {
            TopDown,
            OverShoulder
        }

        [SerializeField]GameObject cameraTarget;         //the target the camera will follow
        GameObject cameraFollowPoint;                    //the physical point in space the camera will follow, which is an empty child of cameraTarget 
        CameraModes cameraMode;
        private Vector3 moveSpeed = Vector3.zero;
        [Range(0.1f, 1.5f)] [SerializeField] private float smoothTime = 0.5f;
        [SerializeField] private Vector3 offset;

        // Start is called before the first frame update
        void Start()
        {
            if (cameraTarget)
            {
                cameraFollowPoint = new GameObject("cameraFollowPoint");
                cameraFollowPoint.transform.SetParent(cameraTarget.transform);
                SetCameraToOverShoulder();
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.SmoothDamp(transform.position, cameraFollowPoint.transform.position, ref moveSpeed, smoothTime);
            if(cameraMode == CameraModes.OverShoulder)
            {
                transform.LookAt(cameraTarget.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(cameraFollowPoint.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }

        public void SetCameraToTopdown()
        {
            cameraMode = CameraModes.TopDown;
            cameraFollowPoint.transform.localPosition = new Vector3(0, 10, 0);
            transform.rotation = Quaternion.Euler(90, 0, 0);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void SetCameraToOverShoulder()
        {
            cameraMode = CameraModes.OverShoulder;
            cameraFollowPoint.transform.localPosition = offset;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ToggleCamera()
        {
            if (cameraMode == CameraModes.OverShoulder) SetCameraToTopdown();
            else SetCameraToOverShoulder();
        }
        public CameraModes CameraMode
        {
            get { return cameraMode; }
        }

        public GameObject CameraFollowPoint
        {
            get { return cameraFollowPoint; }
        }

        public void SetTargetTo(GameObject target)
        {
            cameraTarget = target;
            cameraFollowPoint.transform.eulerAngles = Vector3.zero;
            cameraFollowPoint.transform.SetParent(cameraTarget.transform);
            if (cameraMode == CameraModes.OverShoulder) SetCameraToOverShoulder();
            else SetCameraToTopdown();
        }
    }
}
