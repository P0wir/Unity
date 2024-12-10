using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;

    private float currentHealth;
    private PlayerLevelManager levelManager;
    private GameManager gameManager;
    public Slider healthBar; 

    public float CurrentHealth => currentHealth; 
    public float MaxHealth => playerData.maxHp; 

    void Start()
    {
        currentHealth = playerData.maxHp;
        levelManager = GetComponent<PlayerLevelManager>();

        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
        if (healthBar != null)
        {
            healthBar.maxValue = playerData.maxHp;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage! Current Health: {currentHealth}/{playerData.maxHp}");

        if (healthBar != null)
        {
            healthBar.value = currentHealth; 
        }

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;

        if (gameManager != null)
        {
            gameManager.PlayerDied();
        }
    }
}
