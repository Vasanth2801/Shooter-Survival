using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField] float speed = 5f;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 9f;
    [SerializeField] bool isJumping = false;

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

    [Header("Player Health")]
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    public Image healthBar;

    private void Awake()
    {
        inputActions = new PlayerController();
        MovementCalling();
    }

    void Start()
    {
        currentHealth = maxHealth;
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

        Jump();

        PlayerHealth();
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

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }
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

    void PlayerHealth()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth/maxHealth,0, 1);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
        }
    }
}