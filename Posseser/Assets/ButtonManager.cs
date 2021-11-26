using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
