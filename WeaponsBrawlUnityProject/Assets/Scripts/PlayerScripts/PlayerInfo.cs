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

    // Use this for initialization
    void Start () {
        MatchManager._instance.AddPlayer(this);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
