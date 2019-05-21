using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public abstract class AbstractWeaponBulletBased : AbstractWeaponGeneric
{
    public GameObject bulletPrefab;
    public GameObject throwingChargeBar;

    public override void Attack()
    {
        Shoot();
    }


    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<AbstractBulletExplosive>().speed *= throwingChargeBar.GetComponent<ThrowingPowerBarScript>().Charge / 100f;
        NetworkServer.Spawn(bullet);
    }



}