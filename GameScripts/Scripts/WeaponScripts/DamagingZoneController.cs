using UnityEngine;

public class DamagingZoneController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedDamagingZone = Instantiate(weaponData.prefab);
        spawnedDamagingZone.transform.position = transform.position;
    }
}
