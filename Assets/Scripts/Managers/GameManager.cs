using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Spawning")]
    public GameObject chickenPrefab;
    public int chickensPerRow = 8;
    public int numberOfRows = 3;
    public float horizontalSpacing = 3f;
    public float verticalSpacing = 3f;
    public float spawnHeight = 20f;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    
    private int score;
    private bool isGameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameManager instance created");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("GameManager Start called");
        StartGame();
    }

    public void StartGame()
    {
        Debug.Log("StartGame called");
        if (chickenPrefab == null)
        {
            Debug.LogError("Chicken prefab is not assigned!");
            return;
        }

        score = 0;
        isGameOver = false;
        UpdateScoreUI();
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        SpawnChickenWave();
    }

    private void SpawnChickenWave()
    {
        Debug.Log($"Spawning wave: {chickensPerRow} chickens per row, {numberOfRows} rows");
        float startX = -(chickensPerRow - 1) * horizontalSpacing * 0.5f;
        
        for (int row = 0; row < numberOfRows; row++)
        {
            for (int col = 0; col < chickensPerRow; col++)
            {
                Vector3 spawnPosition = new Vector3(
                    startX + col * horizontalSpacing,
                    0,
                    spawnHeight - row * verticalSpacing
                );

                GameObject chicken = Instantiate(chickenPrefab, spawnPosition, Quaternion.identity);
                Debug.Log($"Spawned chicken at position: {spawnPosition}");
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }
} 