using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartGame : MonoBehaviour {

    public GameObject startgame;
    public GameObject creditos;

    public void StartGame()
    {
        SceneManager.LoadScene("Vertical", LoadSceneMode.Single);
    }

    public void Creditos()
    {
        startgame.SetActive(false);
        creditos.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Voltar()
    {
        startgame.SetActive(true);
        creditos.SetActive(false);
    }
}
