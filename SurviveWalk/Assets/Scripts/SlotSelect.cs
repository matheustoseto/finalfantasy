using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSelect : MonoBehaviour {

    public Inventory inventory;
    public Weapon weapon;
    public int slotSelect = 15;

    private int backupSlotSelect = 15;
    private Color defaultColor = new Color(0.875F, 0.875F, 0.875F, 1.000F);

    private bool redFlag = true;
    private float a = 255;

    private void Start()
    {
        Select(15);
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (inventory.slots[slotSelect].transform.childCount > 0)
                inventory.UseItem(inventory.slots[slotSelect].transform.GetChild(0).gameObject.GetComponent<ItemData>().item);
        } else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Select(15);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Select(16);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Select(17);
        }else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Select(18);
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //Select(4);
        } else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //Select(5);
        }
        SelectEfect();
    }

    private void SelectEfect()
    {
        if (redFlag)
        {
            inventory.slots[slotSelect].GetComponent<Image>().color = new Color(0, a / 255, 0, 255);
            a -= 5;
            if (a <= 200)
                redFlag = false;
        }
        else if (!redFlag)
        {
            inventory.slots[slotSelect].GetComponent<Image>().color = new Color(0, a / 255, 0, 255);
            a += 5;
            if (a >= 255)
                redFlag = true;
        }
    }

    private void Select(int id)
    {
        slotSelect = id;
        inventory.slots[backupSlotSelect].GetComponent<Image>().color = defaultColor;
        inventory.slots[slotSelect].GetComponent<Image>().color = Color.green;
        backupSlotSelect = slotSelect;
        if (inventory.slots[slotSelect].transform.childCount > 0)
            SetWeapon(inventory.items[slotSelect]);
        else
            SetWeapon(null);
    }

    public void UpdateSelect()
    {
        Select(slotSelect);
    }

    public void SetWeapon(Item item)
    {
        if (item != null)
        {
            if ("Weapon".Equals(item.Type))
            {
                weapon.item = item;
            }
            else
            {
                weapon.item = null;
            }
        } else
        {
            weapon.item = null;
        }    
    }

    public Item GetSelectItemBySlot()
    {
        if (inventory.slots[slotSelect].transform.childCount > 0)
        {
            return inventory.items[slotSelect];
            //return inventory.slots[slotSelect].transform.GetChild(0).gameObject.GetComponent<ItemData>().item;
        }
        return null;
    }
}
