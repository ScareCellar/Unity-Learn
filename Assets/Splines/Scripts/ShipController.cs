using UnityEngine;
using UnityEngine.InputSystem;
public class ShipController : MonoBehaviour
{
    [SerializeField] SplineFollower splineFollower;
    [SerializeField] Camera shipCamera;
    [SerializeField, Tooltip("Vector offset of camera from spline follower")]
    Vector3
    cameraOffset = Vector3.zero;
    [SerializeField, Range(1, 50), Tooltip("Forward speed along spline")] float thrust = 5;
    [SerializeField, Range(1, 50), Tooltip("Forward speed boost along spline")]
    float
    thrustBoost = 8;
    [SerializeField, Range(1, 50), Tooltip("Speed (up/down/left/right) of ship")]
    float speed =
    20;
    [SerializeField, Range(1, 50), Tooltip("Rate that ship comes to a stop")] float damping = 8;
    [SerializeField, Range(0, 90), Tooltip("Z roll of ship")] float roll = 4;
    [SerializeField, Range(0, 1), Tooltip("Normalized distance from edge of screen to clampship" )] float edgeClamp = 0.2f;
bool boost;
    Vector2 inputMove;
    Vector3 velocity = Vector3.zero;
    InputAction moveAction;
    InputAction attackAction;
    InputAction sprintAction;
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        sprintAction = InputSystem.actions.FindAction("Sprint");
    }
    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        moveAction.Enable();
        attackAction.started += OnAttack;
        attackAction.canceled += OnAttack;
        attackAction.Enable();
        sprintAction.started += OnSprint;
        sprintAction.canceled += OnSprint;
        sprintAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.Disable();
        attackAction.started -= OnAttack;
        attackAction.canceled -= OnAttack;
        attackAction.Disable();
        sprintAction.started -= OnSprint;
        sprintAction.canceled -= OnSprint;
        sprintAction.Disable();
    }
    void Update()
    {
        // offset camera position from the parent
        shipCamera.transform.localPosition = cameraOffset;
        // force = input
        // velocity += force
        // position += velocity
        Vector3 force = new Vector3(inputMove.x, inputMove.y, 0) * speed;
        velocity += force * Time.deltaTime;
        // apply damping to velocity
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, damping * Time.deltaTime);
        transform.localPosition += velocity * Time.deltaTime;
        // roll based on x velocity
        transform.localRotation = Quaternion.AngleAxis(-velocity.x * roll, Vector3.forward);
        // clamp position to viewport
        Vector3 viewportPosition = shipCamera.WorldToViewportPoint(transform.position);
        // clamp position and kill velocity at edges
        if (viewportPosition.x <= edgeClamp || viewportPosition.x >= 1 - edgeClamp)
            velocity.x = Mathf.MoveTowards(velocity.x, 0, 40 * Time.deltaTime);
        if (viewportPosition.y <= edgeClamp || viewportPosition.y >= 1 - edgeClamp)
            velocity.y = Mathf.MoveTowards(velocity.y, 0, 40 * Time.deltaTime);
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, edgeClamp, 1 - edgeClamp);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, edgeClamp, 1 - edgeClamp);
        transform.position = shipCamera.ViewportToWorldPoint(viewportPosition);
        // set spline follower speed
        splineFollower.Speed = (boost) ? thrustBoost : thrust;
    }
    #region input
    public void OnMove(InputAction.CallbackContext ctx)
    {
        inputMove = ctx.ReadValue<Vector2>();
    }
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Attacking");
        // add weapon fire
    }
    public void OnSprint(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started) boost = true;
        else if (ctx.phase == InputActionPhase.Canceled) boost = false;
    }
    #endregion // input
}