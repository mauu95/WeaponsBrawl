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
        yield return new WaitForSeconds(2f);
        this.GetComponent<Movement>().enabled = true;
    }
}
