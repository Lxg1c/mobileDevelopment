using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int obstaclePoolSize = 15;
    [SerializeField] private int coinPoolSize = 20;

    private readonly Queue<GameObject> obstacles = new Queue<GameObject>();
    private readonly Queue<GameObject> coins = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        Prewarm(obstacles, obstaclePrefab, obstaclePoolSize);
        Prewarm(coins, coinPrefab, coinPoolSize);
    }

    private void Prewarm(Queue<GameObject> pool, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(prefab, transform);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public GameObject GetObstacle(Vector3 position) => GetFromPool(obstacles, obstaclePrefab, position);
    public GameObject GetCoin(Vector3 position) => GetFromPool(coins, coinPrefab, position);

    private GameObject GetFromPool(Queue<GameObject> pool, GameObject prefab, Vector3 position)
    {
        GameObject go = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, transform);
        go.transform.position = position;
        go.SetActive(true);
        return go;
    }

    public void ReturnObstacle(GameObject go) => ReturnToPool(obstacles, go);
    public void ReturnCoin(GameObject go) => ReturnToPool(coins, go);

    private void ReturnToPool(Queue<GameObject> pool, GameObject go)
    {
        go.SetActive(false);
        pool.Enqueue(go);
    }
}
