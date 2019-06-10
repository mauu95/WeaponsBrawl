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
        StartCoroutine("SpawnPlayerWithDelay");
    }

    [Command]
    public void CmdSpawnPlayer()
    {
        player = (GameObject)Instantiate(playerToSpawn, transform);
        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
        RpcSetController(player);
        this.gameObject.GetComponent<PlayerInfo>().status = PlayerInfo.Status.alive;
        RpcSetCameraFollow(player);

    }

    [ClientRpc]
    public void RpcSetController(GameObject p)
    {

        PlayerManager playerManager = p.GetComponent<PlayerManager>();
        PlayerInfo playerInfo = this.GetComponent<PlayerInfo>();
        playerManager.controller = gameObject;
        playerInfo.physicalPlayer = p;
        playerManager.ChangeActiveStatus(MatchManager._instance.turn == playerInfo.team);
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

    IEnumerator SpawnPlayerWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
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





}