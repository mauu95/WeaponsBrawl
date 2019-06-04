using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//List of players in the match
public class MatchManager : MonoBehaviour
{
    public static MatchManager _instance = null;
    public List<PlayerInfo> _players = new List<PlayerInfo>();

    public void Start()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
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
        
    
}

