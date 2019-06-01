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

    // Use this for initialization
    void Start () {
        MatchManager._instance.AddPlayer(this);	
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
