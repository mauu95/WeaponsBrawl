using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : AbstractMenu {

    public Button PlayButton;

    private void Start()
    {
        PlayButton.onClick.RemoveAllListeners();
        print(LobbyMyFeature.Instance);
        PlayButton.onClick.AddListener(Ciao);
    }

    private void Ciao()
    {
        LobbyMyFeature.Instance.SetActive(true);
    }
}
