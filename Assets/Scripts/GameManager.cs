using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main game manager - handles game state, score, and UI
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    public int moveLimit = 30;
    public int targetScore = 1000;
    
    [Header("Scoring")]
    public int match3Score = 100;
    public int match4Score = 200;
    public int match5Score = 300;
    
    [Header("UI References")]
    public Text scoreText;
    public Text movesText;
    public Text gameOverText;
    
    private int currentScore = 0;
    private int movesRemaining;
    private GameState gameState = GameState.Playing;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        movesRemaining = moveLimit;
        UpdateUI();
        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// Add score to the player's total
    /// </summary>
    public void AddScore(int matchCount)
    {
        int scoreToAdd = 0;
        
        if (matchCount == 3)
            scoreToAdd = match3Score;
        else if (matchCount == 4)
            scoreToAdd = match4Score;
        else if (matchCount >= 5)
            scoreToAdd = match5Score;
        
        currentScore += scoreToAdd;
        UpdateUI();
        CheckWinCondition();
    }
    
    /// <summary>
    /// Decrease move count after a valid move
    /// </summary>
    public void UseMove()
    {
        if (gameState != GameState.Playing) return;
        
        movesRemaining--;
        UpdateUI();
        
        if (movesRemaining <= 0)
        {
            CheckLoseCondition();
        }
    }
    
    /// <summary>
    /// Update UI elements
    /// </summary>
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
        
        if (movesText != null)
        {
            movesText.text = $"Moves: {movesRemaining}";
        }
    }
    
    /// <summary>
    /// Check if player has won
    /// </summary>
    void CheckWinCondition()
    {
        if (currentScore >= targetScore && gameState == GameState.Playing)
        {
            gameState = GameState.Won;
            ShowGameOver("You Win!");
        }
    }
    
    /// <summary>
    /// Check if player has lost
    /// </summary>
    void CheckLoseCondition()
    {
        if (movesRemaining <= 0 && currentScore < targetScore)
        {
            gameState = GameState.Lost;
            ShowGameOver("Game Over!");
        }
    }
    
    /// <summary>
    /// Display game over message
    /// </summary>
    void ShowGameOver(string message)
    {
        if (gameOverText != null)
        {
            gameOverText.text = message;
            gameOverText.gameObject.SetActive(true);
        }
        
        Debug.Log(message + $" Final Score: {currentScore}");
    }
    
    /// <summary>
    /// Restart the game
    /// </summary>
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
    
    public int GetCurrentScore()
    {
        return currentScore;
    }
    
    public GameState GetGameState()
    {
        return gameState;
    }
}

/// <summary>
/// Possible game states
/// </summary>
public enum GameState
{
    Playing,
    Won,
    Lost,
    Paused
}

