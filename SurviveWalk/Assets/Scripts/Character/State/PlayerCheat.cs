using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCheat : MonoBehaviour {

    private CharacterMoveControl moveControl;

    [SerializeField] private List<Transform> listCheckPoints = new List<Transform>();
    [SerializeField] private int actualCheckPoint = 0;

    public GameObject npcGameObject;

    private bool LeftAlt = false;

    // Use this for initialization
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
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (actualCheckPoint + 1 >= listCheckPoints.Count)
                actualCheckPoint = 0;
            else
                actualCheckPoint++;
            moveControl.ReturnCheckPoint(listCheckPoints[actualCheckPoint].position);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
            LeftAlt = true;
        if (Input.GetKeyUp(KeyCode.LeftAlt))
            LeftAlt = false;

        // Pular Tutorial
        if (LeftAlt && Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!NpcController.Instance.npcType.Equals(Utils.NpcType.Npc6))
            {
                npcGameObject.GetComponent<NavMeshAgent>().speed = 60;
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();

                NpcController.Instance.npcType = Utils.NpcType.Npc6;
            } else
            {
                npcGameObject.GetComponent<NavMeshAgent>().speed = 6;
            }
            
        }

        // Add life 100
        if (LeftAlt && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Inventory.Instance.AddLife(100);
        }

        // Add espada
        if (LeftAlt && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Inventory.Instance.AddItem(1001);
        }

        // Move Speed
        if (LeftAlt && Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (gameObject.GetComponent<CharacterMoveControl>().runSpeed == 50)
                gameObject.GetComponent<CharacterMoveControl>().runSpeed = 10;
            else
                gameObject.GetComponent<CharacterMoveControl>().runSpeed = 50;
        }
    }
}
