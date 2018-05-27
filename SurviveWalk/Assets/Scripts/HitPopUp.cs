using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPopUp : MonoBehaviour {

    public TextMesh text;
    private float timer = 0.9F;

    void Update()
    {
        if (timer < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
            transform.position += new Vector3(0, 0.09F, 0);
        }
    }

    public void SetText(string txt)
    {
        text.text = txt;
    }
}
