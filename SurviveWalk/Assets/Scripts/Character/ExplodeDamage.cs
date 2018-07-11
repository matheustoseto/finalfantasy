using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDamage : MonoBehaviour {

    [SerializeField] private EnemyController enemyStatus = null;
    [SerializeField] private GameObject particulas = null;
    private SphereCollider sphereCol = null;


	// Use this for initialization
	void Start () {
        enemyStatus = transform.parent.GetComponentInChildren<EnemyController>();
        particulas = GetComponentInChildren<ParticleSystem>().gameObject;
        sphereCol = GetComponent<SphereCollider>();
        gameObject.SetActive(false);
        particulas.SetActive(false);
        sphereCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableParticle()
    {
        particulas.SetActive(true);
        sphereCol.enabled = true;
    }

    public void DisableParticle()
    {
        particulas.SetActive(false);
        sphereCol.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyStatus.Attack();
            sphereCol.enabled = false;
        }
    }
}
