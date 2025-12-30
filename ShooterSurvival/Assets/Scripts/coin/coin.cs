using UnityEngine;

public class coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
