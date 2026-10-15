using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameConfig config;
    [SerializeField] private float spawnX = 12f;
    [SerializeField] private float groundY = -3f;
    [SerializeField] private float spawnZ = -4f;
    [SerializeField] private Collider2D groundCollider;
    [SerializeField] private float coinHeightAboveGround = 3.5f;

    private float currentInterval;

    private void Start()
    {
        currentInterval = config.spawnInterval;
        StartCoroutine(SpawnLoop());
        StartCoroutine(IncreaseDifficulty());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentInterval);
            if (GameManager.Instance != null && GameManager.Instance.IsRunning)
            {
                Spawn();
            }
        }
    }

    private IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            currentInterval = Mathf.Max(currentInterval - 0.1f, config.minSpawnInterval);
        }
    }

    private void Spawn()
    {
        float surfaceY = groundCollider != null ? groundCollider.bounds.max.y : groundY;
        ObjectPool.Instance.GetObstacle(new Vector3(spawnX, surfaceY, spawnZ));

        if (Random.value < config.coinSpawnChance)
        {
            ObjectPool.Instance.GetCoin(new Vector3(spawnX, surfaceY + coinHeightAboveGround, spawnZ));
        }
    }
}
