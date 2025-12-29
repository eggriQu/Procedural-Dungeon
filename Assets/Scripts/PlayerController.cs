using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private Transform movementTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody playerRb;
    Vector2 moveDirection = Vector2.zero;
    Vector2 smoothedDirection;
    Vector2 moveDirectionSmoothedVelocity;
    [SerializeField] private float smoothDampTime;

    [Header("Input Variables")]
    private InputAction move;
    private InputAction fire;

    [Header("Other Variables")]
    private Camera mainCamera;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        move = InputSystem.actions.FindAction("Move");
        fire = InputSystem.actions.FindAction("Fire");
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        move.Enable();
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Rigidbody bulletRb = Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
        bulletRb.AddForce(firePoint.forward * 5, ForceMode.Impulse);
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

        Vector3 transformForward = movementTransform.forward;
        Vector3 transformRight = movementTransform.right;
        transformForward.y = 0;
        transformRight.y = 0;

        Vector3 forwardRelative = smoothedDirection.y * transformForward;
        Vector3 rightRelative = smoothedDirection.x * transformRight;

        Vector3 moveDir = forwardRelative + rightRelative;

        playerRb.linearVelocity = new Vector3(moveDir.x * moveSpeed, playerRb.linearVelocity.y, moveDir.z * moveSpeed);
        //playerRb.linearVelocity = new Vector3(smoothedDirection.x * moveSpeed, playerRb.linearVelocity.y, smoothedDirection.y * moveSpeed);
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }
}
