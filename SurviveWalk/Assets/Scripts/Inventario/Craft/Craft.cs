using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Craft : MonoBehaviour {

    public Inventory inventory;
    private bool openCraft = false;
    public List<Utils.CraftItem> craftItens;

    private void OnGUI()
    {
        if (openCraft)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, 50, 300, 200), "");

            float y1 = 60;
            float y2 = 60;

            foreach (Utils.CraftItem craftItem in craftItens)
            {
                Item item = inventory.FindItem(craftItem.idItem);
                if (GUI.Button(new Rect(10 + Screen.width / 2 - 150, y1, 100, 50), item.Title))
                {
                    bool isCraft = false;
                    int craftSize = craftItem.craftCombination.Count;
                    int i = 0;
                    foreach (Utils.CraftCombination craftCombination in craftItem.craftCombination)
                    {               
                        foreach (Item it in inventory.items)
                        {
                            if (it.Id == craftCombination.idItem)
                            {
                                i++;
                                if (inventory.FindItemData(it.Id).amount >= craftCombination.qnt)
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
                        foreach (Utils.CraftCombination craftCombination in craftItem.craftCombination)
                        {
                            inventory.RemoveItem(craftCombination.idItem, craftCombination.qnt);
                        }
                    }
                }
                foreach (Utils.CraftCombination craftCombination in craftItem.craftCombination)
                {
                    GUI.Label(new Rect(120 + Screen.width / 2 - 150, y2, 100, 50), inventory.FindItem(craftCombination.idItem).Title + ": " + craftCombination.qnt);
                    y2 += 20;
                }
                y1 += 20;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            openCraft = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            openCraft = false;
        }
    }
}
