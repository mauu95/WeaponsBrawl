using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;


public class NetworkLobbyHook : LobbyHook {

    // for users to apply settings from their lobby player GameObject to their in-game player GameObject
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager network, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        var cc = lobbyPlayer.GetComponent<LobbyPlayer>();
        var player = gamePlayer.GetComponent<PlayerInfo>();
        player.team = cc.playerColor.ToString();
        player.pname = cc.playerName;
    }


}
