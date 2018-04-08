using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Craft : MonoBehaviour {

    public CharacterInventory playerInventory;
    public bool openCraft = false;
    public List<GameObject> itens;

    private void OnGUI()
    {
        if (openCraft)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, 50, 300, 200), "");
            GUI.Label(new Rect(120 + Screen.width / 2 - 150, 60, 100, 50), "Madeira : 3");
            GUI.Label(new Rect(120 + Screen.width / 2 - 150, 80, 100, 50), "Ferro : 1");
            if (GUI.Button(new Rect(10 + Screen.width / 2 - 150, 60, 100, 50), "Machado"))
            {
                Instantiate(FindItem(Utils.ResourceItem.MachadoX), transform.position + new Vector3(Random.Range(-1.9f, 1.9f), 0, Random.Range(-1.9f, 1.9f)), transform.rotation);
            }
        }
    }

    private GameObject FindItem(Utils.ResourceItem resourceItem)
    {
        GameObject item = (from it in itens
                           where it.GetComponent<Item>().item == resourceItem
                           select it).FirstOrDefault();

        return item;
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
