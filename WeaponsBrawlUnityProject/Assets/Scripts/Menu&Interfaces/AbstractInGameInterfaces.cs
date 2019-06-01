using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractInGameInterfaces : MonoBehaviour
{

    public GameObject UI;
    public bool isActive;

    private void Start()
    {
        isActive = UI.activeSelf;
    }

    public virtual void OpenClose()
    {
        if (isActive)
        {
            Close();
        }
        else
        {
            Open();
        }


    }

    public virtual void Close()
    {
        isActive = false;
        UI.SetActive(false);
    }

    public void Open()
    {
        isActive = true;
        UI.SetActive(true);
    }




}
