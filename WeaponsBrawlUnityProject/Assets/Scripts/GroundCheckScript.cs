using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour {

    private Movement movementScript;

    private void Start()
    {
        movementScript = GetComponentInParent<Movement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        movementScript.isGrounded = true;
    }
}
