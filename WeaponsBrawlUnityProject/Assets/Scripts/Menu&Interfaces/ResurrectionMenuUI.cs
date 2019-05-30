using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResurrectionMenuUI : AbstractInGameInterfaces {

    public GameObject ResurrectButtonPrefab;
    public Transform ItemsParent;

	public void OpenCloseResurrectionMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (!gameObject.activeSelf)//if we are closing it
        {
            Button[] buttons= ItemsParent.GetComponentsInChildren<Button>(true);
            foreach (Button button in buttons)
            {
                Destroy(button.gameObject);
            }
        }
    }

    public void OpenResurrectionMenu()
    {
        gameObject.SetActive(true);
    }

    public void AddResurrectButton(string text, UnityAction listner)
    {
        GameObject button = Instantiate(ResurrectButtonPrefab);
        button.transform.SetParent(ItemsParent);
        Text t = button.GetComponentInChildren<Text>();
        t.text = text;
        Button buttonComponenet = button.GetComponent<Button>();
        buttonComponenet.onClick.AddListener(listner);
        button.transform.localScale = Vector3.one;
    }




}
