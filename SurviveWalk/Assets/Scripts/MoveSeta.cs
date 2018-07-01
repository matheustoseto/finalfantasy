using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSeta : MonoBehaviour {

    private bool up = false;
    private float yMax;
    private float yMin;

    private void Start()
    {
        yMax = transform.localPosition.y;
        yMin = yMax - 0.5f;
    }

    void Update () {

        if (up)
        {
            transform.localPosition += new Vector3(0, 0.09F, 0);

            if (transform.localPosition.y >= yMax)
            {
                up = false;
            }
        }
        else
        {       
            transform.localPosition -= new Vector3(0, 0.09F, 0);

            if (transform.localPosition.y <= yMin)
            {
                up = true;
            }
        }
    }
}
