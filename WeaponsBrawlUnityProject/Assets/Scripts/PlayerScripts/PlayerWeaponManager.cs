using System;
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
        CmdSwitchWeapon(0);
    }


    protected void Update()
    {

        if (hasAuthority)
            if (Input.GetButtonDown("Fire1"))
                CmdAttack(throwingChargeBar.GetComponent<ThrowingPowerBarScript>().Charge);
        if (hasAuthority)
            if (Input.GetKeyDown(KeyCode.Alpha2))
                CmdSwitchWeapon(1);
        if (hasAuthority)
            if (Input.GetKeyDown(KeyCode.Alpha1))
                CmdSwitchWeapon(0);


    }

    [Command]
    public void CmdAttack(int charge)
    {
        CurrentWeapon.Attack(charge);
    }

    [Command]
    public void CmdSwitchWeapon(int id)
    {
        RpcSwitchWeapon(id);
    }

    [ClientRpc]
    private void RpcSwitchWeapon(int id)
    {
        SwitchWeapon(id);
    }

    public void SwitchWeapon(int id)
    {
        if(CurrentWeapon)
            CurrentWeapon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        CurrentWeapon = Weapons[id];
        CurrentWeapon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
