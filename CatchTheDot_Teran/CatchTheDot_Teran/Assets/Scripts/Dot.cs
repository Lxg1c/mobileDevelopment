using UnityEngine;


public class Dot : MonoBehaviour
{
    [SerializeField] private float minX = -4f;
    [SerializeField] private float maxX = 4f;
    [SerializeField] private float minY = -7f;
    [SerializeField] private float maxY = 7f;


    public void MoveToRandomPosition()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        transform.position = new Vector2(x, y);
    }
}