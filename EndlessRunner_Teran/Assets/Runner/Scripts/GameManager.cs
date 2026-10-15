using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameConfig config;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private GameObject gameOverPanel;

    private float worldSpeed;
    private float speedTimer;
    private int score;
    private bool isRunning = true;

    public bool IsRunning => isRunning;
    public float WorldSpeed => worldSpeed;

    private void Awake()
    {
        Instance = this;
        worldSpeed = config.baseWorldSpeed;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        scoreText.text = "0";
    }

    private void Update()
    {
        if (!isRunning) return;

        speedTimer += Time.deltaTime;
        if (speedTimer >= 1f)
        {
            speedTimer = 0f;
            worldSpeed = Mathf.Min(worldSpeed + config.speedIncreasePerSecond, config.maxWorldSpeed);
            score += 1;
            scoreText.text = score.ToString();
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (!isRunning) return;
        isRunning = false;

        int best = PlayerPrefs.GetInt("BestScore", 0);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("BestScore", best);
            PlayerPrefs.Save();
        }
        bestText.text = "Рекорд: " + best;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}