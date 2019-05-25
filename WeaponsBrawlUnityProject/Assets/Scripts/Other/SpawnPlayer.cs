using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SpawnPlayer : NetworkBehaviour
{
    public GameObject playerToSpawn;
    public GameObject mainCamera;
    public GameObject virtualCamera;

    private GameObject player;

    void Start()
    {
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

    [Command]
    public void CmdSpawnPlayer()
    {
        player = (GameObject)Instantiate(playerToSpawn, transform);
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