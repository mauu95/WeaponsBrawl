using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWeaponManager : NetworkBehaviour {

    public List<AbstractWeaponGeneric> Weapons = new List<AbstractWeaponGeneric>();
    private AbstractWeaponGeneric CurrentWeapon;
    public GameObject throwingChargeBar;

    private void Start()
    {
        SwitchWeapon(0);
    }


    protected void Update()
    {

        if (hasAuthority)
            if (Input.GetButtonDown("Fire1"))
                CmdAttack(throwingChargeBar.GetComponent<ThrowingPowerBarScript>().Charge);
        if (hasAuthority)
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchWeapon(1);
        if (hasAuthority)
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchWeapon(0);


    }

    [Command]
    public void CmdAttack(int charge)
    {
        CurrentWeapon.Attack(charge);
    }

    public void SwitchWeapon(int id)
    {
        CurrentWeapon = Weapons[id];
    }
}
