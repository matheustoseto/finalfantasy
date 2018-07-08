﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour {

    private CharacterState charState = null;

    [Header("UI:")]
    public GameObject lifeBar;
    public Text lifeText;
    public GameObject hitPopUp;
    public GameObject damage;

    [Header("Status:")]
    public float life;
    public float initialLife = 30;
    public float lifeProgress;

    private bool redFlag = true;
    private float a = 170;
    private Color originalColor;

    public bool noDamage = false;

    private void Awake()
    {
        if (initialLife > life)
            initialLife = life;
        lifeProgress = initialLife;

        charState = GetComponent<CharacterState>();
    }

    private void Start()
    {
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
                a -= 150 * Time.deltaTime;
                if (a <= 100)
                    redFlag = false;
            } else if(!redFlag)
            {
                lifeBar.GetComponent<Image>().color = new Color(255, 0, 0, a / 255);
                a += 150 * Time.deltaTime;
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
        if (!noDamage)
        {
            GameObject hit = Instantiate(hitPopUp, transform.position + new Vector3(Random.Range(-1,1), 6, 0), hitPopUp.transform.rotation);
            hit.GetComponent<HitPopUp>().SetText(removeLife.ToString());
            hit.GetComponent<HitPopUp>().text.color = Color.yellow;
            damage.SetActive(true);

            if ((lifeProgress - removeLife) <= 0)
            {
                lifeProgress = 0;
                charState.EventDead();
            }
            else
            {
                lifeProgress -= removeLife;
            }

            lifeBar.transform.localScale = new Vector3(PegarTamanhoBarra(lifeProgress, life), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
            lifeText.text = PegarPorcentagemBarra(lifeProgress, life, 100) + "%";
        }      
    }

    public float PegarTamanhoBarra(float min, float max)
    {
        return min / max;
    }

    public int PegarPorcentagemBarra(float min, float max, int fator)
    {
        return Mathf.RoundToInt(PegarTamanhoBarra(min, max) * fator);
    }

    #region Restart Status

    public void ResetToInitialLife()
    {
        lifeProgress = initialLife;
        lifeBar.transform.localScale = new Vector3(PegarTamanhoBarra(lifeProgress, life), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        lifeText.text = PegarPorcentagemBarra(lifeProgress, life, 100) + "%";
    }
    #endregion
}
