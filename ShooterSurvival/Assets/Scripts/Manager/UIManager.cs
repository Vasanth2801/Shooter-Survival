using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score;
    [SerializeField] GameObject resumePanel;
    public static bool isGamePaused = false;

    void Awake()
    {
        if (Instance == null)
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void AddCoin()
    {
        score += 10;
        scoreText.text = "Score : " + score.ToString();
    }

    public void Resume()
    {
        if (resumePanel != null)
        {
            resumePanel.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }

    private void Pause()
    {
        if (resumePanel != null)
        {
            resumePanel.SetActive(true);
            Time.timeScale = 0f;
            isGamePaused = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }
}