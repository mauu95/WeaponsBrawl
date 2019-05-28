using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : AbstractMenu {

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
