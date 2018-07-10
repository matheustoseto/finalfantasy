using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDamage : MonoBehaviour {

    [SerializeField] private EnemyController enemyStatus = null;

	// Use this for initialization
	void Start () {
        enemyStatus = transform.parent.GetComponentInChildren<EnemyController>();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyStatus.Attack();
        }
    }
}
