using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
            //Prototype.NetworkLobby.LobbyManager.s_Singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
        }

    }
}
