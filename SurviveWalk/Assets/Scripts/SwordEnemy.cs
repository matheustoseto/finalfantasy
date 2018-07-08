using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Weapon {
    public BoxCollider boxCol = null;
    public EnemyController enemyController;
    public IcarusPlayerController player = null;

    private bool attackOnOff = false;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.tag) && player == null)
        {
            enemyController.Attack();
            player = other.GetComponent<IcarusPlayerController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag) && attackOnOff)
        {
            enemyController.Attack();
            player = null;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if ("Player".Equals(other.tag))
    //    {
    //        enemyController.Attack();
    //    }
    //}

    public override void AttackOn()
    {
        //enemyController.Attack();
        attackOnOff = true;
        //enemyController.Attack();
        boxCol.enabled = true;
    }

    public override void AttackOff()
    {
        attackOnOff = false;
        boxCol.enabled = false;
    }
}
