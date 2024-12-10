using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private OrcAnimatorController animatorController;
    public Slider healthBar;
    public GameObject healthBarPrefab;

    private float currentMoveSpeed;
    private float currentHealth;
    private float currentDamage;
    private float damageCooldownTimer = 0f;
    public float damageCooldown = 0.2f;

    public AudioClip hitSound; 
    private AudioSource audioSource; 

    public float CurrentHealth => currentHealth;
    public float MaxHealth => enemyData.maxHp;
    public float Exp => enemyData.exp;

    public float currentExp;

    void Start()
    {
        float difficultyMultiplier = EnemyScaleManager.GlobalDifficultyMultiplier;

        currentMoveSpeed = enemyData.moveSpeed;
        currentHealth = enemyData.maxHp * difficultyMultiplier;
        currentDamage = enemyData.damage * difficultyMultiplier;
        currentExp = enemyData.exp * difficultyMultiplier;

        animatorController = GetComponent<OrcAnimatorController>();
        if (animatorController == null)
        {
            Debug.LogError("OrcAnimatorController is not attached to the GameObject!");
        }

        audioSource = GetComponent<AudioSource>();
        if (healthBarPrefab != null)
        {
            GameObject healthBarInstance = Instantiate(healthBarPrefab, transform);
            healthBarInstance.transform.localPosition = new Vector3(0.9f, -2.3f, 0);
            healthBar = healthBarInstance.GetComponentInChildren<Slider>();

            if (healthBar != null)
            {
                healthBar.maxValue = currentHealth;
                healthBar.value = currentHealth;
            }
        }

    }

    void Update()
    {
        if (damageCooldownTimer > 0f)
        {
            damageCooldownTimer -= Time.deltaTime;
        }

        UpdateHealthBar();
    }

    public void TakeDamage(float dmg)
    {
        if (damageCooldownTimer > 0f)
        {
            return;
        }

        currentHealth -= dmg;
        damageCooldownTimer = damageCooldown;

        PlaySound(hitSound);

        Debug.Log($"Enemy took {dmg} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            if (gameObject.layer == LayerMask.NameToLayer("SpecialBoss"))
            {
                BossKill();
            }
            else
            {
                Kill();
            }
        } 
    }

    public void Kill()
    {
        GrantExpToPlayer();
        Destroy(gameObject);
    }

    private void GrantExpToPlayer()
    {
        PlayerLevelManager playerLevelManager = FindFirstObjectByType<PlayerLevelManager>();
        if (playerLevelManager != null)
        {
            playerLevelManager.AddExp(currentExp);
            Debug.Log($"Granted {currentExp} EXP to the player!");
        }
        else
        {
            Debug.LogError("PlayerLevelManager not found!");
       }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void BossKill()
    {
        GrantExpToPlayer();

        if (gameObject.layer == LayerMask.NameToLayer("SpecialBoss")) 
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.BossKilled();
                Debug.Log("BossKilled called in GameManager.");
            }
        }
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }
}
