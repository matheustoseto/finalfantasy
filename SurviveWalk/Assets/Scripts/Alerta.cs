using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alerta : MonoBehaviour {

    private float Timer = 3F;
    private float DeltaTime = 0f;
    private Text text;
    private Image image;

    private void Start()
    {
        text = this.transform.GetChild(0).GetComponent<Text>();
        image = GetComponent<Image>();
    }

    void Update () {
        if (DeltaTime >= 0F)
        {
            if (image.color.a < 255)
            {
                float a = image.color.a + 5 * Time.deltaTime;
                image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            }           
            DeltaTime -= Time.deltaTime;
        } else if(image.color.a >= 0 && DeltaTime < 0)
        {
            float a = image.color.a - 10 * Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            if(image.color.a < 1)
                text.text = "";
        }
	}

    public void SetText(string txt)
    {
        text.text = txt;
        DeltaTime = Timer;
    }
}
