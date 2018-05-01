using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseCraft : MonoBehaviour {

    public Utils.HouseType houseType;   
    public Inventory inventory;
    public ItemDatabase itemDatabase;

    public GameObject inventoryPanel;
    public GameObject housePanel;
    public GameObject houseItem;
    public List<GameObject> levels;

    private GameObject slotPanel;
    private int houseLevel = 0;

    void Start () {
        slotPanel = housePanel.transform.Find("SlotPanel").gameObject;   
    }
	
	void Update () {
		
	}

    private void LoadHousePanel()
    {
        foreach (CraftHouse ct in itemDatabase.GetHouseList())
        {
            if (houseType.GetHashCode() == ct.Id)
            {
                Text title = slotPanel.transform.Find("Title").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
                title.text = ct.Title;

                foreach (HouseLevel houseLevel in ct.houseLevel)
                {
                    GameObject slot = Instantiate(houseItem, slotPanel.transform);
                    slot.GetComponent<HouseItem>().Id = ct.Id;
                    slot.GetComponent<HouseItem>().IdLevel = houseLevel.IdLevel;
                    slot.GetComponent<HouseItem>().Combination = houseLevel.Combination;

                    Button btn = slot.transform.Find("Craft").gameObject.GetComponent<Button>();
                    btn.onClick.AddListener(delegate { UpdateLevel(slot.GetComponent<HouseItem>()); });

                    Text tx = slot.transform.Find("Craft").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
                    tx.text = houseLevel.Title;

                    Text text = slot.transform.Find("Combination").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
                    foreach (Combination cb in houseLevel.Combination)
                        text.text += inventory.FindItem(cb.Id).Title + ": " + cb.Qt + " \n";
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
                        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                        levels[i].SetActive(true);                   
                        if (i > 0)
                            levels[i - 1].SetActive(false);

                        houseLevel++;
                        RemoveItems(houseItem.Combination);
                        if ("HouseItem(Clone)".Equals(slotPanel.transform.GetChild(i+1).name))
                        {
                            Text tx = slotPanel.transform.GetChild(i+1).gameObject.transform.Find("Craft").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
                            tx.color = Color.green;

                            if ("HouseItem(Clone)".Equals(slotPanel.transform.GetChild(i).name))
                            {
                                Text tex = slotPanel.transform.GetChild(i).gameObject.transform.Find("Craft").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
                                tex.color = new Color(0.196f, 0.196f, 0.196f, 1.000f);
                            }
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

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            LoadHousePanel();
            if (!inventoryPanel.activeSelf)
                inventory.ActiveDisableInventory();
            housePanel.SetActive(true);

            for (int i = 1; i <= levels.Count; i++)
            {
                if(houseLevel == i)
                {
                    if ("HouseItem(Clone)".Equals(slotPanel.transform.GetChild(i).name))
                    {
                        Text tx = slotPanel.transform.GetChild(i).gameObject.transform.Find("Craft").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
                        tx.color = Color.green;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            if(inventoryPanel.activeSelf)
                inventory.ActiveDisableInventory();
            housePanel.SetActive(false);

            for (int i = 0; i < slotPanel.transform.childCount; i++)
            {
                if ("HouseItem(Clone)".Equals(slotPanel.transform.GetChild(i).name))
                {
                    Destroy(slotPanel.transform.GetChild(i).gameObject);
                }       
            }
        }
    }
}
