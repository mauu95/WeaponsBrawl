using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerResourceScript : NetworkBehaviour {

    private ResourceUI UI;
    [SyncVar]
    public int resources = 100;

    private void Start()
    {
        UI = GetGameObjectInRoot("Canvas").GetComponent<ResourceUI>();
        UpdateUI();
    }

    [Command]
    public void CmdAddResouces(int amount)
    {
        resources += amount;
        RpcUpdateUI();
    }

    public bool UseResource(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    [ClientRpc]
    public void RpcUpdateUI()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (hasAuthority)
        {
            UI.SetResourceUI(resources);
        }
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
