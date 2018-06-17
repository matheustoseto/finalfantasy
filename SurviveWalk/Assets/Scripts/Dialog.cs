using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour {

    public GameObject npcPanel;
    public GameObject inventoryPanel;
    public Inventory inventory;

    private Npc npc = new Npc();
    private int step = 0;

    public void SetNpc(Npc npc)
    {
        this.npc = npc;
        Load();
    }

    private void Load()
    {
        step = 0;
        this.gameObject.transform.Find("Title").Find("Text").GetComponent<Text>().text = npc.Title;
        this.gameObject.transform.Find("Text").GetComponent<Text>().text = npc.Intro[step].Step;
    }

    public void NextStep()
    {
        step++;
        if (step < npc.Intro.Count)
        {
            this.gameObject.transform.Find("Text").GetComponent<Text>().text = npc.Intro[step].Step;
        } else
        {
            CloseDialog();
            if (npc.IsCraft || npc.IsQuest)
            {
                if (npc.IsQuest)
                {
                    npcPanel.transform.Find("Tabs").Find("QuestTab").gameObject.SetActive(true);
                    npcPanel.GetComponent<NpcPanel>().OpenQuestPanel();
                }
                else
                {
                    npcPanel.transform.Find("Tabs").Find("QuestTab").gameObject.SetActive(false);
                }

                if (npc.IsCraft)
                {
                    npcPanel.transform.Find("Tabs").Find("CraftTab").gameObject.SetActive(true);
                    npcPanel.GetComponent<NpcPanel>().OpenCraftPanel();
                } else
                {
                    npcPanel.transform.Find("Tabs").Find("CraftTab").gameObject.SetActive(false);
                }

                if (!inventoryPanel.activeSelf)
                    inventory.ActiveDisableInventory();

                npcPanel.SetActive(true);
            }
        }      
    }

    public void CloseDialog()
    {
        this.gameObject.SetActive(false);
    }

    public void CloseNpcPanel()
    {
        npcPanel.GetComponent<NpcPanel>().ClosePanel();
        inventory.DisableInventory();
    }
}
