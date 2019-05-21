using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    public void ActivateMovementAfterSec()
    {
        StartCoroutine(ActivateMovement());
    }
    IEnumerator ActivateMovement()
    {
        while (gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            yield return 0;
        this.GetComponent<PlayerMovement>().enabled = true;
    }


}
