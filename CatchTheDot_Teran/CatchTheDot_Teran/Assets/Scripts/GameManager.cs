using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;


    private float timeLeft = 30f;
    private int score = 0;
    private bool isRunning = true;


    public bool IsRunning => isRunning;


    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if (!isRunning) return;
        timeLeft -= Time.deltaTime;
        timerText.text = "Время: " + Mathf.CeilToInt(timeLeft);
        if (timeLeft <= 0f) GameOver();
    }


    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Счёт: " + score;
    }


    private void GameOver()
    {
        isRunning = false;
        gameOverPanel.SetActive(true);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}