using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject creditsMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1 Tor");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditMenu()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
