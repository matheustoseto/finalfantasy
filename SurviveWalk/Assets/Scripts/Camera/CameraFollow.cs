using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Target target;

    public Target TargetObject { get { return target; } }

    // Use this for initialization
    void Start () {
		
	}
	

	void LateUpdate () {
        Follow();
	}


    protected virtual void Follow()
    {
        transform.position = target.GetTransform().position;
    }
}
