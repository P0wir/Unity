using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyAfterTime;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldown;
    protected int currentPierce;

    void Awake()
    {
        PlayerLevelManager levelManager = FindFirstObjectByType<PlayerLevelManager>();
        if (levelManager != null)
        {
            currentDamage = levelManager.GetDamage() * weaponData.damage;
        }
        else
        {
            Debug.LogError("PlayerLevelManager not found in the scene!");
            currentDamage = weaponData.damage; 
        }
        currentSpeed = weaponData.speed;
        currentCooldown = weaponData.cooldown;
        currentPierce = weaponData.pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(currentDamage);
            }
        }
    }
}