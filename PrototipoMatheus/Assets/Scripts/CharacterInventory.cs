using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInventory : MonoBehaviour {

    public List<Item> itens = new List<Item>();

    private void OnGUI()
    {
        int y = 50;
        foreach (Item item in itens){
            GUI.Label(new Rect(5, y, 100, 100), item.nome +": " + item.qnt);
            y += 20;
        }
    }

    public void AddItemIventory(Item addItem)
    {
        Item item = (from it in itens
                     where it.GetComponent<Item>().item == addItem.item
                     select it).FirstOrDefault();
        if (item != null)
        {
            item.qnt += addItem.qnt;
        } else
        {
            itens.Add(addItem);
        }
    }
}
