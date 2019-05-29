using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

    //List of players in the lobby
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

    public int PlayerAlive()
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

    public int PlayerAlive(Color team)
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

    public int PlayerDead()
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

    public int PlayerDead(Color team)
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

}

