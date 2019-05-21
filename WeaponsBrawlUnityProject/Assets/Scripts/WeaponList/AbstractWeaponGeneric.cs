using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AbstractWeaponGeneric : NetworkBehaviour {

    protected Transform firePoint;

    public Transform Player;


    protected void Awake()
    {
        firePoint = Player.Find("FirePointPivot/FirePoint");
    }

    public abstract void Attack();
}