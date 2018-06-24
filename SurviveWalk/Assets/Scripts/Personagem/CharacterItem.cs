using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour {

    public Inventory inventory;
    public GameObject getItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemResource>() != null)
        {
            Destroy(other.gameObject);
            inventory.AddItem(other.GetComponent<ItemResource>().idItem);
            GameObject item = Instantiate(getItem, other.gameObject.transform.position + new Vector3(0,1,0), Quaternion.identity);
            item.GetComponent<SpriteRenderer>().sprite = inventory.FindItem(other.GetComponent<ItemResource>().idItem).Sprite;          
        }   
    }

    public void GetItem(int idItem, int qnt)
    {
        for (int i = 1; i <= qnt; i++)
        {
            inventory.AddItem(idItem);
            GameObject item = Instantiate(getItem, gameObject.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
            item.GetComponent<SpriteRenderer>().sprite = inventory.FindItem(idItem).Sprite;
            item.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + qnt;
        }       
    }
}
