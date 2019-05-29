﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounter : MonoBehaviour {
    //private int playerCounter;
    private Dictionary<Color, int> playerCounter;
    private List<GameObject> inside = new List<GameObject>();
    // Use this for initialization
    void Start () {
        playerCounter = new Dictionary<Color, int>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetPlayerCounter(Color c)
    {
        if (!playerCounter.ContainsKey(c))
        {
            playerCounter[c] = 0;
        }
        //Debug.Log("key:" + c + " value:" + playerCounter[c]);
        return playerCounter[c];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"&& (!inside.Contains(other.gameObject)))
        {
            inside.Add(other.gameObject);
            Color team=other.gameObject.GetComponent<PlayerManager>().GetTeam();
            if (!playerCounter.ContainsKey(team))
            {
                playerCounter[team] = 0;
            }
            playerCounter[team]++;
            //Debug.Log("key:" + team + " value" + playerCounter[team]);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inside.Remove(other.gameObject);
            Color team = other.gameObject.GetComponent<PlayerManager>().GetTeam();
            if (!playerCounter.ContainsKey(team))
            {
                playerCounter[team] = 0;
            }
            playerCounter[team]--;
            playerCounter[team] = Mathf.Max(playerCounter[team], 0);
            //Debug.Log("key:" + team + " value" + playerCounter[team]);
        }
    }

}
