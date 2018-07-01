using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour {

    public Utils.NpcType npcType;
    public GameObject dialogPanel;
    public QuestNpc questNpc;
    public GameObject seta;

    private bool playerEnter = false;

    private static NpcController instance = null;
    public static NpcController Instance { get { return instance; } }

    private void Start()
    {
        instance = this;
    }

    private void OnMouseOver()
    {
        if (playerEnter && !dialogPanel.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogPanel.GetComponent<Dialog>().SetNpc(Inventory.Instance.GetNpcData(npcType.GetHashCode()));
                dialogPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.gameObject.tag))
        {
            playerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.gameObject.tag))
        {
            playerEnter = false;
            dialogPanel.GetComponent<Dialog>().CloseDialog();
            dialogPanel.GetComponent<Dialog>().CloseNpcPanel();
        }
    }
}
