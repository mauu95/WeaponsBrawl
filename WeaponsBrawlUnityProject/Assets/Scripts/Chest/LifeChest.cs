using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LifeChest : AbstractChest {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    override
    internal void DoSomething(PlayerChestManager p)
    {
        p.LifeChest(25);
    }
}
