using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPCStateControl : MonoBehaviour {

    private NPCStateControl npcControl = null;

    [Header("Events:")]
    [SerializeField] private bool isEventDead;
    [SerializeField] private bool isEventPatrol;

    // Use this for initialization
    void Start () {
        npcControl = GetComponent<NPCStateControl>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isEventDead)
        {
            isEventDead = false;
            npcControl.EventDead();
        }

        if (isEventPatrol)
        {
            isEventPatrol = false;
            npcControl.EventPatrol();
        }
	}
}
