using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private bool _scored = false;

    private void Update()
    {
        if (!GameManager.Instance.IsRunning) return;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < -12f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_scored) return;
        if (other.CompareTag("Player"))
        {
            _scored = true;
            GameManager.Instance.AddScore(1);
        }
    }
}