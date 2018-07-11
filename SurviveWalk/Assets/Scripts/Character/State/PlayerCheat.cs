using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCheat : MonoBehaviour {

    private CharacterMoveControl moveControl;

    [SerializeField] private List<Transform> listCheckPoints = new List<Transform>();
    [SerializeField] private int actualCheckPoint = 0;

    public GameObject npcGameObject;
    public GameObject alert;

    void Start () {
        moveControl = GetComponent<CharacterMoveControl>();

        if (listCheckPoints.Count != 0)
        {
            for (int i = 0; i < listCheckPoints.Count; i++)
            {
                if (listCheckPoints[i] == null)
                {
                    listCheckPoints.Clear();
                    GameObject gobj = new GameObject();
                    gobj.transform.position = transform.position;
                    gobj.transform.rotation = transform.rotation;
                    listCheckPoints.Add(gobj.transform);
                    break;
                }
            }
        }
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (actualCheckPoint + 1 >= listCheckPoints.Count)
                actualCheckPoint = 0;
            else
                actualCheckPoint++;
            moveControl.ReturnCheckPoint(listCheckPoints[actualCheckPoint].position);
        }

        // Pular Tutorial
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (NpcController.Instance.npcType.Equals(Utils.NpcType.Npc2))
            {
                npcGameObject.GetComponent<NavMeshAgent>().speed = 60;
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();

                NpcController.Instance.npcType = Utils.NpcType.Npc6;

                alert.GetComponent<Alerta>().SetText("Pular Tutorial.");
            } else
            {
                npcGameObject.GetComponent<NavMeshAgent>().speed = 6;
            }           
        }

        // Add life 100
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Inventory.Instance.AddLife(100);
            alert.GetComponent<Alerta>().SetText("100 life.");
        }

        // Add Item
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Inventory.Instance.AddItem(40);
            Inventory.Instance.AddItemQnt(2, 500);
            Inventory.Instance.AddItemQnt(3, 500);
            Inventory.Instance.AddItemQnt(5, 500);
            Inventory.Instance.AddItemQnt(6, 500);
            alert.GetComponent<Alerta>().SetText("Adicionado itens.");
        }

        // Move Speed
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (gameObject.GetComponent<CharacterMoveControl>().runSpeed == 50)
                gameObject.GetComponent<CharacterMoveControl>().runSpeed = 10;
            else
                gameObject.GetComponent<CharacterMoveControl>().runSpeed = 50;

            alert.GetComponent<Alerta>().SetText("Player Speed.");
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (GetComponent<CharacterStatus>().noDamage)
            {
                GetComponent<CharacterStatus>().noDamage = false;
                alert.GetComponent<Alerta>().SetText("No Damage False.");
            } else
            {
                GetComponent<CharacterStatus>().noDamage = true;
                alert.GetComponent<Alerta>().SetText("No Damage True.");
            }          
        }
    }
}
