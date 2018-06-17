using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcPanel : MonoBehaviour {

    public Sprite activeTab;
    public Sprite disableTad;

    public GameObject dialogPanel;
    public GameObject npcPanel;

    public GameObject craftPanel;
    public GameObject questPanel;

    public GameObject craftTab;
    public GameObject questTab;

    public void OpenCraftPanel()
    {
        craftPanel.SetActive(true);
        questPanel.SetActive(false);

        craftTab.GetComponent<Image>().sprite = activeTab;
        questTab.GetComponent<Image>().sprite = disableTad;
    }

    public void OpenQuestPanel()
    {
        questPanel.SetActive(true);
        craftPanel.SetActive(false);

        questTab.GetComponent<Image>().sprite = activeTab;
        craftTab.GetComponent<Image>().sprite = disableTad;
    }

    public void ClosePanel()
    {
        dialogPanel.SetActive(false);
        npcPanel.SetActive(false);
        OpenCraftPanel();
    }
}
