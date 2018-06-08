using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {

    Quaternion rotation;
	public Transform target;

    void Awake()
    {
		//rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.LookAt(target);
		transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
    }
}
