using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLightOnTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            transform.parent.gameObject.GetComponent<TreeScript>().TurnOnTree(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            transform.parent.gameObject.GetComponent<TreeScript>().TurnOnTree(false);
    }
}
