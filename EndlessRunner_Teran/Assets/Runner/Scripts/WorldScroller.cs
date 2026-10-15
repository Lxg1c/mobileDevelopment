using UnityEngine;

public class WorldScroller : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ground;
    [SerializeField] private SpriteRenderer sky;
    [SerializeField] private SpriteRenderer[] mountains;
    [SerializeField] private SpriteRenderer[] clouds;
    [SerializeField] private float skySpeedMultiplier = 0.06f;
    [SerializeField] private float mountainSpeedMultiplier = 0.35f;
    [SerializeField] private float cloudSpeedMultiplier = 0.18f;

    private TiledLayer groundTiles;
    private TiledLayer skyTiles;
    private Camera sceneCamera;

    private void Start()
    {
        sceneCamera = Camera.main;
        groundTiles = new TiledLayer(ground);
        skyTiles = new TiledLayer(sky);
    }

    private void Update()
    {
        GameManager manager = GameManager.Instance;
        if (manager == null || !manager.IsRunning) return;

        float distance = manager.WorldSpeed * Time.deltaTime;
        groundTiles.Scroll(distance);
        skyTiles.Scroll(distance * skySpeedMultiplier);
        ScrollDecorations(mountains, distance * mountainSpeedMultiplier);
        ScrollDecorations(clouds, distance * cloudSpeedMultiplier);
    }

    private void ScrollDecorations(SpriteRenderer[] renderers, float distance)
    {
        if (renderers == null) return;

        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer == null) continue;

            renderer.transform.position += Vector3.left * distance;
            if (sceneCamera == null) continue;

            float halfCameraWidth = sceneCamera.orthographicSize * sceneCamera.aspect;
            float leftEdge = sceneCamera.transform.position.x - halfCameraWidth;
            if (renderer.bounds.max.x < leftEdge)
            {
                Vector3 position = renderer.transform.position;
                position.x = sceneCamera.transform.position.x + halfCameraWidth + renderer.bounds.extents.x + 2f;
                renderer.transform.position = position;
            }
        }
    }

    private class TiledLayer
    {
        private readonly Transform firstTile;
        private readonly Transform secondTile;
        private readonly Vector3 startPosition;
        private readonly float width;
        private float offset;

        public TiledLayer(SpriteRenderer source)
        {
            if (source == null || source.sprite == null) return;

            startPosition = source.transform.position;
            width = source.bounds.size.x;
            firstTile = CreateTile(source, startPosition);
            secondTile = CreateTile(source, startPosition + Vector3.right * width);
            source.enabled = false;
        }

        public void Scroll(float distance)
        {
            if (firstTile == null || width <= 0f) return;

            offset = Mathf.Repeat(offset + distance, width);
            firstTile.position = startPosition + Vector3.left * offset;
            secondTile.position = startPosition + Vector3.right * (width - offset);
        }

        private static Transform CreateTile(SpriteRenderer source, Vector3 position)
        {
            GameObject tile = new GameObject(source.name + " Tile");
            tile.layer = source.gameObject.layer;
            Transform tileTransform = tile.transform;
            tileTransform.SetParent(source.transform.parent, false);
            tileTransform.localScale = source.transform.localScale;
            tileTransform.SetPositionAndRotation(position, source.transform.rotation);

            SpriteRenderer tileRenderer = tile.AddComponent<SpriteRenderer>();
            tileRenderer.sprite = source.sprite;
            tileRenderer.sharedMaterial = source.sharedMaterial;
            tileRenderer.color = source.color;
            tileRenderer.sortingLayerID = source.sortingLayerID;
            tileRenderer.sortingOrder = source.sortingOrder;
            tileRenderer.flipX = source.flipX;
            tileRenderer.flipY = source.flipY;
            return tileTransform;
        }
    }
}
