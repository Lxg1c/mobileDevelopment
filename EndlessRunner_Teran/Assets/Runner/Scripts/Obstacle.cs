using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Update()
    {
        if (!GameManager.Instance.IsRunning) return;
        transform.position += Vector3.left * GameManager.Instance.WorldSpeed * Time.deltaTime;
        if (transform.position.x < -14f)
        {
            ObjectPool.Instance.ReturnObstacle(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
