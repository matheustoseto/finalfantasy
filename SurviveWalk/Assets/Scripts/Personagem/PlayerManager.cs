using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour {

    public GameObject player;
    public GameObject npcGameObject;

    private bool LeftAlt = false;

    private static PlayerManager instance = null;
    public static PlayerManager Instance { get { return instance; } }

    private void Start()
    {
        instance = this;
        player = this.gameObject;
    }

    void Awake()
    {
        instance = this;
        player = this.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            LeftAlt = true;
        if (Input.GetKeyUp(KeyCode.LeftAlt))
            LeftAlt = false;

        // Pular Tutorial
        if (LeftAlt && Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (npcGameObject != null)
            {
                npcGameObject.GetComponent<NavMeshAgent>().speed = 60;
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
                npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
            }
            NpcController.Instance.npcType = Utils.NpcType.Npc6;
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
            this.gameObject.GetComponent<CharacterMoveControl>().runSpeed = 50;
        }
    }
}
