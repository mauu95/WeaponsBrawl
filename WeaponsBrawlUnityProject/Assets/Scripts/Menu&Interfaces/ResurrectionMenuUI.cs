using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionMenuUI : AbstractInGameInterfaces {

    public GameObject ResurrectButtonPrefab;
    public Transform ItemsParent;

    private void AddResurrectButton()
    {
        GameObject button = Instantiate(ResurrectButtonPrefab);
        button.transform.SetParent(ItemsParent);
        button.transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            AddResurrectButton();
    }


}
