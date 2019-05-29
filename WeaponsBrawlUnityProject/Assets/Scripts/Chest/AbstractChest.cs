using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public abstract class AbstractChest : NetworkBehaviour {

    public int level;
    public CircleCollider2D playerNextToRay;

    public void Interact(PlayerChestManager p)
    {
        if (IsInteractable(p))
        {
            DoSomething(p);
            RpcDestroy();
        }
        
        //Destroy(gameObject);
    }

    [ClientRpc]
    public void RpcDestroy()
    {
        Destroy(gameObject);
    }

    internal abstract void DoSomething(PlayerChestManager p);

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



    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
