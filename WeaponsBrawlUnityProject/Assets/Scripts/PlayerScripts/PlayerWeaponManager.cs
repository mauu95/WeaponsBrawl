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
        throwingChargeBar.SetActive(false);
    }


    protected void Update()
    {

        if (hasAuthority)
        {
            if (Input.GetButtonDown("Fire1"))
                throwingChargeBar.SetActive(true);

            if (Input.GetButtonUp("Fire1"))
            {
                CmdAttack(throwingChargeBar.GetComponent<ThrowingPowerBarScript>().Charge);
                throwingChargeBar.SetActive(false);
            }
                



            if (Input.GetKeyDown(KeyCode.Alpha1))
                CmdSwitchWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                CmdSwitchWeapon(1);
        }
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
