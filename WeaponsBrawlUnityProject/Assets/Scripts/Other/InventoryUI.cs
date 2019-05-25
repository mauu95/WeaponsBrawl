using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject Player;
    public GameObject SlotPrefab;

    private PlayerWeaponManager_Inventory inventory;
    private InventorySlot[] slots;
    private int Size = 1; // 2 Initial weapon 

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
            OpenCloseInventory();
    }

    public void OpenCloseInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    private void UpdateUI()
    {
        int i = 0;
        foreach(AbstractWeaponGeneric weapon in inventory.Weapons)
        {
            if (i > Size)
            {
                addSlot();
                Size++;
            }
            slots[i].AddItem(weapon);
            i++;
        }
    }

    public void addSlot()
    {
        GameObject newSlot = Instantiate(SlotPrefab);
        newSlot.transform.SetParent(itemsParent);
        newSlot.transform.localScale = Vector3.one;
    }
}
