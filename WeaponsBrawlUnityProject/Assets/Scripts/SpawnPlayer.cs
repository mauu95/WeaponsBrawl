using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SpawnPlayer : NetworkBehaviour {

    private GameObject player;
    public GameObject playerToSpawn;
    public GameObject mainCamera;
    public GameObject  virtualCamera;
	
    // Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            CmdSpawnPlayer();
            mainCamera.SetActive(true);
            virtualCamera.SetActive(true);
        }
        else
        {
            mainCamera.SetActive(false);
            virtualCamera.SetActive(false);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    [Command]
    public void CmdSpawnPlayer()
    {
        player = (GameObject)Instantiate(playerToSpawn, transform);
        //CameraSetup cs = player.GetComponent<CameraSetup>();
        //cs.virtualCamera = this.virtualCamera;

        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
        RpcSetCameraFollow(player);
       
    }
    [ClientRpc]
    public void RpcSetCameraFollow(GameObject p)
    {
        if (isLocalPlayer)
        {
            Cinemachine.CinemachineVirtualCamera virtualController = virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            virtualController.m_Follow = p.transform;
        }
    }
}
