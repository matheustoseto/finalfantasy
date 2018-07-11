using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour {

    public Utils.NpcType npcType;
    public GameObject dialogPanel;
    public QuestNpc questNpc;
    public GameObject seta;
    public GameObject alert;

    private static NpcController instance = null;
    public static NpcController Instance { get { return instance; } }

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.gameObject.tag))
        {
            if (npcType.Equals(Utils.NpcType.Npc2))
            {
                alert.GetComponent<Alerta>().SetText("Aperte a tecla E para conversar.");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.gameObject.tag))
        {
            if (!dialogPanel.activeSelf)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    SoundControl.GetInstance().ExecuteEffect(TypeSound.Question);
                    dialogPanel.SetActive(true);
                    dialogPanel.GetComponent<Dialog>().SetNpc(Inventory.Instance.GetNpcData(npcType.GetHashCode()));     
                    IcarusPlayerController.Instance.IsBlockInputs = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.gameObject.tag))
        {
            dialogPanel.GetComponent<Dialog>().CloseDialog();
            dialogPanel.GetComponent<Dialog>().CloseNpcPanel();
            IcarusPlayerController.Instance.IsBlockInputs = false;
        }
    }
}
