using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWeaponManager : NetworkBehaviour {

    public Weapon1CarrotScript Weapon;

    private void Start()
    {
        Weapon = transform.Find("FirePointPivot/FirePoint/Weapon1Carrot").GetComponent<Weapon1CarrotScript>();
    }


    protected void Update()
    {

        if (hasAuthority)
            if (Input.GetButtonDown("Fire1"))
                Weapon.CmdShoot();

    }
}
