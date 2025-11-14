using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody playerRb;
    Vector2 moveDirection = Vector2.zero;
    Vector2 smoothedDirection;
    Vector2 moveDirectionSmoothedVelocity;
    [SerializeField] private float smoothDampTime;

    [Header("Input Variables")]
    private InputAction move;
    //private InputAction aim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        move = InputSystem.actions.FindAction("Move");
    }

    private void OnEnable()
    {
        move.Enable();

        //aim = playerControls.FindAction("Look");
        //aim.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = move.ReadValue<Vector2>();

        smoothedDirection = Vector2.SmoothDamp(
            smoothedDirection,
            moveDirection,
            ref moveDirectionSmoothedVelocity,
            smoothDampTime);

        playerRb.linearVelocity = new Vector3(smoothedDirection.x * moveSpeed, playerRb.linearVelocity.y, smoothedDirection.y * moveSpeed);
    }
}
