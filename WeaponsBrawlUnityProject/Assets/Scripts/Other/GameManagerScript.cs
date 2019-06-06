﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour {

    public MatchManager matchInfo;

    void Start()
    {
        matchInfo = Prototype.NetworkLobby.LobbyManager.s_Singleton.transform.Find("MatchManager").GetComponent<MatchManager>();
        matchInfo.waiting = matchInfo.turnDuration;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            matchInfo.waiting = 0.01f;
    }
}
