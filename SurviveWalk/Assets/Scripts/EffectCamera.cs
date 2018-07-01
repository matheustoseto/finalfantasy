using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCamera : MonoBehaviour {

    public GameObject target;

	void Update () {
        lookToTarget();
        transform.position -= new Vector3(0.03f,0,0);
        transform.position += new Vector3(0, 0, 0.03f);
    }

    void lookToTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
