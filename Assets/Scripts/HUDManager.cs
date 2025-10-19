using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] private GameObject livesContainer;
    [SerializeField] private Sprite lifeSprite;
    [SerializeField] private int startingLives = 3;
    private List<Image> lifeImages = new List<Image>();
    
    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;
    private int currentScore = 0;
    
    [Header("Game Timer")]
    [SerializeField] private TMP_Text gameTimerText;
    private float gameTime = 0f;
    private bool isGameRunning = false;
    
    [Header("Ghost Scared Timer")]
    [SerializeField] private TMP_Text ghostScaredTimerText;
    private float ghostScaredTime = 0f;
    private bool isGhostScared = false;
    
    [Header("Exit Button")]
    [SerializeField] private Button exitButton;
    [SerializeField] private string startSceneName = "StartScene";
    
    [Header("Level Name")]
    [SerializeField] private TMP_Text levelNameText;
    [SerializeField] private string levelName = "Level 1";
    
    void Start()
    {
        SetupLives();
        UpdateScore(0);
        UpdateGameTimer();
        UpdateGhostScaredTimer();
        SetupExitButton();
        SetupLevelName();
    }
    
    void Update()
    {
        if (isGameRunning)
        {
            gameTime += Time.deltaTime;
            UpdateGameTimer();
        }
        
        if (isGhostScared)
        {
            ghostScaredTime -= Time.deltaTime;
            if (ghostScaredTime <= 0)
            {
                ghostScaredTime = 0;
                isGhostScared = false;
            }
            UpdateGhostScaredTimer();
        }
    }
    
    void SetupLives()
    {
        if (livesContainer == null || lifeSprite == null) return;
        
        for (int i = 0; i < startingLives; i++)
        {
            GameObject lifeObj = new GameObject($"Life_{i}");
            lifeObj.transform.SetParent(livesContainer.transform);
            
            Image lifeImage = lifeObj.AddComponent<Image>();
            lifeImage.sprite = lifeSprite;
            lifeImage.preserveAspect = true;
            
            RectTransform rect = lifeObj.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            
            lifeImages.Add(lifeImage);
        }
    }
    
    void SetupExitButton()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitToStartScene);
        }
    }
    
    void SetupLevelName()
    {
        if (levelNameText != null)
        {
            levelNameText.text = levelName;
        }
    }
    
    void UpdateGameTimer()
    {
        if (gameTimerText == null) return;
        
        int minutes = Mathf.FloorToInt(gameTime / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);
        int milliseconds = Mathf.FloorToInt((gameTime * 100f) % 100f);
        
        gameTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
    
    void UpdateGhostScaredTimer()
    {
        if (ghostScaredTimerText == null) return;
        
        if (isGhostScared && ghostScaredTime > 0)
        {
            ghostScaredTimerText.gameObject.SetActive(true);
            ghostScaredTimerText.text = Mathf.CeilToInt(ghostScaredTime).ToString();
        }
        else
        {
            ghostScaredTimerText.gameObject.SetActive(false);
        }
    }
    
    // Public methods to control the HUD
    
    public void StartGame()
    {
        isGameRunning = true;
    }
    
    public void StopGame()
    {
        isGameRunning = false;
    }
    
    public void UpdateScore(int score)
    {
        currentScore = score;
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString("D6");
        }
    }
    
    public void AddScore(int points)
    {
        UpdateScore(currentScore + points);
    }
    
    public void SetLives(int lives)
    {
        for (int i = 0; i < lifeImages.Count; i++)
        {
            lifeImages[i].enabled = i < lives;
        }
    }
    
    public void RemoveLife()
    {
        for (int i = lifeImages.Count - 1; i >= 0; i--)
        {
            if (lifeImages[i].enabled)
            {
                lifeImages[i].enabled = false;
                break;
            }
        }
    }
    
    public void StartGhostScaredTimer(float duration)
    {
        isGhostScared = true;
        ghostScaredTime = duration;
        UpdateGhostScaredTimer();
    }
    
    public void ExitToStartScene()
    {
        SceneManager.LoadScene(startSceneName);
    }
}