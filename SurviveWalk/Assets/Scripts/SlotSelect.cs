using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSelect : MonoBehaviour {

    public Inventory inventory;
    public GameObject weapon;
    public int slotSelect = 0;

    private int backupSlotSelect = 0;
    private Color defaultColor = new Color(0.875F, 0.875F, 0.875F, 1.000F);

    private void Start()
    {
        Select(0);
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (inventory.slots[slotSelect].transform.childCount > 0)
                inventory.UseItem(inventory.slots[slotSelect].transform.GetChild(0).gameObject.GetComponent<ItemData>().item);
        } else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Select(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Select(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Select(2);
        }else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Select(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Select(4);
        } else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Select(5);
        }      
    }

    private void Select(int id)
    {
        slotSelect = id;
        inventory.slots[backupSlotSelect].GetComponent<Image>().color = defaultColor;
        inventory.slots[slotSelect].GetComponent<Image>().color = Color.green;
        backupSlotSelect = slotSelect;
        if (inventory.slots[slotSelect].transform.childCount > 0)
            SetWeapon(inventory.slots[slotSelect].transform.GetChild(0).gameObject);
    }

    public void SetWeapon(GameObject slotItem)
    {
        if (slotItem != null)
        {
            Item item = inventory.FindItem(slotItem.GetComponent<ItemData>().item.Id);
            if ("Weapon".Equals(item.Type))
            {
                weapon.GetComponent<Weapon>().item = item;
            }
            else
            {
                weapon.GetComponent<Weapon>().item = null;
            }
        } else
        {
            weapon.GetComponent<Weapon>().item = null;
        }    
    }
}
