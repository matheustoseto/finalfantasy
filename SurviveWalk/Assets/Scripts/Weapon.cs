using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Item item;
    private bool attck = false;
    private Vector3 originalPosition;

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
        attck = true;
        transform.localPosition += new Vector3(0, 0, 0.6f);
    }

    public virtual void AttackOff()
    {
        attck = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Enemy".Equals(other.tag) && attck)
        {
            Damage(other.GetComponent<Skeleton>().enemyController);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ("Enemy".Equals(other.tag) && attck)
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
}
