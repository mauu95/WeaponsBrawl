using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class PlayerHealth : NetworkBehaviour {

    [SyncVar]
    public int hp = 100;

    public GameObject healthBar;

    [Command]
    public void CmdTakeDamage(int damage)
    {
        hp -= damage;
        CmdRefreshHpText();
        if (hp <= 0)
            CmdPlayerDie();
    }


    [Command]
    public void CmdGetLife(int life)
    {
        hp += life;
        CmdRefreshHpText();
    }



    void CmdRefreshHpText()
    {
        RefreshHealth(hp);
        RpcRefreshHp(hp);
    }

    [ClientRpc]
    void RpcRefreshHp(int health)
    {
        RefreshHealth(health);
    }

    void RefreshHealth(int health)
    {
        this.GetComponentInChildren<TextMesh>().text = health.ToString();
        healthBar.GetComponent<HealthBarScript>().SetSize(health * 1f / 100);

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
