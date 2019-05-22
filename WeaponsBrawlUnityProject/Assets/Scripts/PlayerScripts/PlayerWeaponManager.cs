using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWeaponManager : NetworkBehaviour {

    public List<AbstractWeaponGeneric> Weapons = new List<AbstractWeaponGeneric>();
    private AbstractWeaponGeneric CurrentWeapon;
    public GameObject throwingChargeBar;
    public int AxeSpeed=10;

    private GameObject Axe;
    private GameObject FirePoint;
    private GameObject Pivot;

    private void Start()
    {
        CmdSwitchWeapon(0);
        throwingChargeBar.SetActive(false);

        Axe = transform.Find("FirePointPivot/Axe").gameObject;
        FirePoint = transform.Find("FirePointPivot/FirePoint").gameObject;
        Pivot = transform.Find("FirePointPivot").gameObject;
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


            if (Input.GetKeyDown(KeyCode.Q))
            {
                CmdActivateAxe(true);
                Pivot.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                StartCoroutine(SwingAxe());
            }
                
        }
    }

    IEnumerator SwingAxe()
    {
        while (Pivot.transform.localRotation.eulerAngles.z <= 90 || Pivot.transform.localRotation.eulerAngles.z >300)
        {
            Pivot.transform.Rotate(0f, 0f, -10f * AxeSpeed * Time.deltaTime);
            yield return 0;
        }
        CmdActivateAxe(false);
    }

    [Command]
    void CmdActivateAxe(bool active)
    {
        RpcActivateAxe(active);
    }

    [ClientRpc]
    private void RpcActivateAxe(bool active)
    {
        ActivateAxe(active);
    }

    void ActivateAxe(bool active)
    {
        Axe.SetActive(active);
        FirePoint.SetActive(!active);
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
