using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResurrectionChest : AbstractChest {

    public override void ClientPreInteract(PlayerChestManager p)
    {
        if (!IsInteractable(p))
        {

        }
        Color team = p.gameObject.GetComponent<PlayerManager>().GetTeam();
        List<PlayerInfo> deadAlly = MatchManager._instance.DeadPlayerList(team);
        ResurrectionMenuUI resurrectionMenu = GetGameObjectInRoot("Canvas").GetComponentInChildren<ResurrectionMenuUI>(true);
        p.waitingUser = true;
        resurrectionMenu.OpenClose();
        foreach (PlayerInfo ally in deadAlly)
        {
            resurrectionMenu.AddResurrectButton(ally.pname, p);
        }
        //resurrectionMenu.AddResurrectButton("test", p);
    }

    internal override bool DoSomething(PlayerChestManager p)
    {
        Color team = p.gameObject.GetComponent<PlayerManager>().GetTeam();
        List<PlayerInfo> deadAlly = MatchManager._instance.DeadPlayerList(team);
        foreach(PlayerInfo ally in deadAlly)
        {
            if (ally.pname == p.allyToResurrect)
            {
                ally.status = PlayerInfo.Status.alive;
                ally.transform.position = gameObject.transform.position;
                ally.CmdResurrect();
                p.allyToResurrect = null;
                return true;
            }
        }
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool IsInteractable(PlayerChestManager p)
    {
        Color team = p.gameObject.GetComponent<PlayerManager>().GetTeam();
        return base.IsInteractable(p) && (MatchManager._instance.PlayerDeadNumber(team)>0);

    }

    private GameObject GetGameObjectInRoot(string objname)
    {
        GameObject[] root = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in root)
            if (obj.name == objname)
                return obj;
        return null;
    }

}
