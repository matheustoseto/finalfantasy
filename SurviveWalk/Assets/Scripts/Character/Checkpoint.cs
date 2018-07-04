using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private CharacterMoveControl playerMoveControl = null;

    [SerializeField] private Transform point = null;
	// Use this for initialization
	void Start () {
        playerMoveControl = IcarusPlayerController.player.GetComponent<CharacterMoveControl>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerMoveControl.tag && point != null)
        {
            playerMoveControl.CheckPoint = point;

        }
    }
}
