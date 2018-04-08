using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public Utils.ResourceType type;
    public GameObject item;
    public int qnt;

    public Material onMouseOver;
    public Material onMouseExit;

    private bool playerEnter = false;
    private bool mouseEnter = false;

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
        }
    }

    private void OnMouseOver()
    {
        if (playerEnter)
        {
            GetComponent<Renderer>().material = onMouseOver;
            mouseEnter = true;
        }
    }

    private void OnMouseExit()
    {
        if (playerEnter)
        {
            GetComponent<Renderer>().material = onMouseExit;
            mouseEnter = false;
        }
    }

    private void OnMouseUp()
    {
        if (playerEnter && mouseEnter)
        {
            for (int i = 1; i <= qnt; i++)
            {
                Instantiate(item, transform.position + new Vector3(Random.Range(-0.9f,0.9f), 0, Random.Range(-0.9f, 0.9f)), transform.rotation);
            }     
            Destroy(gameObject);
        }   
    }
}
