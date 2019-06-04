using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWeaponManager_Inventory : NetworkBehaviour {

    //Inventory Stuff
    public List<AbstractWeaponGeneric> Weapons = new List<AbstractWeaponGeneric>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    //Weapon Manager Stuff
    public GameObject throwingChargeBar;
    public int AxeSpeed=10;

    private InventoryUI inventoryUI;
    private AbstractWeaponGeneric CurrentWeapon;
    private GameObject Axe;
    private GameObject FirePoint;
    private GameObject Pivot;


    public void Add(AbstractWeaponGeneric weapon)
    {
        Weapons.Add(weapon);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
        
    private void Start()
    {
        CmdSwitchWeapon(0);
        SwitchWeapon(0);
        throwingChargeBar.SetActive(false);

        Axe = transform.Find("FirePointPivot/Axe").gameObject;
        FirePoint = transform.Find("FirePointPivot/FirePoint").gameObject;
        Pivot = transform.Find("FirePointPivot").gameObject;
        inventoryUI = FindObjectOfType<InventoryUI>();
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

    void ActivateAxe(bool active)
    {
        Axe.SetActive(active);
        FirePoint.SetActive(!active);
    }

    public void SwitchWeapon(int id)
    {
        if(CurrentWeapon)
            CurrentWeapon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        CurrentWeapon = Weapons[id];
        CurrentWeapon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator SwingAxe()
    {
        while (Pivot.transform.localRotation.eulerAngles.z <= 90 || Pivot.transform.localRotation.eulerAngles.z > 300)
        {
            Pivot.transform.Rotate(0f, 0f, -10f * AxeSpeed * Time.deltaTime);
            yield return 0;
        }
        CmdActivateAxe(false);
    }

    public void AddWeapon(GameObject weaponToAdd)
    {
        weaponToAdd.transform.SetParent(FirePoint.transform);
        weaponToAdd.transform.localScale = Vector3.one;
        weaponToAdd.transform.rotation = Pivot.transform.parent.gameObject.transform.rotation;
        weaponToAdd.transform.position = FirePoint.transform.position;
        weaponToAdd.GetComponent<AbstractWeaponGeneric>().Player = this.transform;
        weaponToAdd.SetActive(true);
        Weapons.Add(weaponToAdd.GetComponent<AbstractWeaponGeneric>());
        inventoryUI.UpdateUI();
    }





















    [Command]
    public void CmdAttack(int charge)
    {
        CurrentWeapon.Attack(charge);
    }

    [Command]
    void CmdActivateAxe(bool active)
    {
        RpcActivateAxe(active);
    }

    [ClientRpc]
    void RpcActivateAxe(bool active)
    {
        ActivateAxe(active);
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
}
