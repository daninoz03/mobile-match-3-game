using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the game board grid and tile interactions
/// </summary>
public class Board : MonoBehaviour
{
    [Header("Board Dimensions")]
    public int width = 8;
    public int height = 8;
    public float tileSpacing = 1.0f;
    
    [Header("Tile Prefab")]
    public GameObject tilePrefab;
    
    [Header("Tile Sprites")]
    public Sprite[] tileSprites; // Assign sprites for each TileType in Inspector
    
    [Header("Animation")]
    public float swapDuration = 0.3f;
    public float fallDuration = 0.5f;
    
    private Tile[,] tiles;
    private Tile selectedTile;
    private bool isProcessing = false;
    
    private MatchDetector matchDetector;
    private TileSwapper tileSwapper;
    
    void Start()
    {
        matchDetector = GetComponent<MatchDetector>();
        if (matchDetector == null)
        {
            matchDetector = gameObject.AddComponent<MatchDetector>();
        }
        
        tileSwapper = GetComponent<TileSwapper>();
        if (tileSwapper == null)
        {
            tileSwapper = gameObject.AddComponent<TileSwapper>();
        }
        
        InitializeBoard();
    }
    
    /// <summary>
    /// Create the initial game board
    /// </summary>
    void InitializeBoard()
    {
        tiles = new Tile[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                CreateTile(x, y);
            }
        }
        
        // Check for initial matches and regenerate if needed
        while (matchDetector.HasMatches(tiles, width, height))
        {
            RegenerateBoard();
        }
    }
    
    /// <summary>
    /// Create a single tile at the specified position
    /// </summary>
    void CreateTile(int x, int y)
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile prefab is not assigned!");
            return;
        }
        
        Vector3 position = new Vector3(x * tileSpacing, y * tileSpacing, 0);
        GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity, transform);
        tileObject.name = $"Tile_{x}_{y}";
        
        Tile tile = tileObject.GetComponent<Tile>();
        if (tile == null)
        {
            tile = tileObject.AddComponent<Tile>();
        }
        
        TileType randomType = GetRandomTileType();
        tile.Initialize(x, y, this, randomType);
        
        // Set sprite if available
        if (tileSprites != null && tileSprites.Length > (int)randomType)
        {
            tile.SetSprite(tileSprites[(int)randomType]);
        }
        
        tiles[x, y] = tile;
    }
    
    /// <summary>
    /// Get a random tile type (excluding Empty)
    /// </summary>
    TileType GetRandomTileType()
    {
        int typeCount = System.Enum.GetValues(typeof(TileType)).Length - 1; // Exclude Empty
        return (TileType)Random.Range(0, typeCount);
    }
    
    /// <summary>
    /// Regenerate the entire board
    /// </summary>
    void RegenerateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] != null)
                {
                    tiles[x, y].tileType = GetRandomTileType();
                    if (tileSprites != null && tileSprites.Length > (int)tiles[x, y].tileType)
                    {
                        tiles[x, y].SetSprite(tileSprites[(int)tiles[x, y].tileType]);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Called when a tile is selected
    /// </summary>
    public void OnTileSelected(Tile tile)
    {
        if (isProcessing) return;
        
        if (selectedTile == null)
        {
            selectedTile = tile;
            // Visual feedback could be added here (highlight, scale, etc.)
        }
        else
        {
            // Check if tiles are adjacent
            if (tileSwapper.AreAdjacent(selectedTile, tile))
            {
                StartCoroutine(SwapTiles(selectedTile, tile));
            }
            selectedTile = null;
        }
    }
    
    /// <summary>
    /// Swap two tiles and check for matches
    /// </summary>
    private System.Collections.IEnumerator SwapTiles(Tile tile1, Tile tile2)
    {
        isProcessing = true;
        
        // Perform the swap
        yield return StartCoroutine(tileSwapper.SwapTilePositions(tile1, tile2, tiles, swapDuration));
        
        // Check for matches
        List<Tile> matchedTiles = matchDetector.FindAllMatches(tiles, width, height);
        
        if (matchedTiles.Count > 0)
        {
            // Valid move - process matches
            yield return StartCoroutine(ProcessMatches(matchedTiles));
        }
        else
        {
            // Invalid move - swap back
            yield return StartCoroutine(tileSwapper.SwapTilePositions(tile1, tile2, tiles, swapDuration));
        }
        
        isProcessing = false;
    }
    
    /// <summary>
    /// Process matched tiles and refill board
    /// </summary>
    private System.Collections.IEnumerator ProcessMatches(List<Tile> matchedTiles)
    {
        // Remove matched tiles
        foreach (Tile tile in matchedTiles)
        {
            if (tile != null)
            {
                tiles[tile.xIndex, tile.yIndex] = null;
                Destroy(tile.gameObject);
            }
        }
        
        yield return new WaitForSeconds(0.3f);
        
        // Drop tiles and fill empty spaces
        yield return StartCoroutine(RefillBoard());
        
        // Check for cascade matches
        List<Tile> newMatches = matchDetector.FindAllMatches(tiles, width, height);
        if (newMatches.Count > 0)
        {
            yield return StartCoroutine(ProcessMatches(newMatches));
        }
    }
    
    /// <summary>
    /// Drop tiles down and create new ones at the top
    /// </summary>
    private System.Collections.IEnumerator RefillBoard()
    {
        // Drop existing tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] == null)
                {
                    // Find tile above
                    for (int yAbove = y + 1; yAbove < height; yAbove++)
                    {
                        if (tiles[x, yAbove] != null)
                        {
                            tiles[x, y] = tiles[x, yAbove];
                            tiles[x, yAbove] = null;
                            
                            tiles[x, y].xIndex = x;
                            tiles[x, y].yIndex = y;
                            tiles[x, y].MoveTo(new Vector3(x * tileSpacing, y * tileSpacing, 0), fallDuration);
                            break;
                        }
                    }
                }
            }
        }
        
        yield return new WaitForSeconds(fallDuration);
        
        // Create new tiles at the top
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] == null)
                {
                    CreateTile(x, y);
                }
            }
        }
    }
    
    public Tile[,] GetTiles()
    {
        return tiles;
    }
}

