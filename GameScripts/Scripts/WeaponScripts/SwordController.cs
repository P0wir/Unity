using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedSword = Instantiate(weaponData.prefab);
        spawnedSword.transform.position = transform.position;
        spawnedSword.GetComponent<SwordBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
