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



    }

