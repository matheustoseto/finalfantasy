using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusSurvive : MonoBehaviour {

    public float fome = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (fome <= 0f)
            Debug.Log("Morreu");
        else
            fome -= Time.deltaTime;

	}

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 100, 100), "Fome: " + fome.ToString());
    }
}
