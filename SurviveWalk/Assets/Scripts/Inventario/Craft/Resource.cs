using System;
using UnityEngine;

public class Resource : MonoBehaviour {

    public Utils.ResourceType type;
    public int idItem;
    public int qnt;
    public float speed;
    public float timer = 1f;

    public GameObject item; 
    public Material onMouseOver;
    private Material onMouseExit;

    private GameObject player;
    private GameObject alert;

    private bool playerEnter = false;
    private float deltaTimer;
    private bool create = false;

    private Item selectItem;

    private void Start()
    {
        onMouseExit = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player");
        alert = GameObject.FindGameObjectWithTag("Alerta");
    }

    private void Update()
    {
        if (deltaTimer <= 0 && create)
        {
            this.gameObject.transform.localScale = new Vector3(1F, 1F, 1F);
            create = false;
        } else if (deltaTimer >= 0 && create)
        {
            deltaTimer -= Time.deltaTime;
        }
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
        if (playerEnter && !create)
        {
            GetComponent<Renderer>().material = onMouseOver;

            if (Input.GetMouseButtonDown(0)){
                selectItem = player.GetComponent<SlotSelect>().GetSelectItemBySlot();
                if (Utils.PodeCraftar(type, selectItem))
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
            Progress.Instance.DisableProgressBar();
        }
    }

    public void CreateItem()
    {
        if(selectItem != null && !Utils.PodeCraftarSemMaterial(type))
            Inventory.Instance.RemoveDurability(selectItem);
        item.GetComponent<ItemResource>().idItem = idItem;
        for (int i = 1; i <= qnt; i++)
        {
            Instantiate(item, transform.position + new Vector3(UnityEngine.Random.Range(-0.9f, 0.9f), 0, UnityEngine.Random.Range(-0.9f, 0.9f)), transform.rotation);            
        }
        DisableItem();
    }

    private void DisableItem()
    {
        deltaTimer = timer;
        this.gameObject.transform.localScale = new Vector3(0.5F,0.5F,0.5F);
        create = true;
    }
}
