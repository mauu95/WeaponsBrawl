using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : AbstractMenu {

    public void LoadLobby()
    {
        SceneManager.LoadScene(1);
    }

    private void Awake()
    {
        if (Prototype.NetworkLobby.LobbyManager.s_Singleton != null)
            Destroy(Prototype.NetworkLobby.LobbyManager.s_Singleton.gameObject);
    }

}
