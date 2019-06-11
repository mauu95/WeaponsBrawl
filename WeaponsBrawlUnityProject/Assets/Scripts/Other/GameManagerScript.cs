using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class GameManagerScript : NetworkBehaviour {

    public MatchManager matchInfo;

    public bool GameHasStarted;

    void Start()
    {
        matchInfo = Prototype.NetworkLobby.LobbyManager.s_Singleton.transform.Find("MatchManager").GetComponent<MatchManager>();
        StartCoroutine(StartMatch());
    }

    IEnumerator StartMatch()
    {
        yield return new WaitForSeconds(1f);

        GameHasStarted = true; // Prima di matchInfo InitializeTeams()
        matchInfo.waiting = matchInfo.turnDuration;

        matchInfo.InitializeTeams();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            matchInfo.waiting = 0.01f;
    }
}
