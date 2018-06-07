using System;
using UnityEngine;

public class Resource : MonoBehaviour {

    public Utils.ResourceType type;
    public int idItem;
    public int qnt;
    public float speed;
    public float timer = 1f;

    public GameObject item;
    public GameObject objRender;
    public GameObject active;
    public Material onMouseOver;
    private Material onMouseExit;

    private GameObject player;
    private GameObject alert;

    private bool playerEnter = false;
    private float deltaTimer;
    private bool create = false;

    private Item selectItem;
    //private Vector3 originalScale;

    private void Start()
    {
        //originalScale = this.gameObject.transform.localScale;
        onMouseExit = objRender.GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player");
        alert = GameObject.FindGameObjectWithTag("Alerta");
    }

    private void Update()
    {
        if (deltaTimer <= 0 && create)
        {
            //this.gameObject.transform.localScale = originalScale;
            active.SetActive(true);
            create = false;
        } else if (deltaTimer >= 0 && create)
        {
            deltaTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.tag) && !create)
        {
            playerEnter = true;
            objRender.GetComponent<Renderer>().material = onMouseOver;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag) && !create)
        {
            objRender.GetComponent<Renderer>().material = onMouseOver;
            if (Input.GetKeyDown(KeyCode.E))
            {
                CraftItem();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            playerEnter = false;
            objRender.GetComponent<Renderer>().material = onMouseExit;
            Progress.Instance.DisableProgressBar();
        }
    }

    private void OnMouseOver()
    {
        if (playerEnter && !create)
        {
            //GetComponent<Renderer>().material = onMouseOver;

            if (Input.GetMouseButtonDown(0)){
                CraftItem();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //Progress.Instance.DisableProgressBar();
            }
               
        }
    }

    private void CraftItem()
    {
        selectItem = player.GetComponent<SlotSelect>().GetSelectItemBySlot();
        if (Utils.PodeCraftar(type, selectItem))
        {
            Progress.Instance.ProgressBar(speed, new Action(CreateItem));
        }
        else
        {
            alert.GetComponent<Alerta>().SetText(Utils.PodeCraftarDS(type));
        }
    }

    private void OnMouseExit()
    {
        if (playerEnter)
        {
            //GetComponent<Renderer>().material = onMouseExit;
            //Progress.Instance.DisableProgressBar();
        }
    }

    public void CreateItem()
    {
        if (!Utils.PodeCraftarSemMaterial(type))
        {
            Inventory.Instance.RemoveDurability(selectItem);
        }
            
        item.GetComponent<ItemResource>().idItem = idItem;
        for (int i = 1; i <= qnt; i++)
        {
            Vector3 pos = player.transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), 0, UnityEngine.Random.Range(-2f, 2f));
            Instantiate(item, pos, transform.rotation);
        }
        DisableItem();
    }

    private void DisableItem()
    {
        deltaTimer = timer;
        //this.gameObject.transform.localScale = new Vector3(0.5F, this.gameObject.transform.localScale.y, 0.5F);
        active.SetActive(false);
        create = true;
        objRender.GetComponent<Renderer>().material = onMouseExit;
    }
}
