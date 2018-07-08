using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour {

    public Inventory inventory;
    public GameObject getItem;
    public GameObject questPlayerList;
    public QuestNpc questNpc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemResourceSet>() != null)
        {
            ItemResourceSet itemResourceSet = other.GetComponent<ItemResourceSet>();

            Destroy(other.gameObject);
            inventory.AddItem(itemResourceSet.idItem);
            GameObject item = Instantiate(getItem, other.gameObject.transform.position + new Vector3(0,1,0), Quaternion.identity);
            item.GetComponent<SpriteRenderer>().sprite = inventory.FindItem(itemResourceSet.idItem).Sprite;

            ItemResource itemRes = new ItemResource(itemResourceSet.idItem, itemResourceSet.idQuest, itemResourceSet.idTask);

            VerifyQuest(itemRes);
        }   
    }

    public void GetItem(ItemResource itemResource, int qnt)
    {
        for (int i = 1; i <= qnt; i++)
        {
            inventory.AddItem(itemResource.idItem);           
        }

        GameObject item = Instantiate(getItem, gameObject.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        item.GetComponent<SpriteRenderer>().sprite = inventory.FindItem(itemResource.idItem).Sprite;
        item.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + qnt;

        VerifyQuest(itemResource);
    }

    private void VerifyQuest(ItemResource itemResource)
    {
        foreach (Transform go in questPlayerList.transform)
        {
            if(!inventory.GetQuest(go.GetComponent<IdQuest>().questId).IsDelete && itemResource.idQuest == go.GetComponent<IdQuest>().questId)
            {
                foreach (Transform gameObj in go.transform)
                {
                    if ("Task(Clone)".Equals(gameObj.name))
                    {
                        PlayerTask playerTask = gameObj.GetComponent<PlayerTask>();

                        if (itemResource.idTask == playerTask.task.Id)
                        {
                            Destroy(gameObj.gameObject);

                            if (go.transform.childCount == 2)
                            {
                                Destroy(go.gameObject);

                                if (0 == itemResource.idQuest)
                                {
                                    QuestItem questItem = new QuestItem();
                                    questItem.quest = inventory.GetQuest(4);

                                    questNpc.GetQuest(questItem);

                                    NpcController.Instance.npcType = Utils.NpcType.Npc7;
                                }                                
                            }    
                        }
                    }
                }
            }
        }
    }
}
