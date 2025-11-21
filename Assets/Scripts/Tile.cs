using UnityEngine;

/// <summary>
/// Represents a single tile/gem in the match-3 game
/// </summary>
public class Tile : MonoBehaviour
{
    [Header("Tile Properties")]
    public TileType tileType;
    public int xIndex;
    public int yIndex;
    
    [Header("Visual")]
    public SpriteRenderer spriteRenderer;
    
    private Board board;
    private bool isMoving = false;
    
    public void Initialize(int x, int y, Board gameBoard, TileType type)
    {
        xIndex = x;
        yIndex = y;
        board = gameBoard;
        tileType = type;
        
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
    
    /// <summary>
    /// Move tile to a specific position with animation
    /// </summary>
    public void MoveTo(Vector3 targetPosition, float duration)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine(targetPosition, duration));
        }
    }
    
    private System.Collections.IEnumerator MoveCoroutine(Vector3 target, float duration)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPosition, target, t);
            yield return null;
        }
        
        transform.position = target;
        isMoving = false;
    }
    
    /// <summary>
    /// Handle tap/click on this tile
    /// </summary>
    private void OnMouseDown()
    {
        if (board != null)
        {
            board.OnTileSelected(this);
        }
    }
    
    public void SetSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}

/// <summary>
/// Different types of tiles in the game
/// </summary>
public enum TileType
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple,
    Orange,
    Empty
}

