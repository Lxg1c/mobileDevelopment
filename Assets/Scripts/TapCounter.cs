using UnityEngine;
using TMPro;
public class TapCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int _score;

    void Start()
    {
        _score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = "Score: " + _score;
    }

    public void IncrementScore()
    {
        _score++;
        scoreText.text = "Score: " + _score;
        PlayerPrefs.SetInt("score", _score);
    }
}
