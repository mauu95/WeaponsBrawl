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
    public List<PlayerInfo> _playersToWait = new List<PlayerInfo>();

    public List<PlayerInfo> RedTeam = new List<PlayerInfo>();
    public List<PlayerInfo> BlueTeam = new List<PlayerInfo>();

    public void Start()
    {
        _instance = this;
        turn = Color.red;
        waiting = turnDuration;
        DontDestroyOnLoad(this.gameObject);


    }

    public void InitializeTeams()
    {
        foreach (PlayerInfo player in _players)
        {
            if (player.team == Color.red)
                RedTeam.Add(player);
            else
                BlueTeam.Add(player);
        }
    }

   
    private void Update()
    {
        if (isServer)
        {

            if(RedTeam.Count != 0) // GameManagerScript.GameHasStarted.. ma va bene anche cosi
                if (AllPlayerHasEnded(this.turn))
                waiting = 0;

            waiting = waiting - Time.deltaTime;

            if (waiting < 0)
            {
                ChangeTurn();
                               
            }
        }

    }

    private bool AllPlayerHasEnded(Color turn)
    {
        bool result = true;
        List<PlayerInfo> CurrentTeam;

        if (turn == Color.red)
            CurrentTeam = RedTeam;
        else
            CurrentTeam = BlueTeam;

        foreach(PlayerInfo player in CurrentTeam)
        {
            if(player.physicalPlayer.GetComponent<PlayerManager>().isInTurn)
                result = false;
        }

        return result;
    }

    //TODO: very simple just for the prototype
    [Server]
    private void ChangeTurn()
    {
        _playersToWait = new List<PlayerInfo>();
        if (turn == Color.red)
        {
            turn = Color.blue;
        }
        else
        {
            turn = Color.red;
        }
        _playersToWait = this.AlivePlayerList(turn);
        RpcChangeTurn(turn);
        waiting = turnDuration;
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
        return FilterByStatus(PlayerInfo.Status.dead);
    }

    public List<PlayerInfo> AlivePlayerList()
    {
        return FilterByStatus(PlayerInfo.Status.alive);
    }

    private List<PlayerInfo> FilterByStatus(PlayerInfo.Status status)
    {
        List<PlayerInfo> players = new List<PlayerInfo>();
        foreach (PlayerInfo p in _players)
        {
            if (p &&(p.status == status))
            {
                players.Add(p);
            }
        }
        return players;
    }

    public List<PlayerInfo> DeadPlayerList(Color team)
    {
        return FilterByStatusAndTeam(PlayerInfo.Status.dead, team);
    }

    public List<PlayerInfo> AlivePlayerList(Color team)
    {
        return FilterByStatusAndTeam(PlayerInfo.Status.alive, team);
    }

    private List<PlayerInfo> FilterByStatusAndTeam(PlayerInfo.Status status, Color team)
    {
        List<PlayerInfo> players = new List<PlayerInfo>();
        foreach (PlayerInfo p in _players)
        {

            if (p && (p.status == status && p.team == team))
            {
                players.Add(p);
            }
        }
        return players;
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

    [Server]
    public void NotifyTurnEnd(PlayerInfo p)
    {
        
        _playersToWait.Remove(p);
        if (_playersToWait.Count <= 0)
        {
            this.ChangeTurn();
        }
    }

}

