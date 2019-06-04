using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

//List of players in the match
public class MatchManager : NetworkBehaviour
{

    [SyncVar]
    public Color turn;
    [SyncVar]
    public float waiting = 30;
    public float turnDuration=30;
    public static MatchManager _instance = null;
    public List<PlayerInfo> _players = new List<PlayerInfo>();

    public void Start()
    {
        _instance = this;
        turn = Color.red;
        waiting = turnDuration;
        DontDestroyOnLoad(this.gameObject);
    }

   
    private void Update()
    {
        if (isServer)
        {
            waiting = waiting - Time.deltaTime;
            if (waiting < 0)
            {
                waiting = turnDuration;
                ChangeTurn();
                RpcChangeTurn(turn);                
            }
        }
    }

    //TODO: very simple just for the prototype
    [Server]
    private void ChangeTurn()
    {
        if (turn == Color.red)
        {
            turn = Color.blue;
        }
        else
        {
            turn = Color.red;
        }

    }

    public void AddPlayer(PlayerInfo player)
    {
        if (_players.Contains(player))
            return;

        _players.Add(player);
    }

    public void RemovePlayer(PlayerInfo player)
    {
            _players.Remove(player);
    }

    public int PlayerAliveNumber()
    {
        int alivePlayer = 0;
        foreach(PlayerInfo p in _players)
        {
            if (p != null&& p.status==PlayerInfo.Status.alive)
            {
                alivePlayer++;
            }
        }
        return alivePlayer;
    }

    public int PlayerAliveNumber(Color team)
    {
        int alivePlayer = 0;
        foreach (PlayerInfo p in _players)
        {
            if (p != null && p.status == PlayerInfo.Status.alive && p.team==team)
            {
                alivePlayer++;
            }
        }
        return alivePlayer;
    }

    public int PlayerDeadNumber()
    {
        int alivePlayer = 0;
        foreach (PlayerInfo p in _players)
        {
            if (p != null && p.status == PlayerInfo.Status.dead)
            {
                alivePlayer++;
            }
        }
        return alivePlayer;
    }

    public int PlayerDeadNumber(Color team)
    {
        int alivePlayer = 0;
        foreach (PlayerInfo p in _players)
        {
            if (p != null && p.status == PlayerInfo.Status.dead && p.team == team)
            {
                alivePlayer++;
            }
        }
        return alivePlayer;
    }

    public void Reset()
    {
        _players = new List<PlayerInfo>();
    }

    public List<PlayerInfo> DeadPlayerList()
    {
        List<PlayerInfo> dead = new List<PlayerInfo>();
        foreach(PlayerInfo p in _players)
        {
            if (p.status == PlayerInfo.Status.dead){
                dead.Add(p);
            }
        }
        return dead;
    }

    public List<PlayerInfo> DeadPlayerList(Color team)
    {
        List<PlayerInfo> dead = new List<PlayerInfo>();
        foreach (PlayerInfo p in _players)
        {
            if (p.status == PlayerInfo.Status.dead && p.team==team)
            {
                dead.Add(p);
            }
        }
        return dead;
    }
    


    [ClientRpc]
    void RpcChangeTurn(Color color)
    {
        foreach(PlayerInfo p in _players)
        {
            if(color== p.team)
            {
                SetPlayerTurn(p,true);
            }
            else
            {
                SetPlayerTurn(p, false);
            }
        }
    }

    private static void SetPlayerTurn(PlayerInfo p, bool active)
    {
        p.physicalPlayer.GetComponent<PlayerManager>().ChangeActiveStatus(active);
    }

}

