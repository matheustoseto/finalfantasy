using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Weapon {

    public EnemyController enemyController;


    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            enemyController.Attack();
        }
    }

    public override  void AttackOn()
    {
        enemyController.Attack();
    }

    public override void AttackOff()
    {
        
    }
}
