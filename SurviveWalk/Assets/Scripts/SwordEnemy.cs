using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Weapon {

    public EnemyController enemyController;

    private bool attackOnOff = false;

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag) && attackOnOff)
        {
            enemyController.Attack();
        }
    }

    public override void AttackOn()
    {
        //enemyController.Attack();
        attackOnOff = true;
    }

    public override void AttackOff()
    {
        attackOnOff = false;
    }
}
