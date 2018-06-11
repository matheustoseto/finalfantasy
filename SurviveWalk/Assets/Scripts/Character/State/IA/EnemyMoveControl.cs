using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveControl : MonoBehaviour {

    private NavMeshAgent navAgent;


    [Header("Reference:")]
    [SerializeField] private GameObject bodyMiniMap = null;
    [SerializeField] private Transform  body = null;



    private Vector3 enemyInitialPos;




    #region Properties

    public Transform Body { get { return body; } }
    public GameObject BodyMiniMap { get { return bodyMiniMap; } }
    public float Magnitude { get { return navAgent.velocity.magnitude; } }

    #endregion


    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
        
        enemyInitialPos = transform.position;
        Stop();

        if (bodyMiniMap != null)
            bodyMiniMap.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        

    }


    public void Move(Vector3 pointReference)
    {
        
        navAgent.isStopped = false;
        navAgent.SetDestination(pointReference);
        //navAg.speed = speed;
    }

    public void Stop()
    {

        navAgent.isStopped = true;
    }


    

    

}
