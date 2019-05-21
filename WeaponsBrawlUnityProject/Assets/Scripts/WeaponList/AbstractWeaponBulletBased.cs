using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public abstract class AbstractWeaponBulletBased : AbstractWeaponGeneric
{
    public GameObject bulletPrefab;


    public override void Attack()
    {
        Shoot();
    }


    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
    }



}