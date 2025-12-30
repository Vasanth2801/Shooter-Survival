using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float enemySpeed = 3f;
    [SerializeField] Transform target;



    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction *  enemySpeed * Time.deltaTime);
        }
    }
}
