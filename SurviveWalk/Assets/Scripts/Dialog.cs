using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour {

    public GameObject npcPanel;
    public GameObject inventoryPanel;
    public GameObject questPlayerList;
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
        if (!VerifyCompletQuest())
        {
            step++;
            if (step < npc.Intro.Count)
            {
                this.gameObject.transform.Find("Text").GetComponent<Text>().text = npc.Intro[step].Step;
            }
            else
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
                    }
                    else
                    {
                        npcPanel.transform.Find("Tabs").Find("CraftTab").gameObject.SetActive(false);
                    }

                    if (!inventoryPanel.activeSelf)
                        inventory.ActiveDisableInventory();

                    npcPanel.SetActive(true);
                }
            }
        }         
    }

    private bool VerifyCompletQuest()
    {
        if (questPlayerList.transform.childCount > 0)
        {
            for (int i = 0; i < questPlayerList.transform.GetChild(0).childCount; i++)
            {
                GameObject task = questPlayerList.transform.GetChild(0).GetChild(i).gameObject;
                if ("Task(Clone)".Equals(task.name))
                {
                    PlayerTask playerTask = task.GetComponent<PlayerTask>();

                    if (playerTask.task.Complet && !playerTask.speakNpc)
                    {
                        playerTask.speakNpc = true;
                        playerTask.ChangeText();
                        this.gameObject.transform.Find("Text").GetComponent<Text>().text = playerTask.task.Descr;
                        return true;
                    }
                }
            }
        }       
        return false;
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
