using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
