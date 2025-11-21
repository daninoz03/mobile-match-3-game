using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Detects matches of 3 or more tiles in a row
/// </summary>
public class MatchDetector : MonoBehaviour
{
    /// <summary>
    /// Find all matches on the board
    /// </summary>
    public List<Tile> FindAllMatches(Tile[,] tiles, int width, int height)
    {
        HashSet<Tile> matchedTiles = new HashSet<Tile>();
        
        // Check horizontal matches
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width - 2; x++)
            {
                if (tiles[x, y] != null && tiles[x + 1, y] != null && tiles[x + 2, y] != null)
                {
                    TileType type = tiles[x, y].tileType;
                    
                    if (type != TileType.Empty &&
                        tiles[x + 1, y].tileType == type &&
                        tiles[x + 2, y].tileType == type)
                    {
                        matchedTiles.Add(tiles[x, y]);
                        matchedTiles.Add(tiles[x + 1, y]);
                        matchedTiles.Add(tiles[x + 2, y]);
                        
                        // Check for longer matches
                        int offset = 3;
                        while (x + offset < width && tiles[x + offset, y] != null &&
                               tiles[x + offset, y].tileType == type)
                        {
                            matchedTiles.Add(tiles[x + offset, y]);
                            offset++;
                        }
                    }
                }
            }
        }
        
        // Check vertical matches
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height - 2; y++)
            {
                if (tiles[x, y] != null && tiles[x, y + 1] != null && tiles[x, y + 2] != null)
                {
                    TileType type = tiles[x, y].tileType;
                    
                    if (type != TileType.Empty &&
                        tiles[x, y + 1].tileType == type &&
                        tiles[x, y + 2].tileType == type)
                    {
                        matchedTiles.Add(tiles[x, y]);
                        matchedTiles.Add(tiles[x, y + 1]);
                        matchedTiles.Add(tiles[x, y + 2]);
                        
                        // Check for longer matches
                        int offset = 3;
                        while (y + offset < height && tiles[x, y + offset] != null &&
                               tiles[x, y + offset].tileType == type)
                        {
                            matchedTiles.Add(tiles[x, y + offset]);
                            offset++;
                        }
                    }
                }
            }
        }
        
        return new List<Tile>(matchedTiles);
    }
    
    /// <summary>
    /// Check if there are any matches on the board
    /// </summary>
    public bool HasMatches(Tile[,] tiles, int width, int height)
    {
        return FindAllMatches(tiles, width, height).Count > 0;
    }
    
    /// <summary>
    /// Check if a specific tile is part of a match
    /// </summary>
    public bool IsTileInMatch(Tile tile, Tile[,] tiles, int width, int height)
    {
        if (tile == null) return false;
        
        int x = tile.xIndex;
        int y = tile.yIndex;
        TileType type = tile.tileType;
        
        if (type == TileType.Empty) return false;
        
        // Check horizontal
        int horizontalCount = 1;
        
        // Count left
        int leftX = x - 1;
        while (leftX >= 0 && tiles[leftX, y] != null && tiles[leftX, y].tileType == type)
        {
            horizontalCount++;
            leftX--;
        }
        
        // Count right
        int rightX = x + 1;
        while (rightX < width && tiles[rightX, y] != null && tiles[rightX, y].tileType == type)
        {
            horizontalCount++;
            rightX++;
        }
        
        if (horizontalCount >= 3) return true;
        
        // Check vertical
        int verticalCount = 1;
        
        // Count down
        int downY = y - 1;
        while (downY >= 0 && tiles[x, downY] != null && tiles[x, downY].tileType == type)
        {
            verticalCount++;
            downY--;
        }
        
        // Count up
        int upY = y + 1;
        while (upY < height && tiles[x, upY] != null && tiles[x, upY].tileType == type)
        {
            verticalCount++;
            upY++;
        }
        
        return verticalCount >= 3;
    }
}

