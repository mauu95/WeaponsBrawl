using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : NetworkBehaviour {
    public enum Status {dead, alive};
    [SyncVar]
    public string pname;
    [SyncVar]
    public Color team;
    [SyncVar]
    public Status status;
    [SyncVar]
    public GameObject physicalPlayer;
    [SyncVar]
    public int kills=0;
    [SyncVar]
    public int damageToEnemy = 0;
    [SyncVar]
    public int resurrectedAlly = 0;
    [SyncVar]
    public bool win = false;
    [SyncVar]
    public int deaths = 0;
    [SyncVar]
    public int damageToAlly = 0;
    [SyncVar]
    public int allyEliminated = 0;


    // Use this for initialization
    void Start () {
        MatchManager._instance.AddPlayer(this);	
	}
	

    [Command]
    internal void CmdResurrect()
    {
        PlayerHealth ph= physicalPlayer.GetComponent<PlayerHealth>();
        ph.CmdGetLife(ph.maxHealth);
        RpcRestoreUser();
    }

    [ClientRpc]
    public void RpcRestoreUser()
    {
        physicalPlayer.SetActive(true);
    }

    public int GetPoints()
    {
        float points = 0;
        points += 0.1f * damageToEnemy;
        points += 20f * kills;
        points += 20f * resurrectedAlly;
        points -= 0.5f * damageToAlly;
        points -= 20f * deaths;
        points -= 20f * allyEliminated;
        if (win)
            points += 50;
        points = Mathf.Max(points, 0);
        return Mathf.FloorToInt(points);       
    }
}
