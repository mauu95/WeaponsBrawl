using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour {

    private PlayerMovement movementScript;

    private void Start()
    {
        movementScript = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        movementScript.isGrounded = true;
    }
}
