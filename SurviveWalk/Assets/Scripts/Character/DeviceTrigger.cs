using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour {

    [SerializeField] private GateStateControl gate;
    [SerializeField] private TypeStateDevice state = TypeStateDevice.Idle;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (state != gate.State)
                gate.EventDevice(state);
        }
    }
}
