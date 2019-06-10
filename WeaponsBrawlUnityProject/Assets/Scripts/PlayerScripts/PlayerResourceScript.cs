using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerResourceScript : MonoBehaviour {

    private ResourceUI UI;
    public int resources = 100;

    private void Start()
    {
        UI = GetGameObjectInRoot("Canvas").GetComponent<ResourceUI>();
        UpdateUI();
    }

    public void addResouces(int amount)
    {
        resources += amount;
        UpdateUI();
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

    private void UpdateUI()
    {
        UI.SetResourceUI(resources);
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
