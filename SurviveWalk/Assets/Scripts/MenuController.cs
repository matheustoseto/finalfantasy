using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject InventoryPanel;
    public GameObject Dialog;
    public GameObject NpcPanel;
    public GameObject HousePanel;
    public GameObject Alert;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAll();
            
        } else if (Input.GetKeyDown(KeyCode.P))
        {
            MenuPause();
        }
    }

    public void CloseAll()
    {
        InventoryPanel.SetActive(false);
        Dialog.SetActive(false);
        NpcPanel.SetActive(false);
        HousePanel.SetActive(false);
        Alert.SetActive(false);
        IcarusPlayerController.Instance.IsBlockInputs = false;
    }

    public void MenuPause()
    {
        if (PauseMenu.activeSelf)
        {
            IcarusPlayerController.Instance.IsBlockInputs = false;
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            SoundControl.GetInstance().ExecuteEffect(TypeSound.Pause);
            IcarusPlayerController.Instance.IsBlockInputs = true;
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        CloseAll();
        MenuPause();
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
