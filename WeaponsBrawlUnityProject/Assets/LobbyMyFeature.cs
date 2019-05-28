using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMyFeature : MonoBehaviour {

    public static GameObject Instance;

    private void Awake()
    {
        Instance = this.gameObject;
        gameObject.SetActive(false);
    }
}
