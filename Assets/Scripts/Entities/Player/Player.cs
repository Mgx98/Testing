using PossessionGame;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region SerialisedFields

    [Range(20f, 100f)] [SerializeField] private float moveSpeed = 20;   //the speed at which the player will move
    [Range(10f, 50f)] [SerializeField] private float strafeSpeed = 15f; //the speed at which the player will strafe in OverShoulder mode
    [Range(0.1f, 2.0f)] [SerializeField] private float smoothTime = 1;  //the time it will take for the player to accelerate to the movespeed
    [SerializeField] private bool invertMouseY = true;


    //the body the player is currently possessing, and thus the object the movement and such will work on
    [SerializeField] public GameObject currentPossessedBody;

    #endregion

    #region PrivateFields

    //characterController used to move the playerObject with collision with the world
    CharacterController characterController;

    // nca
    public PlayerHealth health;
    public BowCon theBow;

    //reference to the camera script
    CameraBehaviour _mainCamera;

    //the vector used to store the speed of the player
    private Vector3 velocity = Vector3.zero;


    //angles used to clamp the camera in 3rd person mode
    private const int LowestViewAngle = -10;
    private const int HighestViewAngle = 30;
    #endregion

    #region PublicFields

    public float Health => throw new System.NotImplementedException();

    public float Stamina => throw new System.NotImplementedException();

    public float AttackDamage => throw new System.NotImplementedException();

    public float ArmorShielding => throw new System.NotImplementedException();

    public float MaxSpeed => throw new System.NotImplementedException();

    public float MinSpeed => throw new System.NotImplementedException();

    public float DodgingCooldown => throw new System.NotImplementedException();

    public float CombatRange => throw new System.NotImplementedException();
    #endregion

    #region Logic

    #region MovementLogic
    void Start()
    {
        health = currentPossessedBody.AddComponent<PlayerHealth>(); 
         characterController = currentPossessedBody.AddComponent<CharacterController>();
        _mainCamera = Camera.main.GetComponent<CameraBehaviour>();
    }

    void Update()
    {

        
        if (_mainCamera.CameraMode == CameraBehaviour.CameraModes.TopDown)
        {
            CalculateObjectSpeedTopdown();
            RotateObjectTowardsMouse();
        }
        else if (_mainCamera.CameraMode == CameraBehaviour.CameraModes.OverShoulder)
        {
            RotateObjectBecauseMouse();
            CalculateObjectSpeedOverShoulder();
        }

        if (Input.GetKeyDown("space"))
        {
            _mainCamera.ToggleCamera();
        }

        /////nca
        ///
        if (currentPossessedBody.gameObject.name == "ArcherBody")
        {

            if (Input.GetMouseButtonDown(0))
            {
                theBow.isFiring = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                theBow.isFiring = false;
            }
        }
    }

    private void LateUpdate()
    {
        characterController.Move(velocity * Time.deltaTime);
    }

    private void CalculateObjectSpeedTopdown()
    {
        //create a Vector3 that stores the current horizontal and vertical input values
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //Smoothly accelerate the object towards the current input direction
        Vector3.SmoothDamp(velocity, moveDirection * moveSpeed, ref velocity, smoothTime);
    }

    private void CalculateObjectSpeedOverShoulder()
    {
        Vector3 targetVelocity = Vector3.zero;
        targetVelocity.x = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        targetVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;

        targetVelocity = currentPossessedBody.transform.right * targetVelocity.x + currentPossessedBody.transform.forward * targetVelocity.z;
        targetVelocity.y = 0;

        Vector3.SmoothDamp(velocity, targetVelocity, ref velocity, smoothTime);
    }

    private void RotateObjectTowardsMouse()
    {
        //get the vector from the object towards the mouse
        Vector3 ObjectTowardsMouse = Input.mousePosition - Camera.main.WorldToScreenPoint(currentPossessedBody.transform.position);

        //calculate the angle of this vector
        float angleTowardsMouse = Mathf.Atan2(ObjectTowardsMouse.y, ObjectTowardsMouse.x) * Mathf.Rad2Deg;

        //set the rotation of this object to said angle, but negative
        //the angle is negative because the object would otherwise rotate the opposite way of the mouse
        currentPossessedBody.transform.rotation = Quaternion.Euler(currentPossessedBody.transform.rotation.eulerAngles.x, -angleTowardsMouse + 90, currentPossessedBody.transform.rotation.eulerAngles.z);
    }

    private void RotateObjectBecauseMouse()
    {
        //get the amount the mouse moved this frame
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //rotate the object and the followPoint based on the mouse movement
        currentPossessedBody.transform.Rotate(new Vector3(0, mouseDelta.x, 0));
        int invertMouseYValue = invertMouseY ? -1 : 1;
        _mainCamera.CameraFollowPoint.transform.Rotate(new Vector3(mouseDelta.y * invertMouseYValue, 0, 0));

        //store the current rotation of the followpoint
        Vector3 placeholder = _mainCamera.CameraFollowPoint.transform.eulerAngles;

        //clamp the X-axis rotation, so the player can't rotate too far
        placeholder = new Vector3(ClampAngle(placeholder.x, LowestViewAngle, HighestViewAngle), placeholder.y, placeholder.z);

        //set the followPoint rotation to the clamped rotation
        _mainCamera.CameraFollowPoint.transform.eulerAngles = placeholder;
    }

    //Method to clamp an angle between 2 values, accepts negative values as well
    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    public void TakeDamage(float attackPoints)
    {
        throw new System.NotImplementedException();
    }

    public void SetTargetTo(GameObject target)
    {
        Destroy(currentPossessedBody.GetComponent<CharacterController>());
        currentPossessedBody = target;

        //nca
        health = currentPossessedBody.AddComponent<PlayerHealth>();
       


        characterController = currentPossessedBody.AddComponent<CharacterController>();

        _mainCamera.SetTargetTo(target);
    }
    #endregion

    #region PossesionLogic



    #endregion

    #endregion

}
