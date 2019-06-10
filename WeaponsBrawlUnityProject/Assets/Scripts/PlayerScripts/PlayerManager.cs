using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerManager : NetworkBehaviour {
    [SyncVar]
    public GameObject controller;

    public bool isInTurn;
    public List<MonoBehaviour> scriptToDisable;

    private void Start()
    {
        if (hasAuthority)
        {
            InventoryUI inventory = GetGameObjectInRoot("Canvas").GetComponent<InventoryUI>();
            BuildInterfaceUI build = GetGameObjectInRoot("Canvas").GetComponent<BuildInterfaceUI>();
            ResurrectionMenuUI resurrection = GetGameObjectInRoot("Canvas").GetComponent<ResurrectionMenuUI>();
            inventory.InitializeInventoryUI(this.gameObject);
            build.InitializeInventoryUI(this.gameObject);
            resurrection.InizializeInventoryUI(this.gameObject);

            //CmdSetTeam();
        }
        SetSpriteColor();
    }

    void SetSpriteColor()
    {
        GetComponent<SpriteRenderer>().color = GetTeam();
    }
    
    public IEnumerator LockAfterSec(int sec)
    {
        TimerAfterAttackScript.SetTimer(sec);
        yield return new WaitForSeconds(sec);
        ChangeActiveStatus(false);
    }
   
    [ClientRpc]
    public void RpcChangeActiveStatus(bool active)
    {
        ChangeActiveStatus(active);
    }

    public void ChangeActiveStatus(bool active)
    {
        if (!active)
        {
            CmdNotifyTurnEnd();
        }
        isInTurn = active;
        foreach (MonoBehaviour c in scriptToDisable)
        {
            c.enabled = active;
        }
    }

    [Command]
    public void CmdNotifyTurnEnd()
    {
        MatchManager._instance.NotifyTurnEnd(this.controller.GetComponent<PlayerInfo>());
    }

    internal Color GetTeam()
    {
        return controller.GetComponent<PlayerInfo>().team;
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
