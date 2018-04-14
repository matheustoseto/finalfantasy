using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour {

    public Inventory inventory;
    public ItemDatabase itemDatabase;

    public GameObject inventoryPanel;
    public GameObject craftPanel;

    private GameObject slotPanel;
    public GameObject craftItem;
    private List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        slotPanel = craftPanel.transform.Find("SlotPanel").gameObject;

        foreach (CraftItem ct in itemDatabase.GetCraftList())
        {
            GameObject slot = Instantiate(craftItem, slotPanel.transform);
            slot.GetComponent<CraftItem>().Id = ct.Id;
            slot.GetComponent<CraftItem>().Combination = ct.Combination;
            Button btn = slot.transform.Find("Craft").gameObject.GetComponent<Button>();
            btn.onClick.AddListener(delegate { CraftItem(slot.GetComponent<CraftItem>()); });
            Image img = slot.transform.Find("Craft").gameObject.transform.Find("Image").gameObject.GetComponent<Image>();
            img.sprite = inventory.FindItem(ct.Id).Sprite;
            Text text = slot.transform.Find("Combination").gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
            foreach(Combination cb in ct.Combination)
                text.text += inventory.FindItem(cb.Id).Title + ": " + cb.Qt + " \n";
        }
    }

    public void CraftItem(CraftItem craftItem)
    {
        Item item = inventory.FindItem(craftItem.Id);
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
            inventory.AddItem(item.Id);
            foreach (Combination combination in craftItem.Combination)
            {
                inventory.RemoveItem(combination.Id, combination.Qt);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            inventoryPanel.SetActive(true);
            craftPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            inventoryPanel.SetActive(false);
            craftPanel.SetActive(false);
        }
    }
}
