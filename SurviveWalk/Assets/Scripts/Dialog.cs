using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour {

    public GateStateControl gateSouth;
    public GateStateControl gateNorth;
    public GateStateControl gateEast;
    public GameObject npcGameObject;  
    public GameObject npcPanel;
    public GameObject inventoryPanel;
    public GameObject questPlayerList;
    public Inventory inventory;
    public GameObject alert;

    public GameObject[] itensBoss;

    private Npc npc = new Npc();
    private int step = 0;
    private bool moveNpcTutorial = false;

    private string message;
    private IEnumerator coroutine;

    public void SetNpc(Npc npc)
    {
        this.npc = npc;
        Load();
    }

    private void Load()
    {
        step = 0;
        this.gameObject.transform.Find("Title").Find("Text").GetComponent<Text>().text = npc.Title;
        if (!VerifyCompletQuest())
        {
            NextStep();
        }         
    }

    public void NextStep()
    {
        if (!VerifyCompletQuest() && !verifyTuto())
        {
            if (step < npc.Intro.Count)
            {
                //gameObject.transform.Find("Text").GetComponent<Text>().text = npc.Intro[step].Step;
                SetTxt(npc.Intro[step].Step);
                step++;
            }
            else
            {
                CloseDialog();

                SetMovimentAfterStep();

                if (npc.IsCraft || npc.IsQuest)
                {
                    if (npc.IsQuest)
                    {
                        npcPanel.transform.Find("Tabs").Find("QuestTab").gameObject.SetActive(true);
                        npcPanel.GetComponent<NpcPanel>().LoadQuest(npc.Quest);
                        npcPanel.GetComponent<NpcPanel>().OpenQuestPanel();
                    }
                    else
                    {
                        npcPanel.transform.Find("Tabs").Find("QuestTab").gameObject.SetActive(false);
                    }

                    if (npc.IsCraft)
                    {
                        npcPanel.transform.Find("Tabs").Find("CraftTab").gameObject.SetActive(true);
                        npcPanel.GetComponent<NpcPanel>().LoadCraft(npc.Crafts);
                        npcPanel.GetComponent<NpcPanel>().OpenCraftPanel();
                    }
                    else
                    {
                        npcPanel.transform.Find("Tabs").Find("CraftTab").gameObject.SetActive(false);
                    }

                    if (!inventoryPanel.activeSelf)
                        inventory.ActiveDisableInventory();

                    npcPanel.SetActive(true);

                    IcarusPlayerController.Instance.IsBlockInputs = true;
                }

                if (npc.Quest.Count > 0)
                {
                    npcPanel.GetComponent<NpcPanel>().GetQuest(npc.Quest);
                }
            }
        } 
    }

    private bool VerifyCompletQuest()
    {
        foreach (Transform go in questPlayerList.transform)
        {
            foreach (Transform gameObj in go.transform)
            {
                if ("Task(Clone)".Equals(gameObj.name))
                {
                    PlayerTask playerTask = gameObj.GetComponent<PlayerTask>();

                    if (playerTask.task.Complet && !playerTask.speakNpc)
                    {
                        playerTask.speakNpc = true;
                        playerTask.ChangeText();
                        SetTxt(playerTask.task.Descr);

                        if (inventory.GetQuest(go.GetComponent<IdQuest>().questId).IsDelete)
                        {
                            Destroy(go.gameObject);
                        }

                        if (0 == go.GetComponent<IdQuest>().questId)
                        {
                            itensBoss[playerTask.task.Id].SetActive(true);

                            if (3 == playerTask.task.Id || 4 == playerTask.task.Id)
                            {
                                gateNorth.EventDevice(TypeStateDevice.Open);
                            }
                        }

                        if (5 == go.GetComponent<IdQuest>().questId)
                        {
                            gateEast.EventDevice(TypeStateDevice.Open);
                        }

                        moveNpcTutorial = true;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void CloseDialog()
    {
        this.gameObject.SetActive(false);
        IcarusPlayerController.Instance.IsBlockInputs = false;
    }

    public void CloseNpcPanel()
    {
        npcPanel.GetComponent<NpcPanel>().ClosePanel();
        IcarusPlayerController.Instance.IsBlockInputs = false;
        //inventory.DisableInventory();
    }

    public bool verifyTuto()
    {
        if (moveNpcTutorial && NpcController.Instance.npcType.Equals(Utils.NpcType.Npc3))
        {
            npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
            NpcController.Instance.npcType = Utils.NpcType.Npc4;
            moveNpcTutorial = false;
            CloseDialog();
            npcGameObject.GetComponent<NpcController>().seta.SetActive(true);
            alert.GetComponent<Alerta>().SetText("Arraste um item no slot rapido para equipa-lo.");
            return true;
        }

        if (moveNpcTutorial && NpcController.Instance.npcType.Equals(Utils.NpcType.Npc4))
        {
            npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
            NpcController.Instance.npcType = Utils.NpcType.Npc5;
            moveNpcTutorial = false;
            CloseDialog();
            npcGameObject.GetComponent<NpcController>().seta.SetActive(true);
            alert.GetComponent<Alerta>().SetText("Clique com o botão direito para usar um item.");
            return true;
        }

        if (moveNpcTutorial && NpcController.Instance.npcType.Equals(Utils.NpcType.Npc5))
        {
            npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
            NpcController.Instance.npcType = Utils.NpcType.Npc6;
            moveNpcTutorial = false;
            CloseDialog();
            npcGameObject.GetComponent<NpcController>().seta.SetActive(true);
            alert.GetComponent<Alerta>().SetText("Itens possuem durabilidade. Fique ligado!");
            return true;
        }

        return false;
    }

    public void SetMovimentAfterStep()
    {
        if (NpcController.Instance.npcType.Equals(Utils.NpcType.Npc2))
        {
            npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
            NpcController.Instance.npcType = Utils.NpcType.Npc3;
            moveNpcTutorial = false;
            CloseDialog();
            npcGameObject.GetComponent<NpcController>().seta.SetActive(true);
            alert.GetComponent<Alerta>().SetText("Use a tecla 'I' para Abrir/Fechar o inventário.");
        }

        if (NpcController.Instance.npcType.Equals(Utils.NpcType.Npc6))
        {
            gateSouth.EventDevice(TypeStateDevice.Open);
            npcGameObject.GetComponent<NPCStateControl>().EventPatrol();
            NpcController.Instance.npcType = Utils.NpcType.Npc1;
            moveNpcTutorial = false;
            CloseDialog();
            npcGameObject.GetComponent<NpcController>().seta.SetActive(false);
            alert.GetComponent<Alerta>().SetText("Busque recursos e contrua equipamentos mais fortes.");
        }
    }

    private void SetTxt(string text)
    {
        //StopAllCoroutines();
        if(coroutine != null)
            StopCoroutine(coroutine);

        message = text;
        gameObject.transform.Find("Text").GetComponent<Text>().text = "";
        coroutine = TypeText(0.0001f);
        StartCoroutine(coroutine);
    }

    public IEnumerator TypeText(float waitTime) {
        foreach (char letter in message.ToCharArray())
        {
            gameObject.transform.Find("Text").GetComponent<Text>().text += letter;
            yield return new WaitForSeconds(waitTime);
        }      
    }
}
