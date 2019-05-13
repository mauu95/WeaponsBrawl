using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class PlayerHealth : NetworkBehaviour {

    [SyncVar]
    public int hp = 100;

    [Command]
    public void CmdTakeDamage(int damage)
    {
        hp -= damage;
        CmdRefreshHpText();
        if (hp <= 0)
            CmdPlayerDie();
    }



    void CmdRefreshHpText()
    {
        this.GetComponentInChildren<TextMesh>().text = hp.ToString();
        RpcRefreshHp(hp);
    }

    [ClientRpc]
    void RpcRefreshHp(int health)
    {
        this.GetComponentInChildren<TextMesh>().text = health.ToString();
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
}
