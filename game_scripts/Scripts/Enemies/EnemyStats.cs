using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private OrcAnimatorController animatorController;

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

    void Awake()
    {
        currentMoveSpeed = enemyData.moveSpeed;
        currentHealth = enemyData.maxHp;
        currentDamage = enemyData.damage;

        animatorController = GetComponent<OrcAnimatorController>();
        if (animatorController == null)
        {
            Debug.LogError("OrcAnimatorController is not attached to the GameObject!");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to the GameObject!");
        }
    }

    void Update()
    {
        if (damageCooldownTimer > 0f)
        {
            damageCooldownTimer -= Time.deltaTime;
        }
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
            playerLevelManager.AddExp(enemyData.exp);
            Debug.Log($"Granted {enemyData.exp} EXP to the player!");
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
}
