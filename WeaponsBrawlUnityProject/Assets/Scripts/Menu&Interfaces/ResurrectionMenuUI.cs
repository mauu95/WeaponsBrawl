using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionMenuUI : MonoBehaviour {

    public GameObject ResurrectButtonPrefab;
    public Transform ItemsParent;

	public void OpenCloseResurrectionMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

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
