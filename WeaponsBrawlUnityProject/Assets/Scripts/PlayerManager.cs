using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {



    private void Awake()
    {
        GetComponentInChildren<MeshRenderer>().sortingOrder = 10;
    }

    public void ActivateMovementAfterSec()
    {
        StartCoroutine(ActivateMovement());
    }
    IEnumerator ActivateMovement()
    {
        while (gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            yield return 0;
        this.GetComponent<Movement>().enabled = true;
    }


}
