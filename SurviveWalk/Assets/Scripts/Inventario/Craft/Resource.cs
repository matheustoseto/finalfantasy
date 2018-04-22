using System;
using UnityEngine;

public class Resource : MonoBehaviour {

    public Utils.ResourceType type;
    public int idItem;
    public int qnt;
    public float speed;

    public GameObject item; 
    public Material onMouseOver;
    private Material onMouseExit;

    private GameObject player;
    private GameObject alert;

    private bool playerEnter = false;
    private bool mouseEnter = false;

    private void Start()
    {
        onMouseExit = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player");
        alert = GameObject.FindGameObjectWithTag("Alerta");
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            playerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            playerEnter = false;
            GetComponent<Renderer>().material = onMouseExit;
            Progress.Instance.DisableProgressBar();
        }
    }

    private void OnMouseOver()
    {
        if (playerEnter)
        {
            GetComponent<Renderer>().material = onMouseOver;
            mouseEnter = true;

            if (Input.GetMouseButtonDown(0)){
                Item item = player.GetComponent<SlotSelect>().GetSelectItemBySlot();
                if (Utils.PodeCraftar(type, item))
                {
                    Progress.Instance.ProgressBar(speed, new Action(CreateItem));
                } else
                {
                    alert.GetComponent<Alerta>().SetText(Utils.PodeCraftarDS(type));
                }
            }else if (Input.GetMouseButtonUp(0))
            {
                Progress.Instance.DisableProgressBar();
            }
               
        }
    }

    private void OnMouseExit()
    {
        if (playerEnter)
        {
            GetComponent<Renderer>().material = onMouseExit;
            mouseEnter = false;
            Progress.Instance.DisableProgressBar();
        }
    }

    public void CreateItem()
    {
        item.GetComponent<ItemResource>().idItem = idItem;
        for (int i = 1; i <= qnt; i++)
        {
            Instantiate(item, transform.position + new Vector3(UnityEngine.Random.Range(-0.9f, 0.9f), 0, UnityEngine.Random.Range(-0.9f, 0.9f)), transform.rotation);            
        }
        Destroy(gameObject);
    }
}
