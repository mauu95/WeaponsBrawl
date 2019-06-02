using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInterfaceUI : AbstractInGameInterfaces {

    public BuildingController buildManager;

	void Update () {

        if (Input.GetKeyDown(KeyCode.C))
            OpenClose();  

    }

    public override void OpenClose()
    {
        base.OpenClose();
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
