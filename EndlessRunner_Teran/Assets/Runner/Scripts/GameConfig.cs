using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Runner/Game Config")]
public class GameConfig : ScriptableObject
{
    public float baseWorldSpeed = 5f;
    public float maxWorldSpeed = 15f;
    public float speedIncreasePerSecond = 0.15f;
    public float spawnInterval = 1.8f;
    public float minSpawnInterval = 0.7f;
    public float coinSpawnChance = 0.3f;
}