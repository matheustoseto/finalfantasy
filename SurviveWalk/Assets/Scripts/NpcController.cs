using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour {

    public GameObject dialogPanel;
    public Utils.NpcType npcType;

    private bool playerEnter = false;

	void Start () {
		
	}
	
	void Update () {
		
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
