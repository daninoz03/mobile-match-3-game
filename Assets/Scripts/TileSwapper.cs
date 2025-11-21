using UnityEngine;
using System.Collections;

/// <summary>
/// Handles tile swapping logic and validation
/// </summary>
public class TileSwapper : MonoBehaviour
{
    /// <summary>
    /// Check if two tiles are adjacent (horizontal or vertical only)
    /// </summary>
    public bool AreAdjacent(Tile tile1, Tile tile2)
    {
        if (tile1 == null || tile2 == null) return false;
        
        int xDiff = Mathf.Abs(tile1.xIndex - tile2.xIndex);
        int yDiff = Mathf.Abs(tile1.yIndex - tile2.yIndex);
        
        // Adjacent means exactly 1 space apart in one direction, 0 in the other
        return (xDiff == 1 && yDiff == 0) || (xDiff == 0 && yDiff == 1);
    }
    
    /// <summary>
    /// Swap positions of two tiles with animation
    /// </summary>
    public IEnumerator SwapTilePositions(Tile tile1, Tile tile2, Tile[,] tiles, float duration)
    {
        if (tile1 == null || tile2 == null) yield break;
        
        // Store original positions
        int tile1X = tile1.xIndex;
        int tile1Y = tile1.yIndex;
        int tile2X = tile2.xIndex;
        int tile2Y = tile2.yIndex;
        
        Vector3 tile1WorldPos = tile1.transform.position;
        Vector3 tile2WorldPos = tile2.transform.position;
        
        // Animate the swap
        float elapsed = 0f;
        Vector3 tile1Start = tile1.transform.position;
        Vector3 tile2Start = tile2.transform.position;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            
            tile1.transform.position = Vector3.Lerp(tile1Start, tile2WorldPos, t);
            tile2.transform.position = Vector3.Lerp(tile2Start, tile1WorldPos, t);
            
            yield return null;
        }
        
        // Ensure final positions
        tile1.transform.position = tile2WorldPos;
        tile2.transform.position = tile1WorldPos;
        
        // Update grid data
        tiles[tile1X, tile1Y] = tile2;
        tiles[tile2X, tile2Y] = tile1;
        
        // Update tile indices
        tile1.xIndex = tile2X;
        tile1.yIndex = tile2Y;
        tile2.xIndex = tile1X;
        tile2.yIndex = tile1Y;
    }
    
    /// <summary>
    /// Check if swapping two tiles would create a match
    /// </summary>
    public bool WouldCreateMatch(Tile tile1, Tile tile2, Tile[,] tiles, int width, int height, MatchDetector matchDetector)
    {
        if (tile1 == null || tile2 == null || matchDetector == null) return false;
        
        // Temporarily swap in data
        int tile1X = tile1.xIndex;
        int tile1Y = tile1.yIndex;
        int tile2X = tile2.xIndex;
        int tile2Y = tile2.yIndex;
        
        tiles[tile1X, tile1Y] = tile2;
        tiles[tile2X, tile2Y] = tile1;
        
        int tempX = tile1.xIndex;
        int tempY = tile1.yIndex;
        tile1.xIndex = tile2.xIndex;
        tile1.yIndex = tile2.yIndex;
        tile2.xIndex = tempX;
        tile2.yIndex = tempY;
        
        // Check for matches
        bool hasMatch = matchDetector.IsTileInMatch(tile1, tiles, width, height) ||
                       matchDetector.IsTileInMatch(tile2, tiles, width, height);
        
        // Swap back
        tiles[tile1X, tile1Y] = tile1;
        tiles[tile2X, tile2Y] = tile2;
        
        tile1.xIndex = tile1X;
        tile1.yIndex = tile1Y;
        tile2.xIndex = tile2X;
        tile2.yIndex = tile2Y;
        
        return hasMatch;
    }
}

