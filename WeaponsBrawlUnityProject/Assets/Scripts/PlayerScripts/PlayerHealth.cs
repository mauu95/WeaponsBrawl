using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class PlayerHealth : NetworkBehaviour {

    [SyncVar]
    public int hp = 100;
    public int maxHealth = 100;
    public GameObject healthBar;

    [Command]
    public void CmdTakeDamage(int damage)
    {
        hp -= damage;
        CmdRefreshHealth();
        if (hp <= 0)
            CmdPlayerDie();
    }


    [Command]
    public void CmdGetLife(int life)
    {
        hp += life;
        hp = Math.Min(hp, maxHealth);
        CmdRefreshHealth();
    }



    void CmdRefreshHealth()
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
        healthBar.GetComponent<HealthBarScript>().SetHealth(health);
    }

    [Command]
    void CmdPlayerDie()
    {
        PlayerDie();
        gameObject.GetComponent<PlayerManager>().PlayerDie();
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
        this.gameObject.transform.position = new Vector3();
    }
}
