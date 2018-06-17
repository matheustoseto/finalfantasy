using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour {

    public EnemyController enemyController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            enemyController.Attack(other.GetComponent<CharacterStatus>());
        }
    }
}
