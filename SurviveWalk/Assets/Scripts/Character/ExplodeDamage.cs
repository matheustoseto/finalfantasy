using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDamage : MonoBehaviour {

    [SerializeField] private EnemyController enemyStatus = null;
    [SerializeField] private GameObject particulas = null;


	// Use this for initialization
	void Start () {
        enemyStatus = transform.parent.GetComponentInChildren<EnemyController>();
        particulas = GetComponentInChildren<ParticleSystem>().gameObject;
        gameObject.SetActive(false);
        particulas.SetActive(false);

    }

    public void EnableParticle()
    {
        particulas.SetActive(true);
    }

    public void DisableParticle()
    {
        particulas.SetActive(false);
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
