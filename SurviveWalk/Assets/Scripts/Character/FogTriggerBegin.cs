using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTriggerBegin : MonoBehaviour {
    [SerializeField] private Camera camera = null;
    [SerializeField] private Transform player = null;

    [Header("Settings:")]
    [SerializeField] private float fogAmount = 0.2f;
    [SerializeField] private FogTriggerBegin begin = null;
    private bool isImBegin = false;
    private bool isStart = false;

	// Use this for initialization
	void Start () {
        camera = CameraControl.GetInstance().GetComponent<Camera>();
        player = IcarusPlayerController.GetInstance();
        if (gameObject.name == "Begin")
            isImBegin = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isImBegin)
        {
            isStart = true;
        }
    }


}
