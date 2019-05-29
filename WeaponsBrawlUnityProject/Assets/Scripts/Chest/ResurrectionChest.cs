using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionChest : AbstractChest {


    internal override void DoSomething(PlayerChestManager p)
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool IsInteractable(PlayerChestManager p)
    {
        Color team = p.gameObject.GetComponent<PlayerManager>().GetTeam();
        return base.IsInteractable(p) && (MatchManager._instance.PlayerDead(team)>0);

    }

}
