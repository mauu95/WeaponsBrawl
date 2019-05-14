using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAiming : NetworkBehaviour {

    public float firePointRadius = 3.15f;
    public float speed = 10;


    void Update()
    {
        //if (hasAuthority)
            transform.Rotate(0f, 0f, Input.GetAxisRaw("Vertical") * speed);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, firePointRadius);
    }
}
