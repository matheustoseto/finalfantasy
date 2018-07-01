using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestNpc : MonoBehaviour {

    public Inventory inventory;
    public ItemDatabase itemDatabase;

    public GameObject inventoryPanel;
    public GameObject questPanel;
    public GameObject questPlayerList;
    public GameObject emptyPanel;

    private GameObject slotPanel;
    public GameObject quest;
    public GameObject questPlayer;
    public GameObject questTask;

    public void Load(List<Quest> quests)
    {
        slotPanel = questPanel.transform.Find("QuestList").gameObject;

        if (slotPanel.transform.childCount > 0)
        {
            foreach (Transform child in slotPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Quest ct in itemDatabase.GetQuestList())
        {
            foreach (Quest qst in quests)
            {
                if (ct.Id.Equals(qst.Id) && !ct.IsGet)
                {
                    GameObject slot = Instantiate(quest, slotPanel.transform);
                    slot.GetComponent<QuestItem>().quest = ct;

                    Button btn = slot.GetComponent<Button>();
                    btn.onClick.AddListener(delegate { GetQuest(slot.GetComponent<QuestItem>()); });

                    slot.transform.Find("TilePanel").gameObject.transform.Find("Title").gameObject.GetComponent<Text>().text = ct.Title;
                    slot.transform.Find("TilePanel").gameObject.transform.Find("Descr").gameObject.GetComponent<Text>().text = ct.Descr;
                }
            }     
        }

        Instantiate(emptyPanel, slotPanel.transform);
    }

    public void GetQuest(QuestItem questItem)
    {
        questItem.quest.IsGet = true;

        GameObject quest = Instantiate(questPlayer, questPlayerList.transform);
        quest.transform.GetChild(0).GetComponent<Text>().text = questItem.quest.Title;
        quest.GetComponent<IdQuest>().questId = questItem.quest.Id;

        foreach (Task task in questItem.quest.Task)
        {
            GameObject tk = Instantiate(questTask, quest.transform);
            tk.transform.GetChild(0).GetComponent<Text>().text = task.Title;
            tk.GetComponent<PlayerTask>().task = task;
            tk.GetComponent<IdTask>().taskId = task.Id;
        }

        try{    Destroy(questItem.gameObject);  }
        catch (System.Exception){   }          
    }
    
    public void CompletTaskQuest(CompletQuest completQuest)
    {
        foreach (Transform go in questPlayerList.transform)
        {
            if (go.GetComponent<IdQuest>().questId.Equals(completQuest.questId))
            {
                foreach (Transform gameObj in go.transform)
                {
                    if ("Task(Clone)".Equals(gameObj.name) && gameObj.GetComponent<IdTask>().taskId.Equals(completQuest.taskId) && !gameObj.GetComponent<PlayerTask>().task.Complet)
                    {
                        gameObj.GetComponent<Image>().color = Color.green;
                        gameObj.GetComponent<PlayerTask>().task.Complet = true;
                        Text text = gameObj.transform.GetChild(0).GetComponent<Text>();
                        text.color = Color.white;
                        text.text = "Fale com o NPC.";
                    }
                }
            }
        }
    }
}
