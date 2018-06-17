using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour {

    public GameObject progressBar;
    public bool activeBar;
    private float speed;
    private Action action;

    private static Progress instance = null;
    public static Progress Instance { get { return instance; } }

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (activeBar && progressBar.transform.localScale.x <= 1.2F)
        {
            progressBar.transform.position = PlayerManager.Instance.transform.position + new Vector3(0,4,0);
            progressBar.transform.localScale += new Vector3(speed, 0, 0);
            if (progressBar.transform.localScale.x >= 1.2F)
            {
                DisableProgressBar();
                action();
            }                
        }
    }

    public void ProgressBar(float speed, Action action)
    {
        this.speed = speed;
        this.action = action;
        ActiveProgressBar();       
    }

    private void ActiveProgressBar()
    {
        activeBar = true;
        progressBar.SetActive(true);      
    }

    public void DisableProgressBar()
    {
        activeBar = false;
        progressBar.SetActive(false);
        progressBar.transform.localScale = new Vector3(0,0.2F,0.2F);
    }
}
