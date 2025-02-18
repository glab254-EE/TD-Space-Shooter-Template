using UnityEngine;
using UnityEngine.InputSystem;

public class Ship_Movement : MonoBehaviour
{
    [SerializeField] private float GlobalSpeed = 10;
    [SerializeField] private float ForwardThrustMultiplier = 2;
    [SerializeField] private float SpeedMultiplyer = 2;
    [SerializeField] private float TurnSpeed = 2;
    Camera _camera;
    Mouse mouse;
    Rigidbody2D rb;
    CorePlayerInput inputActions;
    Vector2 movementvector;
    void OnMovementKeysPressed(InputAction.CallbackContext callbackContext){
        movementvector = callbackContext.ReadValue<Vector2>().normalized;
    }
    void TurnTowardsMouse()
    {
        Vector3 difference = _camera.ScreenToWorldPoint(mouse.position.ReadValue()) - transform.position;
        difference.z = transform.position.z;
        float TargetRotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // removed 90 from targetrotation as its fixed angle needed to face mouse
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,TargetRotationZ-90),Time.deltaTime*TurnSpeed);
    }
    void MoveStep()
    {
        float BackForthaddition = movementvector.y > 0 ? SpeedMultiplyer * ForwardThrustMultiplier : SpeedMultiplyer;
        Vector2 newforce = transform.up * movementvector.y * GlobalSpeed  * BackForthaddition + transform.right * movementvector.x * SpeedMultiplyer * GlobalSpeed;
        rb.AddForce(newforce * Time.deltaTime);
    }
    void Start()
    {
        mouse = Mouse.current;
        _camera = Camera.main;
        if (!gameObject.TryGetComponent<Rigidbody2D>(out rb)){
            Debug.LogError("No RigidBody2d found!");
        }
        inputActions = new();
        inputActions.PrimaryGameControl.Movement.performed += OnMovementKeysPressed;
        inputActions.PrimaryGameControl.Movement.canceled += OnMovementKeysPressed;
        inputActions.PrimaryGameControl.Movement.Enable();
    }
    void Update()
    {
        TurnTowardsMouse();
        MoveStep();
    }
    void OnDestroy()
    {
        inputActions.PrimaryGameControl.Movement.performed -= OnMovementKeysPressed;
        inputActions.PrimaryGameControl.Movement.canceled -= OnMovementKeysPressed;
        inputActions.PrimaryGameControl.Movement.Disable();        
    }
}
