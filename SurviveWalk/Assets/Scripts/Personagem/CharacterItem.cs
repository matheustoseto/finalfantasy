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
        if (other.GetComponent<ItemResource>() != null)
        {
            Destroy(other.gameObject);
            inventory.AddItem(other.GetComponent<ItemResource>().idItem);
            GameObject item = Instantiate(getItem, other.gameObject.transform.position + new Vector3(0,1,0), Quaternion.identity);
            item.GetComponent<SpriteRenderer>().sprite = inventory.FindItem(other.GetComponent<ItemResource>().idItem).Sprite;

            VerifyQuest(other.GetComponent<ItemResource>());
        }   
    }

    public void GetItem(int idItem, int qnt)
    {
        for (int i = 1; i <= qnt; i++)
        {
            inventory.AddItem(idItem);           
        }

        GameObject item = Instantiate(getItem, gameObject.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        item.GetComponent<SpriteRenderer>().sprite = inventory.FindItem(idItem).Sprite;
        item.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + qnt;
    }

    private void VerifyQuest(ItemResource itemResource)
    {
        foreach (Transform go in questPlayerList.transform)
        {
            if(itemResource.idQuest == go.GetComponent<IdQuest>().questId)
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
