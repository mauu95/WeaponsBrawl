using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject Player;

    private PlayerWeaponManager_Inventory inventory;
    private InventorySlot[] slots;

    public void InitializeInventoryUI(GameObject player)
    {
        Player = player;
        inventory = Player.GetComponent<PlayerWeaponManager_Inventory>();
        inventory.onItemChangedCallBack += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
            inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    private void UpdateUI()
    {
        int i = 0;
        foreach(AbstractWeaponGeneric weapon in inventory.Weapons)
        {
            slots[i].AddItem(weapon);
            i++;
        }
    }
}
