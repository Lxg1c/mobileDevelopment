using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsRunning) return;

        if (Input.GetMouseButtonDown(0))
        {
            _rb.linearVelocity = Vector2.up * jumpForce;
        }
    }
}
