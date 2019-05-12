using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealth : NetworkBehaviour {

    [SyncVar]
    public int hp = 100;

    public void ActivateMovementAfterSec()
    {
        StartCoroutine(ActivateMovement());
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            CmdPlayerDie();
        }
    }

    [Command]
    void CmdPlayerDie()
    {
        PlayerDie();
        RpcPlayerDie();
    }

    [ClientRpc]
    void RpcPlayerDie()
    {
       PlayerDie();
    }

    void PlayerDie()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator ActivateMovement()
    {
        yield return new WaitForSeconds(2f);
        this.GetComponent<Movement>().enabled = true;
    }

}
