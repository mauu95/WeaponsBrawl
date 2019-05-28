using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInterfaceUI : MonoBehaviour {

    public GameObject buildInterfaceUI;

	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
            OpenCloseBuildInterface();
    }

    public void OpenCloseBuildInterface()
    {
        buildInterfaceUI.SetActive(!buildInterfaceUI.activeSelf);
    }

    public void RicordatiDiRinominareQuestaFunzionePaolo()
    {

    }
}
