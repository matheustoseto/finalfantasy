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
    public NpcPanel npcPanel;
    private CharacterStatus characterStatus;

    public int slotAmount;
	public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public GameObject stickSeta;
    public GameObject appleSeta;

    private Item addItem;

    private static Inventory instance = null;
    public static Inventory Instance { get { return instance; } }

    void Start()
	{
        instance = this;
        database = GetComponent<ItemDatabase>();
		slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;
        characterStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();

        for (int i = 0; i < slotAmount; i++)
		{
			items.Add(new Item());
			slots.Add(Instantiate(inventorySlot));
			slots[i].GetComponent<Slot>().id = i;
            if (i >= slotAmount - 4)
            {
                slots[i].transform.SetParent(slotLifePanel.transform);
            } else
            {
                slots[i].transform.SetParent(slotPanel.transform);            
            }			
		}

        //slots[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(10, 30);
        //slots[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 30);
        //slots[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 30);
        //slots[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 30);
        //slots[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(520, 30);
        //slots[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(590, 30);

        slots[15].GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 30);
        slots[16].GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 30);
        slots[17].GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 30);
        slots[18].GetComponent<RectTransform>().anchoredPosition = new Vector2(520, 30);

        //Add item
        //AddItemInSlot(40,15);
        //AddItemQnt(2, 50);
        //AddItemQnt(9, 2);
        //AddItemQnt(40, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActiveDisableInventory();
        }
    }

    public void AddItemInSlot(int id, int slot)
    {
        Item itemToAdd = new Item(database.FetchItemById(id));
        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id.Equals(itemToAdd.Id))
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
            if (items[slot].Id.Equals(-1))
            {
                itemToAdd.Slot = slot;
                items[slot] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.GetComponent<ItemData>().slotId = slot;
                itemObj.GetComponent<ItemData>().amount = 1;
                itemObj.GetComponent<ItemData>().durability = itemToAdd.Durability;

                if(itemToAdd.Durability <= 0)
                    itemObj.GetComponent<ItemData>().transform.GetChild(0).GetComponent<Text>().text = "";
                if (itemToAdd.Durability > 0)
                    itemObj.GetComponent<ItemData>().transform.GetChild(1).GetComponent<Text>().text = itemToAdd.Durability.ToString();

                itemObj.transform.SetParent(slots[slot].transform);
                itemObj.transform.position = Vector3.zero;
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                itemObj.name = "Item: " + itemToAdd.Title;
                //slots[i].name = "Slot: " + itemToAdd.Title;
            }
        }

        PlayerManager.Instance.player.GetComponent<SlotSelect>().UpdateSelect();
        CheckQuest(id);
    }

    public void AddItemQnt(int id, int qnt)
    {
        for (int i = 1; i <= qnt; i++)
        {
            AddItem(id);
        }
    }

    public void AddItem(int id)
	{
        Item itemToAdd = new Item(database.FetchItemById(id));

        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i].Id.Equals(itemToAdd.Id))
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
				if (items[i].Id.Equals(-1))
				{
                    itemToAdd.Slot = i;
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
					itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slotId = i;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().durability = itemToAdd.Durability;

                    if (itemToAdd.Durability <= 0)
                        itemObj.GetComponent<ItemData>().transform.GetChild(0).GetComponent<Text>().text = "";
                    if (itemToAdd.Durability > 0)
                        itemObj.GetComponent<ItemData>().transform.GetChild(1).GetComponent<Text>().text = itemToAdd.Durability.ToString();

                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = Vector3.zero;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
					itemObj.name = "Item: " + itemToAdd.Title;
					//slots[i].name = "Slot: " + itemToAdd.Title;
                    break;
				}
			}
		}

        PlayerManager.Instance.player.GetComponent<SlotSelect>().UpdateSelect();
        CheckQuest(id);
        Sound(id);
    }

    private void CheckQuest(int id)
    {
        if (id == 0)
        {
            if (NpcController.Instance != null && NpcController.Instance.npcType.Equals(Utils.NpcType.Npc4))
            {
                CompletQuest completQuest = new CompletQuest();
                completQuest.questId = 3;
                completQuest.taskId = 0;

                NpcController.Instance.questNpc.CompletTaskQuest(completQuest);
            }
        }

        if (id == 9)
        {
            if (NpcController.Instance != null && NpcController.Instance.npcType.Equals(Utils.NpcType.Npc3))
            {
                CompletQuest completQuest = new CompletQuest();
                completQuest.questId = 1;
                completQuest.taskId = 0;

                NpcController.Instance.questNpc.CompletTaskQuest(completQuest);
                npcPanel.ClosePanel();
            }
        }

        if (id == 5)
        {
            stickSeta.SetActive(false);
        }

        if (id == 0)
        {
            appleSeta.SetActive(false);
        }

        if (id == 40)
        {
            if (NpcController.Instance != null && NpcController.Instance.npcType.Equals(Utils.NpcType.Npc7))
            {
                CompletQuest completQuest = new CompletQuest();
                completQuest.questId = 4;
                completQuest.taskId = 0;

                NpcController.Instance.questNpc.CompletTaskQuest(completQuest);
                npcPanel.ClosePanel();

                NpcController.Instance.npcType = Utils.NpcType.Npc8;
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
                if (items[i].Id.Equals(id))
                {
                    items[i] = new Item();
                }
            }
        }
    }

    public Item FindItem(int id)
    {
        Item item = new Item(database.FetchItemById(id));
        return item;
    }

    public ItemData FindItemData(int id)
    {
        ItemData item = (from it in slots
                        where it.transform.childCount > 0 &&
                        it.transform.GetChild(0) != null && 
                        it.transform.GetChild(0).GetComponent<ItemData>() != null && 
                        it.transform.GetChild(0).GetComponent<ItemData>().item.Id.Equals(id)
                         select it.transform.GetChild(0).GetComponent<ItemData>()).FirstOrDefault();

        return item;
    }

    public ItemData FindDurabilityItemData(Item itemRemove)
    {
        ItemData item = (from it in slots
                         where it.transform.childCount > 0 &&
                         it.transform.GetChild(0) != null &&
                         it.transform.GetChild(0).GetComponent<ItemData>() != null &&
                         it.transform.GetChild(0).GetComponent<ItemData>().item.Equals(itemRemove)
                         select it.transform.GetChild(0).GetComponent<ItemData>()).FirstOrDefault();

        return item;
    }

    bool CheckIfItemIsInInventory(Item item)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].Id.Equals(item.Id))
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
                addItem = new Item(item);
                Progress.Instance.ProgressBar(0.09F, AddLife);
                return true;
            }                     
        }
        return false;
    }

    public void AddLife()
    {
        characterStatus.addLife(addItem.Power);
        this.RemoveItem(addItem.Id, 1);
        addItem = null;
    }

    public void AddLife(float life)
    {
        characterStatus.addLife(life);
    }

    public bool RemoveDurability(Item item)
    {    
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Id.Equals(item.Id) && items[i].Slot.Equals(item.Slot) && items[i].DurabilityCount > 0)
            {
                items[i].DurabilityCount -= 1;

                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                data.transform.GetChild(1).GetComponent<Text>().text = items[i].DurabilityCount.ToString();

                if (items[i].DurabilityCount <= 0)
                {
                    RemoveItemDurability(data);
                    PlayerManager.Instance.player.GetComponent<SlotSelect>().UpdateSelect();
                    return true;
                }
            }
        }
        return false;
    }

    public void RemoveItemDurability(ItemData itemData)
    {
        tooltip.SetActive(false);

        Destroy(itemData.gameObject);
        items[itemData.slotId] = new Item();
    }

    public void DisableInventory()
    {
        inventoryPanel.SetActive(false);
        tooltip.SetActive(false);
    }

    public void ActiveDisableInventory()
    {
        if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
            tooltip.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
        }
    }

    public Enemy GetEnemyData(int id)
    {
        Enemy enemy = (from it in database.GetEnemyList()
                         where it.Id.Equals(id)
                         select it).FirstOrDefault();

        return new Enemy(enemy);
    }

    public Npc GetNpcData(int id)
    {
        Npc npc = (from it in database.GetNpcList()
                       where it.Id.Equals(id)
                       select it).FirstOrDefault();

        return npc;
    }

    public Quest GetQuest(int id)
    {
        return database.GetQuestList(id);
    }

    public void Sound(int id)
    {
        SoundControl.GetInstance().ExecuteEffect(TypeSound.Coleta);
    }
}
