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

    private void Start()
    {
        slotPanel = questPanel.transform.Find("QuestList").gameObject;

        foreach (Quest ct in itemDatabase.GetQuestList())
        {
            GameObject slot = Instantiate(quest, slotPanel.transform);
            slot.GetComponent<QuestItem>().quest = ct;

            Button btn = slot.GetComponent<Button>();
            btn.onClick.AddListener(delegate { GetQuest(slot.GetComponent<QuestItem>()); });

            slot.transform.Find("TilePanel").gameObject.transform.Find("Title").gameObject.GetComponent<Text>().text = ct.Title;
            slot.transform.Find("TilePanel").gameObject.transform.Find("Descr").gameObject.GetComponent<Text>().text = ct.Descr;
        }

        Instantiate(emptyPanel, slotPanel.transform);
    }

    public void GetQuest(QuestItem questItem)
    {
        GameObject quest = Instantiate(questPlayer, questPlayerList.transform);
        quest.transform.GetChild(0).GetComponent<Text>().text = questItem.quest.Title;

        foreach (Task task in questItem.quest.Task)
        {
            GameObject tk = Instantiate(questTask, quest.transform);
            tk.transform.GetChild(0).GetComponent<Text>().text = task.Title;
            tk.GetComponent<PlayerTask>().task = task;
        }

        Destroy(questItem.gameObject);
    }
    
    public void CompletTaskQuest(int id)
    {
        questPlayerList.transform.GetChild(0).GetChild(id + 1).GetComponent<Image>().color = Color.green;
        Text text = questPlayerList.transform.GetChild(0).GetChild(id + 1).GetChild(0).GetComponent<Text>();
        text.color = Color.white;
        text.text = "Fale com o NPC na cidade.";

        questPlayerList.transform.GetChild(0).GetChild(id + 1).GetComponent<PlayerTask>().task.Complet = true;        
    }
}
