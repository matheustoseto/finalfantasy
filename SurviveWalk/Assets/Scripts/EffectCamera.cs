using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCamera : MonoBehaviour {

    public GameObject target;

    private void Update()
    {
        gameObject.transform.position -= new Vector3(Time.deltaTime, 0, 0);
        gameObject.transform.position += new Vector3(0, 0, Time.deltaTime);
        lookToTarget();
    }

    void lookToTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
