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

    [Header("Flipping Logic")]
    [SerializeField] bool isFacingRight = true;

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

        Flip();
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
        if(Input.GetMouseButtonDown(0))
        {
            poolManager.SpawnObjects("Bullet", firePoint.position, firePoint.rotation);
        }
    }

    void Flip()
    {
        if(isFacingRight && movement.x < 0 || !isFacingRight && movement.x > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}