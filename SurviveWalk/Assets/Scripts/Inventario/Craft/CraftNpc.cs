using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftNpc : MonoBehaviour {

    public Inventory inventory;
    public ItemDatabase itemDatabase;

    public NpcPanel npcPanel;
    public GameObject inventoryPanel;
    public GameObject craftPanel;
    public GameObject emptyPanel;

    private GameObject slotPanel;
    public GameObject craft;
    public GameObject item;

    public void Load(List<CraftItem> crafts)
    {
        slotPanel = craftPanel.transform.Find("CraftList").gameObject;

        if (slotPanel.transform.childCount > 0)
        {
            foreach (Transform child in slotPanel.transform)
            {
               Destroy(child.gameObject);
            }
        }

        foreach (CraftItem ct in itemDatabase.GetCraftList())
        {
            foreach (CraftItem ci in crafts)
            {
                if (ct.Id.Equals(ci.Id))
                {
                    GameObject slot = Instantiate(craft, slotPanel.transform);
                    slot.GetComponent<CraftItem>().Id = ct.Id;
                    slot.GetComponent<CraftItem>().Combination = ct.Combination;
                    Button btn = slot.GetComponent<Button>();
                    btn.onClick.AddListener(delegate { CraftItem(slot.GetComponent<CraftItem>()); });

                    Image img = slot.transform.Find("Icon").gameObject.GetComponent<Image>();
                    img.sprite = inventory.FindItem(ct.Id).Sprite;

                    Item it = inventory.FindItem(ct.Id);

                    slot.transform.Find("TilePanel").gameObject.transform.Find("Title").gameObject.GetComponent<Text>().text = it.Title;
                    slot.transform.Find("TilePanel").gameObject.transform.Find("Descr").gameObject.GetComponent<Text>().text = "Power: " + it.Power;

                    foreach (Combination cb in ct.Combination)
                    {
                        Item itCb = inventory.FindItem(cb.Id);
                        GameObject slotItem = Instantiate(item, slot.transform.Find("ItemPanel"));

                        slotItem.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = itCb.Sprite;
                        slotItem.transform.Find("Title").gameObject.GetComponent<Text>().text = cb.Qt + "x " + itCb.Title;
                    }
                }
            }     
        }

        Instantiate(emptyPanel, slotPanel.transform);
    }

    public void CraftItem(CraftItem craftItem)
    {
        bool isCraft = false;
        int craftSize = craftItem.Combination.Count;
        int i = 0;
        foreach (Combination combination in craftItem.Combination)
        {
            foreach (Item it in inventory.items)
            {
                if (it.Id == combination.Id)
                {
                    i++;
                    if (inventory.FindItemData(it.Id).amount >= combination.Qt)
                    {
                        isCraft = true;
                    }
                    else
                    {
                        isCraft = false;
                    }
                }
            }
        }
        if (craftSize == i && isCraft)
        {
            inventory.AddItem(inventory.FindItem(craftItem.Id).Id);
            foreach (Combination combination in craftItem.Combination)
            {
                inventory.RemoveItem(combination.Id, combination.Qt);
            }

            if (NpcController.Instance.npcType.Equals(Utils.NpcType.Npc3))
            {
                CompletQuest completQuest = new CompletQuest();
                completQuest.questId = 1;
                completQuest.taskId = 0;

                NpcController.Instance.questNpc.CompletTaskQuest(completQuest);
                npcPanel.ClosePanel();
            }
        }
    }

}
