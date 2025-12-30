using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField] float speed = 5f;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 9f;
    [SerializeField] bool isJumping = false;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 7f;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private float dashDuration = 0.07f;
    bool isDashing;
    bool canDash = true;
    bool dashPressed;

    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    PlayerController inputActions;

    [Header("Shooting References")]
    [SerializeField] ObjectPoolManager poolManager;
    [SerializeField] Transform firePoint;

    [Header("Inputs")]
    [SerializeField] Vector2 movement;

    [Header("Flipping Logic")]
    [SerializeField] int facingDirection = 1;

    [Header("Player Health")]
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    public Image healthBar;

    private void Awake()
    {
        inputActions = new PlayerController();
        MovementCalling();
        DashCalling();
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

    void DashCalling()
    {
        inputActions.Player.Dash.performed += ctx => dashPressed = true;
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

        if (movement.x > 0 && transform.localScale.x < 0 || movement.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        Jump();

        PlayerHealth();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        HandleMovement();


        if (canDash == true && dashPressed == true)
        {
            dashPressed = false;
            StartCoroutine(Dash());
        }
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

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.AddForce(movement * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void Flip()
    {
        facingDirection = 1;
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
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