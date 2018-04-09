using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItem : MonoBehaviour {

    public Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemResource>() != null)
        {
            inventory.AddItem(other.GetComponent<ItemResource>().idItem);
            Destroy(other.gameObject);
        }   
    }

}
