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
    public int timeToRepairAfterAttack = 5;
    private bool canAttack = true;

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
            if (canAttack)
            {
                if (Input.GetButtonDown("Fire1"))
                    throwingChargeBar.SetActive(true);

                if (Input.GetButtonUp("Fire1"))
                {
                    CmdAttack(throwingChargeBar.GetComponent<ThrowingPowerBarScript>().Charge);
                    canAttack = false;
                    StartCoroutine(gameObject.GetComponent<PlayerManager>().LockAfterSec(timeToRepairAfterAttack));
                    throwingChargeBar.SetActive(false);
                }
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

    private void AddWeapon(GameObject weaponToAdd, GameObject player)
    {
        GameObject localFirePoint = player.transform.Find("FirePointPivot/FirePoint").gameObject;
        GameObject localPivot = player.transform.Find("FirePointPivot").gameObject;
        weaponToAdd.transform.SetParent(localFirePoint.transform);
        weaponToAdd.transform.localScale = Vector3.one;
        weaponToAdd.transform.rotation = localPivot.transform.parent.gameObject.transform.rotation;
        weaponToAdd.transform.position = localFirePoint.transform.position;
        weaponToAdd.GetComponent<AbstractWeaponGeneric>().SetPlayer(player);

        Weapons.Add(weaponToAdd.GetComponent<AbstractWeaponGeneric>());
        inventoryUI.UpdateUI();
    }


















    [Command]
    public void CmdAddWeapon(GameObject weaponToAdd, GameObject player)
    {
        RpcAddWeapon(weaponToAdd, player);
    }

    [ClientRpc]
    private void RpcAddWeapon(GameObject weaponToAdd, GameObject player)
    {
        AddWeapon(weaponToAdd, player);
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

    void OnDisable()
    {
        throwingChargeBar.SetActive(false);
    }

    void OnEnable()
    {
        canAttack = true;
    }

}
