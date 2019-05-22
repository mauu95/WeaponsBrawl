using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour {
    private List<string> notGroundedOnTag;
    public string[] ignoreTag = { "Chest", "Destroyer" };
    private PlayerMovement movementScript;

    private void Start()
    {
        notGroundedOnTag = new List<string>(ignoreTag);
        movementScript = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!notGroundedOnTag.Contains(collision.tag))
        {
            movementScript.isGrounded = true;
        }
    }
}
