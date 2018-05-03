using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour {

    public GameObject lifeBar;
    public GameObject startPoint;
    public Text lifeText;
    public float life;
    public float lifeProgress;

    private bool redFlag = true;
    private float a = 170;
    private Color originalColor;

    private void Start()
    {
        transform.position = startPoint.transform.position;
        originalColor = lifeBar.GetComponent<Image>().color;
        lifeBar.transform.localScale = new Vector3(PegarTamanhoBarra(lifeProgress, life), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        lifeText.text = PegarPorcentagemBarra(lifeProgress, life, 100) + "%";
        
    }

    private void Update()
    {
        if (lifeProgress < 50)
        {
            if (redFlag)
            {
                lifeBar.GetComponent<Image>().color = new Color(255, 0, 0, a / 255);
                a -= 4;
                if (a <= 100)
                    redFlag = false;
            } else if(!redFlag)
            {
                lifeBar.GetComponent<Image>().color = new Color(255, 0, 0, a / 255);
                a += 4;
                if (a >= 200)
                    redFlag = true;
            }
        } else
        {
            lifeBar.GetComponent<Image>().color = originalColor;
        }
    }

    public void addLife(float addLife)
    {
        lifeProgress += addLife;
        if (lifeProgress > life)
            lifeProgress = life;

        lifeBar.transform.localScale = new Vector3(PegarTamanhoBarra(lifeProgress, life), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        lifeText.text = PegarPorcentagemBarra(lifeProgress, life, 100) + "%";     
    }

    public void RemoveLife(float removeLife)
    {
        if ((lifeProgress - removeLife) <= 0)
        {
            lifeProgress = 0;
            transform.position = startPoint.transform.position;
            lifeProgress = 30;
        } else
        {
            lifeProgress -= removeLife;
        }
            
        lifeBar.transform.localScale = new Vector3(PegarTamanhoBarra(lifeProgress, life), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        lifeText.text = PegarPorcentagemBarra(lifeProgress, life, 100) + "%";
    }

    public float PegarTamanhoBarra(float min, float max)
    {
        return min / max;
    }

    public int PegarPorcentagemBarra(float min, float max, int fator)
    {
        return Mathf.RoundToInt(PegarTamanhoBarra(min, max) * fator);
    }
}
