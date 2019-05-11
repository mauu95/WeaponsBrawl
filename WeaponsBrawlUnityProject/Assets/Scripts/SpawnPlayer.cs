using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class SpawnPlayer : NetworkBehaviour {

    private GameObject player;
    public GameObject playerToSpawn;
    public GameObject mainCamera;
    public GameObject  virtualCamera;
	
	void Start () {
        if (isLocalPlayer)
        {
            SetServerPlayerParent();
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
        RpcSetParent(player, this.gameObject);
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

    [ClientRpc]
    public void RpcSetParent(GameObject child, GameObject parent)
    {
        child.transform.SetParent(parent.transform);
    }

    public void SetServerPlayerParent()
    {
        var scene = SceneManager.GetActiveScene();
        GameObject[] RootGameObjs = scene.GetRootGameObjects();
        GameObject player = FindInArrayByName(RootGameObjs, "Player(Clone)");
        if (player)
        {
            GameObject playerObject = FindInArrayByName(RootGameObjs, "PlayerObject(Clone)");
            player.transform.SetParent(playerObject.transform);
        }
    }

    GameObject FindInArrayByName(GameObject[] array, string name)
    {
        GameObject result = null;
        foreach(GameObject gmobj in array)
        {
            if (gmobj.name == name)
            {
                result = gmobj;
                break;
            }
        }
        return result;
    }
}
