using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    public GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;
    public GameObject tooltip;
    public GameObject slotLifePanel;
    private CharacterStatus characterStatus;

    public int slotAmount;
	public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private Item addItem;

    void Start()
	{
        database = GetComponent<ItemDatabase>();
		slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;
        characterStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();

        for (int i = 0; i < slotAmount; i++)
		{
			items.Add(new Item());
			slots.Add(Instantiate(inventorySlot));
			slots[i].GetComponent<Slot>().id = i;
            if (i > 3)
            {
                slots[i].transform.SetParent(slotPanel.transform);
            } else
            {
                slots[i].transform.SetParent(slotLifePanel.transform);                
            }			
		}

        //slots[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(10, 30);
        //slots[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 30);
        //slots[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 30);
        //slots[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 30);
        //slots[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(520, 30);
        //slots[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(590, 30);

        slots[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 30);
        slots[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 30);
        slots[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 30);
        slots[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(520, 30);

        //Add item
        //AddItem(2);
        //AddItem(2);
        //AddItem(2);
        //AddItem(2);
        //AddItem(3);
        //AddItem(3);
        //AddItem(3);
        //AddItem(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
                tooltip.SetActive(false);
            } else
            {
                inventoryPanel.SetActive(true);
            }
        }
    }

    public void AddItem(int id)
	{        
		Item itemToAdd = database.FetchItemById(id);
        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i].Id == id)
				{
					ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
					data.amount++;
					data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
					break;
				}
			}
		}
		else
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i].Id == -1)
				{
					items[i] = itemToAdd;
					GameObject itemObj = Instantiate(inventoryItem);
					itemObj.GetComponent<ItemData>().item = itemToAdd;
					itemObj.GetComponent<ItemData>().slotId = i;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = Vector3.zero;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
					itemObj.name = "Item: " + itemToAdd.Title;
					//slots[i].name = "Slot: " + itemToAdd.Title;
                    break;
				}
			}
		}
	}

    public void RemoveItem(int id, int qnt)
    {
        ItemData data = FindItemData(id);
        data.amount-= qnt;
        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

        if (data.amount <= 0)
        {
            Destroy(data.gameObject);
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == id)
                {
                    items[i] = new Item();
                }
            }
        }
    }

    public Item FindItem(int id)
    {
        Item item = database.FetchItemById(id);
        return item;
    }

    public ItemData FindItemData(int id)
    {
        ItemData item = (from it in slots
                        where it.transform.childCount > 0 &&
                        it.transform.GetChild(0) != null && 
                        it.transform.GetChild(0).GetComponent<ItemData>() != null && 
                        it.transform.GetChild(0).GetComponent<ItemData>().item.Id == id
                         select it.transform.GetChild(0).GetComponent<ItemData>()).FirstOrDefault();

        return item;
    }

    bool CheckIfItemIsInInventory(Item item)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].Id == item.Id)
			{
				return true;
			}
		}

		return false;
	}

    public bool UseItem(Item item)
    {
        if ("Food".Equals(item.Type) && characterStatus.lifeProgress != characterStatus.life)
        {
            if (!Progress.Instance.activeBar)
            {
                addItem = item;
                Progress.Instance.ProgressBar(0.09F, AddItem);
                return true;
            }                     
        }
        return false;
    }

    public void AddItem()
    {
        characterStatus.addLife(addItem.Power);
        this.RemoveItem(addItem.Id, 1);
        addItem = null;
    }
}
