using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
	public int id;
	private Inventory inv;

	void Start()
	{
        inv = Inventory.Instance;
	}

	public void OnDrop(PointerEventData eventData)
	{
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
		if (inv.items[id].Id.Equals(-1))
		{
			inv.items[droppedItem.slotId] = new Item();
			inv.items[id] = droppedItem.item;
			droppedItem.slotId = id;
		}
		else if(!droppedItem.slotId.Equals(id))
		{
            droppedItem.item.Slot = id;

            Transform item = this.transform.GetChild(0);
			item.GetComponent<ItemData>().slotId = droppedItem.slotId;
            item.GetComponent<ItemData>().item.Slot = droppedItem.slotId;
            item.transform.SetParent(inv.slots[droppedItem.slotId].transform);
			item.transform.position = inv.slots[droppedItem.slotId].transform.position;

            inv.items[droppedItem.slotId] = new Item(item.GetComponent<ItemData>().item);
            inv.items[id] = new Item(droppedItem.item);

            droppedItem.slotId = id;
			droppedItem.transform.SetParent(this.transform);
			droppedItem.transform.position = this.transform.position;        
		}
	}
}