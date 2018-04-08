using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItem : MonoBehaviour {

    private CharacterStatus characterStatus;
    private CharacterInventory characterInventory;


    // Use this for initialization
    void Start () {
        characterStatus = GetComponent<CharacterStatus>();
        characterInventory = GetComponent<CharacterInventory>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Item>() != null)
        {
            Utils.ResourceType type = other.GetComponent<Item>().type;
            if (type == Utils.ResourceType.Comida)
            {
                characterStatus.fome += other.GetComponent<Item>().qnt;
                Destroy(other.gameObject);
            } else
            {
                characterInventory.AddItemIventory(other.GetComponent<Item>());
                other.gameObject.SetActive(false);
            }
        }   
    }


}
