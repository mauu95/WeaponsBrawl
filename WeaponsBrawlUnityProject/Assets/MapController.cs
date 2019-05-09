using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

public class MapController : NetworkBehaviour {

    public Tilemap map;

    // Use this for initialization
    void Start () {
        map = GetComponent<Tilemap>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    [Command]  
    public void CmdDestroyTile(int x, int y, int z)
    {
        Vector3Int pos = new Vector3Int(x, y, z);
        map.SetTile(pos, null);
        RpcDestroyTile( x,  y,  z);
    }


    [ClientRpc]
    public void RpcDestroyTile(int x, int y, int z)
    {
        Vector3Int pos = new Vector3Int(x, y, z);
        map.SetTile(pos, null);
    }

}
