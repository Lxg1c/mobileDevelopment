using UnityEngine;


public class Basket : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float catchRadius = 0.8f;
    [SerializeField] private Dot dot;


    private Vector2 targetPosition;
    private bool isCaught = false;


    private void Start()
    {
        targetPosition = transform.position;
    }


    private void Update()
    {
        if (!GameManager.Instance.IsRunning) return;


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector2(wp.x, wp.y);
        }


        transform.position = Vector2.MoveTowards(
            transform.position, targetPosition, moveSpeed * Time.deltaTime);


        if (!isCaught &&
            Vector2.Distance(transform.position, dot.transform.position) < catchRadius)
        {
            isCaught = true;
            GameManager.Instance.AddScore(1);
            dot.MoveToRandomPosition();
            Invoke(nameof(ResetCatch), 0.2f);
        }
    }


    private void ResetCatch()
    {
        isCaught = false;
    }
}