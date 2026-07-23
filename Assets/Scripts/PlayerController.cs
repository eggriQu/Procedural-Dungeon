using System.Collections;
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
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isGrounded;

    [Header("Input Variables")]
    private InputAction move;
    private InputAction jump;

    [Header("Other Variables")]
    private Camera mainCamera;
    [SerializeField] private LayerMask groundMask;
    public Vector3 respawnPoint;
    [SerializeField] private Animator animator;

    [Header("Player Stats")]
    public int health;
    public bool invincible;

    public int tool;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        move.Enable();
        move.performed += Move;
        move.canceled += StopMoving;
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        isMoving = true;
    }

    private void StopMoving(InputAction.CallbackContext context)
    {
        isMoving = false;
        playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = move.ReadValue<Vector2>();

        Vector3 transformForward = movementTransform.forward;
        Vector3 transformRight = movementTransform.right;
        transformForward.y = 0;
        transformRight.y = 0;

        Vector3 forwardRelative = moveDirection.y * transformForward;
        Vector3 rightRelative = moveDirection.x * transformRight;

        Vector3 moveDir = forwardRelative + rightRelative;
        Vector3 movementVector = new Vector3(moveDir.x * moveSpeed, playerRb.linearVelocity.y, moveDir.z * moveSpeed);

        if (isMoving)
        {
            playerRb.linearVelocity = new Vector3(moveDir.x * moveSpeed, playerRb.linearVelocity.y, moveDir.z * moveSpeed);
        }
    }

    private void Update()
    {
        
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
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            Debug.Log(true);
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            Debug.Log(false);
            return (success: false, position: Vector3.zero);
        }
    }

    IEnumerator TakeDamage()
    {
        health -= 1;
        if (health == 0)
        {
            GameManager.instance.GameOver();
            StopCoroutine(TakeDamage());
        }
        invincible = true;
        yield return new WaitForSeconds(1);
        invincible = false;
    }

    private void TakeHazardDamage()
    {
        StartCoroutine(TakeDamage());
    }

    void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.CompareTag("Hazard"))
        {
            TakeHazardDamage();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TakeDamage());
        }

        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !invincible)
        {
            StartCoroutine(TakeDamage());
            Destroy(collision.gameObject);
        }
    }
}
