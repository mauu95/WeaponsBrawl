using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : AbstractMenu
{
    public GameObject PauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenClosePauseMenu();
    }

    public void OpenClosePauseMenu()
    {
        PauseMenuUI.SetActive(!PauseMenuUI.activeSelf);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}



