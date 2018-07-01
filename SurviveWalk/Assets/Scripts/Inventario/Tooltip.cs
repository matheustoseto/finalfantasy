﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
	private Item item;
	private string data;
	private GameObject tooltip;
    private Vector3 pos;

	void Start()
	{
		tooltip = GameObject.Find("Tooltip");
		tooltip.SetActive(false);
	}

	void Update()
	{
		if (tooltip.activeSelf)
		{
            tooltip.transform.position = Input.mousePosition + pos;
        }
	}

    public void Activate(string txt)
    {
        tooltip.transform.GetChild(0).GetComponent<Text>().text = txt;
        tooltip.SetActive(true);
        pos = new Vector3(-270, 0, 0);
    }


    public void Activate(Item item)
	{
		this.item = item;
		ConstructDataString();
		tooltip.SetActive(true);
        pos = new Vector3(0, 70, 0);
    }

	public void Deactivate()
	{
		tooltip.SetActive(false);
	}

	public void ConstructDataString()
	{
        data = "<color=#FFEC58FF><b>" + item.Title + "</b></color>";

        if(item.Durability > 0)
            data += " (" + item.DurabilityCount + "/" + item.Durability + ")";

        data += "\n\n" + item.Description;

        if ("Weapon".Equals(item.Type) || "Machado".Equals(item.Type) || "Picareta".Equals(item.Type))
        {
            data+= "\n<color=#FFEC58FF><b>Power:</b></color> " + item.Power;
        } else if ("Food".Equals(item.Type))
        {
            data += "\n<color=#FFEC58FF><b>Vida:</b></color> " + item.Power;
        } else
        {
            data += "\n<color=#FFEC58FF><b>" + item.Type + "</b></color>";
        }

        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
	}

}
