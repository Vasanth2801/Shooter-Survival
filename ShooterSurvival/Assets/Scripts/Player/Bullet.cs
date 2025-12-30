using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 15f;
    public float lifeTime = 4f;
    float lifeTimer;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        lifeTimer = lifeTime;
        if( rb != null )
        {
            rb.linearVelocity = transform.right * bulletSpeed;
        }
    }

    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
            UIManager.Instance.AddCoin();
        }
        gameObject.SetActive(false);
    }
}