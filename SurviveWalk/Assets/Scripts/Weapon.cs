using System;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour {

    [Serializable]
    private class ToolItem
    {
        [SerializeField] private string name = "";
        [SerializeField] private TypeTool typeTool = TypeTool.Axe;
        [SerializeField] private GameObject tool = null;


        public string Name       { get { return name; }  set { name = value; } }
        public TypeTool TypeTool { get { return typeTool; } }
        public GameObject Tool   { get { return tool; } }
    }
    public TrailRenderer trailRender = null;

    public Item item;
    private bool attack = false;
    private Vector3 originalPosition;

    [SerializeField] private List<ToolItem> listTools = new List<ToolItem>();



    private void Awake()
    {
        for (int i = 0; i < listTools.Count; i++)
        {
            if (listTools[i].Name == "")
                listTools[i].Name = listTools[i].TypeTool.ToString();
        }

        if (trailRender != null)
            trailRender.gameObject.SetActive(false);
    }

    void Start () {
        originalPosition = transform.localPosition;
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //Attack();
        } else if (Input.GetMouseButtonUp(0))
        {
            //attck = false;
            //transform.localPosition = originalPosition;
        }

        
    }

    public virtual void AttackOn()
    {
        attack = true;
        transform.localPosition += new Vector3(0, 0, 0.6f);
    }

    public virtual void AttackOff()
    {
        attack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Enemy".Equals(other.tag) && attack)
        {
            Skeleton skeleton = other.GetComponent<Skeleton>();
            EnemyController skeletonController = skeleton.enemyController;
            Damage(skeletonController);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ("Enemy".Equals(other.tag) && attack)
        {
            Damage(other.GetComponent<Skeleton>().enemyController);
        }
    }

    public virtual void Damage(EnemyController enemyController)
    {
        if (item != null)
        {
            if (enemyController.RemoveLife(item.Power))
            {
                if (Inventory.Instance.RemoveDurability(item))
                {
                    item = null;
                }
            }
        }
        else
        {
            enemyController.RemoveLife(1);
        }

        transform.localPosition = originalPosition;
    }

    public void SetActiveTool(int idItem)
    {
        TypeTool typeTool = GetTypeTool(idItem);
        SetActiveTool(typeTool);
    }

    public void SetActiveTool(TypeTool typeToolActive)
    {
        for (int i = 0; i < listTools.Count; i++)
        {
            if (listTools[i].TypeTool == typeToolActive)
                listTools[i].Tool.SetActive(true);
            else
                listTools[i].Tool.SetActive(false);
        }
    }

    public TypeTool GetTypeTool(int idItem)
    {
        TypeTool typeTool = (TypeTool)idItem;

        switch (typeTool)
        {
            case TypeTool.AxePrimitive : typeTool = TypeTool.AxePrimitive ; break;
            case TypeTool.Axe          : typeTool = TypeTool.Axe          ; break;
            case TypeTool.AxeMetal     : typeTool = TypeTool.AxeMetal     ; break;
            case TypeTool.PickPrimitive: typeTool = TypeTool.PickPrimitive; break;
            case TypeTool.Pick         : typeTool = TypeTool.Pick         ; break;
            case TypeTool.PickMetal    : typeTool = TypeTool.PickMetal    ; break;
            case TypeTool.SwordWood    : typeTool = TypeTool.SwordWood    ; break;
            case TypeTool.SwordLegend  : typeTool = TypeTool.SwordLegend  ; break;
            case TypeTool.SwordMetal   : typeTool = TypeTool.SwordMetal   ; break;
            default:
                typeTool = TypeTool.None;
                break;
        }
        return typeTool;
    }

    public void TrailRenderActivated(bool isActivatedTrailRender)
    {
        if (trailRender != null)
            trailRender.gameObject.SetActive(isActivatedTrailRender);
    }

}
