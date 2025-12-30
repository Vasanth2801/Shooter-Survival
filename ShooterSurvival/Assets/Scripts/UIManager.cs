using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddCoin()
    {
        score += 10;
        scoreText.text = "Score : " + score.ToString();
    }
}