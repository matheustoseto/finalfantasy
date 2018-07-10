using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerTask : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Task task;
    public bool speakNpc = false;
    private Inventory inv;
    private Tooltip tooltip;

    // Use this for initialization
    void Start () {
        inv = Inventory.Instance;
        tooltip = inv.GetComponent<Tooltip>();
    }
	
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if(task.Complet && speakNpc)
            //tooltip.Activate(task.Descr);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }

    public void ChangeText()
    {
        if(speakNpc)
            gameObject.transform.GetChild(0).GetComponent<Text>().text = task.Info;
    }
}
