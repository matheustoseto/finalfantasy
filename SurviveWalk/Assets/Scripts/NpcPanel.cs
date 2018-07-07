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

    public QuestNpc questNpc;
    public CraftNpc craftNpc;

    public GameObject stick;
    public GameObject stickSeta;
    public GameObject apple;
    public GameObject appleSeta;
    public GameObject enemy;

    public void LoadCraft(List<CraftItem> crafts)
    {
        craftNpc.Load(crafts);
    }

    public void OpenCraftPanel()
    {
        craftPanel.SetActive(true);
        questPanel.SetActive(false);

        craftTab.GetComponent<Image>().sprite = activeTab;
        questTab.GetComponent<Image>().sprite = disableTad;
    }

    public void LoadQuest(List<Quest> quests)
    {
        questNpc.Load(quests);
    }

    public void OpenQuestPanel()
    {
        questPanel.SetActive(true);
        craftPanel.SetActive(false);

        questTab.GetComponent<Image>().sprite = activeTab;
        craftTab.GetComponent<Image>().sprite = disableTad;
    }

    public void GetQuest(List<Quest> quests)
    {
        foreach (Quest quest in quests)
        {
            if (!quest.IsGet)
            {
                QuestItem questItem = new QuestItem();
                questItem.quest = quest;

                questNpc.GetQuest(questItem);

                if (1 == quest.Id)
                {
                    stick.GetComponent<Resource>().isActive = true;
                    stickSeta.SetActive(true);
                }

                if (3 == quest.Id)
                {
                    apple.GetComponent<Resource>().isActive = true;
                    appleSeta.SetActive(true);
                }

                if (2 == quest.Id)
                {
                    enemy.SetActive(true);
                }
            }    
        }
    }

    public void ClosePanel()
    {
        dialogPanel.SetActive(false);
        npcPanel.SetActive(false);
        OpenCraftPanel();
    }
}
