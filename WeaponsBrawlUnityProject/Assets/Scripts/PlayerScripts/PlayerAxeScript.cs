using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAxeScript : NetworkBehaviour
{
    private PlayerResourceScript resource;
    public GameObject player;

    private void Start()
    {
        resource = player.GetComponent<PlayerResourceScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            resource.addResouces(50);
            Destroy(collision.gameObject);
        }
            
    }

}
