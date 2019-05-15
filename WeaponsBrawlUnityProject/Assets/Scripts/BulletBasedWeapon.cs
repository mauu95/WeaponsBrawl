using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public abstract class BulletBasedWeapon : NetworkBehaviour
{

    private Transform firePoint;

    public GameObject bulletPrefab;

    protected void Awake()
    {
        firePoint = transform.Find("FirePointPivot/FirePoint");
    }

    protected void Update()
    {

        if (hasAuthority)
            if (Input.GetButtonDown("Fire1"))
                CmdShoot();

    }



    [Command]
    protected void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
    }



}