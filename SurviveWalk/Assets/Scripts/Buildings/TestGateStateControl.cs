using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGateStateControl : MonoBehaviour {
    private GateStateControl gateControl = null;


    [SerializeField] private TypeStateDevice newStateDevice = TypeStateDevice.Idle;
    [SerializeField] private bool isEventDevice = false;

    // Use this for initialization
    void Start () {
        gateControl = GetComponent<GateStateControl>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isEventDevice)
        {
            isEventDevice = false;
            gateControl.EventDevice(newStateDevice);
        }
    }
}
