using UnityEngine;

public class BirdCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsRunning) return;
        GameManager.Instance.GameOver();
        GetComponent<Rigidbody2D>().simulated = false;
    }
}