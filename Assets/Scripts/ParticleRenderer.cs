using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _emptySpaceColor;

    [field: SerializeField] public Vector2Int SimulationSize { get; private set; }
    [field: SerializeField] public int PixelPerUnit { get; private set; }

    public Texture2D Texture { get; private set; }

    private void Awake()
    {
        CreateTexture();
        ClearSpace();
    }

    private void CreateTexture()
    {
        Texture = new Texture2D(SimulationSize.x, SimulationSize.y);
        Texture.filterMode = FilterMode.Point;

        Sprite sprite = Sprite.Create(Texture, new Rect(0, 0, SimulationSize.x, SimulationSize.y), new Vector2(0.5f, 0.5f), PixelPerUnit);
        _spriteRenderer.sprite = sprite;
    }

    public void SetPixelColor(int x, int y, Color color)
    {
        Texture.SetPixel(x, y, color);
    }

    public void SetPixelColor(Vector2Int pos, Color color)
    {
        SetPixelColor(pos.x, pos.y, color);
    }

    public void SetEmptySpace(int x, int y)
    {
        Texture.SetPixel(x, y, _emptySpaceColor);
    }

    public void SetEmptySpace(Vector2Int pos)
    {
        Texture.SetPixel(pos.x, pos.y, _emptySpaceColor);
    }

    public void ApplyPixelChanges()
    {
        Texture.Apply();
    }

    public void ClearSpace()
    {
        for (int x = 0; x < SimulationSize.x; x++)
        {
            for (int y = 0; y < SimulationSize.y; y++)
            {
                SetEmptySpace(x, y);
            }
        }

        ApplyPixelChanges();
    }

    public Vector2Int GetPixelPositionFromWorldPosition(float worldX, float worldY)
    {
        int xPos = Mathf.RoundToInt(worldX * PixelPerUnit) + SimulationSize.x / 2;
        int yPos = Mathf.RoundToInt(worldY * PixelPerUnit) + SimulationSize.y / 2;

        return new Vector2Int(xPos, yPos);
    }

    public Vector2Int GetPixelPositionFromWorldPosition(Vector3 worldPosition)
    {
        return GetPixelPositionFromWorldPosition(worldPosition.x, worldPosition.y);
    }

    public Vector3 GetWorldPositionFromPixelPosition(int posX, int posY)
    {
        float xPos = (posX - SimulationSize.x / 2f) / PixelPerUnit;
        float yPos = (posY - SimulationSize.y / 2f) / PixelPerUnit;

        return new Vector3(xPos, yPos, 0f);
    }

    public Vector3 GetWorldPositionFromPixelPosition(Vector2Int pixelPosition)
    {
        return GetWorldPositionFromPixelPosition(pixelPosition.x, pixelPosition.y);
    }

    public Vector3 GetPixelSizeInWorldSize()
    {
        return new Vector3(1f / PixelPerUnit, 1f / PixelPerUnit, 0f);
    }
}
