using UnityEngine;

public class OrcAnimatorController : MonoBehaviour
{
    private Animator animator;
    private EnemyStats enemyStats;

    public Transform player; 
    public float attackRange = 2f;
    public float damage = 10f; 
    public float attackCooldown = 1f;

    private bool isAttacking;
    private float attackTimer; 

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        CheckAttackRange();
        HandleAttackCooldown();
    }

    void CheckAttackRange()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        isAttacking = distanceToPlayer <= attackRange;
        animator.SetBool("IsAttacking", isAttacking);

        if (isAttacking && attackTimer <= 0f && distanceToPlayer <= 1)
        {
            DealDamageToPlayer();
        }
    }

    void HandleAttackCooldown()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime; 
        }
    }

    void DealDamageToPlayer()
    {
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);

            Debug.Log($"Orc attacked! Player took {damage} damage. Current health: {playerStats.CurrentHealth}/{playerStats.MaxHealth}");

            attackTimer = attackCooldown;
        }
        else
        {
            Debug.LogError("PlayerStats component not found on the player!");
        }
    }
}
