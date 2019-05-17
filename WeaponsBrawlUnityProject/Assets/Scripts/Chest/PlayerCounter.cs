using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounter : MonoBehaviour {
    private int playerCounter;

	// Use this for initialization
	void Start () {
        playerCounter = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetPlayerCounter()
    {
        return playerCounter;
    }
    private List<GameObject> inside = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("in "+other+"::"+other.tag);
        if (other.tag == "Player"&& (!inside.Contains(other.gameObject)))
        {
            inside.Add(other.gameObject);
            playerCounter++;
        }
        //Debug.Log(playerCounter);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inside.Remove(other.gameObject);
            playerCounter--;
        }
        playerCounter = Mathf.Max(playerCounter, 0);
        //Debug.Log(playerCounter);
    }

}
