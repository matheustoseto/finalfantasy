using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour {

    public GameObject lifeBar;
    public Text lifeText;
    public float life;
    public float lifeProgress;

    private void Start()
    {
        lifeBar.transform.localScale = new Vector3(PegarTamanhoBarra(lifeProgress, life), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        lifeText.text = PegarPorcentagemBarra(lifeProgress, life, 100) + "%";
    }

    public void addLife(float addLife)
    {
        lifeProgress += addLife;
        if (lifeProgress > life)
            lifeProgress = life;

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
