using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField] float speed = 5f;

    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    PlayerController inputActions;

    [Header("Shooting References")]
    [SerializeField] ObjectPoolManager poolManager;
    [SerializeField] Transform firePoint;

    [Header("Inputs")]
    [SerializeField] Vector2 movement;

    private void Awake()
    {
        inputActions = new PlayerController();
        MovementCalling();
    }

    void MovementCalling()
    {
        inputActions.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movement = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 move = rb.position + movement * speed * Time.deltaTime;
        rb.MovePosition(move);
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            poolManager.SpawnObjects("Bullet", firePoint.position, firePoint.rotation);
        }
    }
}