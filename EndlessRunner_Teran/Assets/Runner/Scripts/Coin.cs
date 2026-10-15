using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreValue = 5;
    private static Sprite fallbackSprite;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = GetFallbackSprite();
        }
    }

    private static Sprite GetFallbackSprite()
    {
        if (fallbackSprite != null) return fallbackSprite;

        const int size = 32;
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(15.5f, 15.5f));
                Color color = distance > 15f ? Color.clear
                    : distance > 12f ? new Color(0.75f, 0.38f, 0.03f)
                    : distance > 9f ? new Color(1f, 0.7f, 0.12f)
                    : new Color(1f, 0.85f, 0.2f);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        fallbackSprite = Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 200f);
        return fallbackSprite;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsRunning) return;
        transform.position += Vector3.left * GameManager.Instance.WorldSpeed * Time.deltaTime;
        if (transform.position.x < -14f)
        {
            ObjectPool.Instance.ReturnCoin(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.IsRunning)
        {
            GameManager.Instance.AddScore(scoreValue);
            ObjectPool.Instance.ReturnCoin(gameObject);
        }
    }
}
