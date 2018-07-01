using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    private float timer = 0.1f;

	void Update () {
        if (timer < 0)
        {
            this.gameObject.SetActive(false);
            timer = 0.1f;
        } else
        {
            timer -= Time.deltaTime;
        }
	}
}
