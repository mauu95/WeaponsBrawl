using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public abstract class AbstractChest : NetworkBehaviour {

    public int level;
    public CircleCollider2D playerNextToRay;
    public CircleCollider2D interactionRay;


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

    public bool IsInteractable(PlayerChestManager p)
    {
        if (NumberOfPlayerIntheRay() >= level)
        {
            return true;
        }
        return false;
    }

    private int NumberOfPlayerIntheRay()
    {
        return playerNextToRay.GetComponent<PlayerCounter>().GetPlayerCounter();
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
