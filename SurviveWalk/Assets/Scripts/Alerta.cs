using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alerta : MonoBehaviour {

    private float Timer = 5F;
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
            float a = image.color.a + 5 * Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            DeltaTime -= Time.deltaTime;
        } else if(image.color.a >= 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            text.text = "";
        }
	}

    public void SetText(string txt)
    {
        text.text = txt;
        DeltaTime = Timer;
    }
}
