using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseCraft : MonoBehaviour {

    public Utils.HouseType houseType;
    public CompletQuest completQuest;
    public Inventory inventory;
    public ItemDatabase itemDatabase;
    public QuestNpc questNpc;
    public GameObject inventoryPanel;
    public GameObject housePanel;
    public GameObject houseItem;
    public GameObject item;
    public GameObject level0;
    public List<GameObject> levels;

    private int houseLevel = 0;
    private bool isLoad = false;

    private void LoadHousePanel()
    {
        isLoad = true;
        foreach (CraftHouse ct in itemDatabase.GetHouseList())
        {
            if (houseType.GetHashCode() == ct.Id)
            {
                Text title = housePanel.transform.Find("Tabs").Find("HouseTab").Find("Text").gameObject.GetComponent<Text>();
                title.text = ct.Title;

                foreach (HouseLevel houseLevel in ct.houseLevel)
                {
                    GameObject slot = Instantiate(houseItem, housePanel.transform.Find("HousePanel").Find("HouseList").transform);
                    slot.GetComponent<HouseItem>().Id = ct.Id;
                    slot.GetComponent<HouseItem>().IdLevel = houseLevel.IdLevel;
                    slot.GetComponent<HouseItem>().Combination = houseLevel.Combination;

                    Button btn = slot.GetComponent<Button>();
                    btn.onClick.AddListener(delegate { UpdateLevel(slot.GetComponent<HouseItem>()); });

                    slot.transform.Find("TilePanel").gameObject.transform.Find("Title").gameObject.GetComponent<Text>().text = houseLevel.Title;

                    foreach (Combination cb in houseLevel.Combination)
                    {
                        Item itCb = inventory.FindItem(cb.Id);
                        GameObject slotItem = Instantiate(item, slot.transform.Find("ItemPanel"));

                        slotItem.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = itCb.Sprite;
                        slotItem.transform.Find("Title").gameObject.GetComponent<Text>().text = cb.Qt + "x " + itCb.Title;
                    }
                } 
            }
        }        
    }

    public void UpdateLevel(HouseItem houseItem)
    {
        if (houseLevel == houseItem.IdLevel)
        {
            if (VerificaInventario(houseItem.Combination))
            {
                for (int i = 0; i < levels.Count; i++)
                {
                    if (houseLevel == i)
                    {
                        level0.SetActive(false);
                        levels[i].SetActive(true);                   
                        if (i > 0)
                            levels[i - 1].SetActive(false);

                        houseLevel++;
                        RemoveItems(houseItem.Combination);

                        if ("House(Clone)".Equals(housePanel.transform.Find("HousePanel").Find("HouseList").transform.GetChild(i).name))
                        {
                            Image img = housePanel.transform.Find("HousePanel").Find("HouseList").transform.GetChild(i).gameObject.transform.Find("Icon").gameObject.GetComponent<Image>();
                            img.color = Color.green;
                            questNpc.CompletTaskQuest(completQuest);

                            SoundControl.GetInstance().ExecuteEffect(TypeSound.Create);
                        }
                        break;
                    }
                }
            }        
        }
    }

    public bool VerificaInventario(List<Combination> cb)
    {
        bool isCraft = false;
        int craftSize = cb.Count;
        int i = 0;
        foreach (Combination combination in cb)
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
            return true;
        }
        return false;
    }

    private void RemoveItems(List<Combination> cb)
    {
        foreach (Combination combination in cb)
        {
            inventory.RemoveItem(combination.Id, combination.Qt);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag) && Input.GetKey(KeyCode.E) && !isLoad)
        {
            IcarusPlayerController.Instance.IsBlockInputs = true;
            LoadHousePanel();
            if (!inventoryPanel.activeSelf)
                inventory.ActiveDisableInventory();
            housePanel.SetActive(true);

            for (int i = 1; i <= levels.Count; i++)
            {
                if(houseLevel == i)
                {
                    if ("House(Clone)".Equals(housePanel.transform.Find("HousePanel").Find("HouseList").transform.GetChild(i-1).name))
                    {
                        Image img = housePanel.transform.Find("HousePanel").Find("HouseList").transform.GetChild(i-1).gameObject.transform.Find("Icon").gameObject.GetComponent<Image>();
                        img.color = Color.green;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            IcarusPlayerController.Instance.IsBlockInputs = false;
            housePanel.SetActive(false);

            for (int i = 0; i < housePanel.transform.Find("HousePanel").Find("HouseList").transform.childCount; i++)
            {
                if ("House(Clone)".Equals(housePanel.transform.Find("HousePanel").Find("HouseList").transform.GetChild(i).name))
                {
                    Destroy(housePanel.transform.Find("HousePanel").Find("HouseList").transform.GetChild(i).gameObject);
                }       
            }
            isLoad = false;
        }
    }
}
