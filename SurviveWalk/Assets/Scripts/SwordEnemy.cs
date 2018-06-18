using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Weapon {

    public EnemyController enemyController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            enemyController.Attack(other.GetComponent<CharacterStatus>());
        }
    }



    public override  void AttackOn()
    {
        
    }

    public override void AttackOff()
    {
        
    }


    public override void Damage(EnemyController enemyController)
    {

    }




}
