using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

    [SyncVar]
    public int hp = 100;
	
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

}
