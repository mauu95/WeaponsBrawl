using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAxeScript : NetworkBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
            Destroy(collision.gameObject);
    }

}
