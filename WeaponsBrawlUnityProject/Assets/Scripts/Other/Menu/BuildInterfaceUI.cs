using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInterfaceUI : MonoBehaviour {

    public GameObject buildInterfaceUI;
    public BuildingController buildManager;

	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OpenCloseBuildInterface();
        }       
    }

    public void OpenCloseBuildInterface()
    {
        buildInterfaceUI.SetActive(!buildInterfaceUI.activeSelf);
        buildManager.ChangeBuildingStatus();
    }

    public void SelectBuilding(int rotation)
    {
        buildManager.zRotation = rotation;
    }

    internal void InitializeInventoryUI(GameObject player)
    {
        buildManager = player.GetComponent<BuildingController>();
    }
}
