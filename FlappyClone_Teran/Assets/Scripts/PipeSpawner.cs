using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 3f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPipe), 1f, spawnInterval);
    }

    private void SpawnPipe()
    {
        if (!GameManager.Instance.IsRunning) return;
        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(spawnX, y, 0f);
        Instantiate(pipePrefab, pos, Quaternion.identity);
    }
}