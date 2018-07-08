using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour {

    private float timer = 0.9F;

	void Update () {

        if (timer < 0)
        {
            Destroy(gameObject);
        } else
        {
            timer -= Time.deltaTime;
            transform.position += new Vector3(0, Time.deltaTime, 0);
        }        
	}
}
