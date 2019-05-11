using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
    public int hp = 100;
	
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            CmdRpcPlayerDie();
        }
    }

    [Command]
    [ClientRpc]
    void CmdRpcPlayerDie()
    {
        this.gameObject.SetActive(false);
    }
}
