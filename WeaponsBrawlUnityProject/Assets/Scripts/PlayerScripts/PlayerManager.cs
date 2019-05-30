using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerManager : NetworkBehaviour {
    [SyncVar]
    public GameObject controller;

    private void Start()
    {
        if (hasAuthority)
        {
            InventoryUI inventory = GetGameObjectInRoot("Canvas").GetComponent<InventoryUI>();
            BuildInterfaceUI build = GetGameObjectInRoot("Canvas").GetComponent<BuildInterfaceUI>();
            inventory.InitializeInventoryUI(this.gameObject);
            build.InitializeInventoryUI(this.gameObject);
        }
    }

    public void ActivateMovementAfterSec()
    {
        StartCoroutine(ActivateMovement());
    }

    internal Color GetTeam()
    {
        return controller.GetComponent<PlayerInfo>().team;
    }

    IEnumerator ActivateMovement()
    {
        while (gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            yield return 0;
        this.GetComponent<PlayerMovement>().enabled = true;
    }


    private GameObject GetGameObjectInRoot(string objname)
    {
        GameObject[] root = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in root)
            if (obj.name == objname)
                return obj;
        return null;
    }
    public void PlayerDie()
    {
        if (isServer)
        {
            controller.GetComponent<PlayerInfo>().status=PlayerInfo.Status.dead;
        }
        
    }
}
