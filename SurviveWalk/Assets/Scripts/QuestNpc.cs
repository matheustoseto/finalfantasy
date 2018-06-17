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
    public GameObject emptyPanel;

    private GameObject slotPanel;
    public GameObject quest;

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

    }  
}
