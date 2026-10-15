using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool scored;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (scored || !GameManager.Instance.IsRunning) return;
        if (!other.CompareTag("Player")) return;

        scored = true;
        GameManager.Instance.AddScore(1);
    }
}