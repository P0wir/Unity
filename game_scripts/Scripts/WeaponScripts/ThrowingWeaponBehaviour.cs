using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
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

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // left
        {
            scale.x *= -1;
            scale.y *= -1;
        }
        else if (dirx == 0 && diry < 0) // down
        {
            scale.y *= -1;
        }
        else if (dirx == 0 && diry > 0) // up
        {
            scale.x *= -1;
        }
        else if (dirx > 0 && diry > 0) // right up
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry < 0) // right down
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) // left up
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry < 0) // left down
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                Debug.Log($"Weapon hits enemy. Weapon Damage: {currentDamage}");
                enemy.TakeDamage(currentDamage);
                ReducePierce();
            }
            else
            {
                Debug.LogError("EnemyStats not found on collided object!");
            }
        }
        else
        {
            Renderer colRenderer = col.GetComponent<Renderer>();
            if (colRenderer != null)
            {
                string sortingLayerName = colRenderer.sortingLayerName;
                if (sortingLayerName == "Foreground" || sortingLayerName == "Decorations")
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
