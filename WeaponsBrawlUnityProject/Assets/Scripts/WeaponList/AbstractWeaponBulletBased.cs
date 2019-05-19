using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public abstract class AbstractWeaponBulletBased : NetworkBehaviour
{
    private Transform firePoint;

    public Transform Player;
    public GameObject bulletPrefab;

    protected void Awake()
    {
        firePoint = Player.Find("FirePointPivot/FirePoint");
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
    }

    public void Attack()
    {
        Shoot();
    }



}