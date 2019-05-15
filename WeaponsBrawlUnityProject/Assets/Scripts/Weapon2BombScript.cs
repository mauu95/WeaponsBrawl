using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Weapon2BombScript : NetworkBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletOffset;

    private void Awake()
    {
        firePoint = transform.Find("FirePointPivot/FirePoint");
        print(firePoint);
    }

    void Update()
    {

        if (hasAuthority)
            if (Input.GetButtonDown("Fire1"))
                CmdShoot();

    }



    [Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
    }



}
