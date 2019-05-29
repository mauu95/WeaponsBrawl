﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            Destroy(collision.gameObject);
        }
        else
        {
            PlayerHealth ph=collision.gameObject.GetComponentInChildren<PlayerHealth>();
            ph.CmdTakeDamage(ph.maxHealth);

        }
    }
}
