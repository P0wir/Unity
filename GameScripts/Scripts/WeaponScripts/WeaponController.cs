using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    public GameObject prefab;
    float currentCooldown;

    protected PlayerMovement pm;

    protected virtual void Start()
    {
        pm = Object.FindFirstObjectByType<PlayerMovement>();
        currentCooldown = weaponData.cooldown;
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.cooldown;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }
}
