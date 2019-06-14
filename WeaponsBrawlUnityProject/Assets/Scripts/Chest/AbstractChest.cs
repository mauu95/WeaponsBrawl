﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public abstract class AbstractChest : NetworkBehaviour {

    public int level;
    public CircleCollider2D playerNextToRay;

    internal abstract bool DoSomething(PlayerChestManager p);

    public virtual void ClientPreInteract(PlayerChestManager p)
    {
        p.waitingUser = false;
        return;
    }

    public void Interact(PlayerChestManager p)
    {
        if (IsInteractable(p))
            if (DoSomething(p))
                RpcDestroy();
            
    }

    [ClientRpc]
    public void RpcDestroy()
    {
        Destroy(gameObject);
    }

    public virtual bool IsInteractable(PlayerChestManager p)
    {
        Color team= p.gameObject.GetComponent<PlayerManager>().GetTeam();
        if (NumberOfPlayerIntheRay(team) >= level)
        {
            return true;
        }
        return false;
    }

    private int NumberOfPlayerIntheRay(Color team)
    {
        return playerNextToRay.GetComponent<PlayerCounter>().GetPlayerCounter(team);
    }


}
