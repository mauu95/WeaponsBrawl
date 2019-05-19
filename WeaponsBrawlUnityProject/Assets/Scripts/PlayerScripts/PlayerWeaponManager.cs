using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWeaponManager : NetworkBehaviour {

    public AbstractWeaponGeneric Weapon;

    private void Start()
    {
        Weapon = transform.Find("FirePointPivot/FirePoint/Weapon1Carrot").GetComponent<AbstractWeaponGeneric>();
    }


    protected void Update()
    {

        if (hasAuthority)
            if (Input.GetButtonDown("Fire1"))
                CmdAttack();

    }

    [Command]
    public void CmdAttack()
    {
        Weapon.Attack();
    }
}
