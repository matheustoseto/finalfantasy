using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiniMap : CameraFollow {

    //[SerializeField] private float distance = 10;



    // Use this for initialization
    void Start () {
		
	}

    protected override void Follow()
    {
        //Vector3 distancePoint = TargetObject.GetTransform().position;
        //distancePoint.y = Mathf.Abs(distance);

        Vector3 distancePoint = TargetObject.GetTransform().position + new Vector3(0,70,0);
        //distancePoint.y = transform.position.y;

        transform.position =  distancePoint;
    }

}
