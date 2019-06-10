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
        Debug.Log("Client?"+isClient);
        Debug.Log("server?" + isServer);
        resource = player.GetComponent<PlayerResourceScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       //if (isServer)
        {
            if (collision.gameObject.CompareTag("Tree"))
            {
                resource.CmdAddResouces(50);
                NetworkServer.Destroy(collision.gameObject);
            }
       }            
    }

}
