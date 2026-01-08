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
    Vector2 smoothedDirection;
    Vector2 moveDirectionSmoothedVelocity;
    [SerializeField] private float smoothDampTime;
    [SerializeField] private bool isMoving;
    [SerializeField] private float maxVelocity;

    [Header("Input Variables")]
    private InputAction move;
    private InputAction fire;

    [Header("Other Variables")]
    private Camera mainCamera;
    [SerializeField] private LayerMask groundMask;
    public DungeonRoom currentRoom;
    public Vector3 respawnPoint;
    [SerializeField] private GameManager gameManager;

    [Header("Weapon Variables")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private bool weaponFired;
    [SerializeField] private float movementCooldownTime;
    public int shotsLeft;
    [SerializeField] private float reloadTime;
    public bool isReloading;

    [Header("Player Stats")]
    public int health;
    public bool invincible;

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
        move.performed += Move;
        move.canceled += StopMoving;

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
        if (shotsLeft > 0 && !isReloading)
        {
            StopCoroutine("MovementCooldown");
            Rigidbody bulletRb = Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
            bulletRb.AddForce(firePoint.forward * 15, ForceMode.Impulse);
            StartCoroutine(MovementCooldown(movementCooldownTime));
            shotsLeft--;
        }
        else if (shotsLeft <= 0 && !isReloading)
        {
            StartCoroutine(Reload(reloadTime));
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        isMoving = true;
    }

    private void StopMoving(InputAction.CallbackContext context)
    {
        isMoving = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (maxVelocity > 14)
        {
            maxVelocity -= 0.07333333333f; // I actually forgot what this value was :(
            weaponFired = true;
        }
        else
        {
            maxVelocity = 14;
            weaponFired = false;
        }
        playerRb.maxLinearVelocity = maxVelocity;

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
        Vector3 movementVector = new Vector3(moveDir.x * moveSpeed, playerRb.linearVelocity.y, moveDir.z * moveSpeed);

        if (isMoving && !weaponFired)
        {
            playerRb.linearVelocity = new Vector3(moveDir.x * moveSpeed, playerRb.linearVelocity.y, moveDir.z * moveSpeed);
        }
        else if (isMoving && weaponFired)
        {
            playerRb.linearVelocity = playerRb.linearVelocity + new Vector3(moveDir.x, 0, moveDir.z);
        }
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

    private IEnumerator MovementCooldown(float time)
    {
        //weaponFired = true;
        maxVelocity = 18;
        playerRb.linearVelocity = Vector3.zero;
        playerRb.AddForce(-transform.forward * 300, ForceMode.Impulse);
        yield return new WaitForSeconds(movementCooldownTime);
        //weaponFired = false;
    }

    private IEnumerator Reload(float time)
    {
        isReloading = true;
        yield return new WaitForSeconds(time);
        shotsLeft = 2;
        isReloading = false;
    }

    IEnumerator TakeDamage()
    {
        health -= 1;
        if (health == 0)
        {
            gameManager.GameOver();
            StopCoroutine(TakeDamage());
        }
        invincible = true;
        yield return new WaitForSeconds(1);
        invincible = false;
    }

    private void TakeHazardDamage()
    {
        transform.position = currentRoom.respawnPoint.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.CompareTag("Hazard"))
        {
            TakeHazardDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !invincible)
        {
            StartCoroutine(TakeDamage());
            Destroy(other.gameObject);
        }
    }

    public void ChangeCurrentRoom(DungeonRoom dungeonRoom)
    {
        currentRoom = dungeonRoom;
    }
}
