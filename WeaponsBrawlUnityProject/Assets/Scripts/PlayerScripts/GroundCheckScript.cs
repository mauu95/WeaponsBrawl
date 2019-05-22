using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour {
    private List<string> unjampableColliderTag;
    public string[] ignoreTag = { "Chest", "Destroyer" };
    private PlayerMovement movementScript;

    private void Start()
    {
        unjampableColliderTag = new List<string>(ignoreTag);
        movementScript = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (!unjampableColliderTag.Contains(collision.tag))
        {
            movementScript.isGrounded = true;
        }
    }
}
